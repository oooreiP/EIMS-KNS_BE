using EIMS.Application.Commons.Helpers;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Mapping;
using EIMS.Application.DTOs.XMLModels.TB04;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.CQT.NotifyInvoiceError
{
    public class NotifyInvoiceErrorCommandHandler : IRequestHandler<NotifyInvoiceErrorCommand, Result>
{
    private readonly IUnitOfWork _uow;
    private readonly ITaxApiClient _taxClient;
    private readonly IInvoiceXMLService _invoiceXmlService;

    public NotifyInvoiceErrorCommandHandler(
        IUnitOfWork uow, 
        ITaxApiClient taxClient,
        IInvoiceXMLService invoiceXmlService)
    {
        _uow = uow;
        _taxClient = taxClient;
        _invoiceXmlService = invoiceXmlService;
    }

        public async Task<Result> Handle(NotifyInvoiceErrorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // =========================================================================
                // BƯỚC 1: LẤY DỮ LIỆU & VALIDATE
                // =========================================================================
                var invoice = await _uow.InvoicesRepository.GetByIdAsync(request.InvoiceId, "Customer,InvoiceItems.Product,Template.Serial.Prefix,Template.Serial.SerialStatus, Template.Serial.InvoiceType,Company");

                if (invoice == null)
                    return Result.Fail("Không tìm thấy hóa đơn.");
                var validationResult = ValidateInvoiceStatus(invoice, request.ErrorType);
                if (validationResult.IsFailed)
                {
                    return validationResult;
                }
                if (string.IsNullOrEmpty(invoice.TaxAuthorityCode))
                    return Result.Fail("Hóa đơn chưa được cấp mã CQT. Không thể gửi thông báo sai sót (Mẫu 04/SS).");
                var template = invoice.Template;
                var serial = template.Serial;
                var prefix = serial.Prefix;
                string khmsHDon = prefix.PrefixID.ToString();
                string khHDon =
                    $"{serial.SerialStatus.Symbol}" +
                    $"{serial.Year}" +
                    $"{serial.InvoiceType.Symbol}" +
                    $"{serial.Tail}";
                // =========================================================================
                // BƯỚC 2: TẠO MODEL XML TB04 (Theo cấu trúc chuẩn Image_30be13.png)
                // =========================================================================
                var messageCode = new TaxMessageCode { MessageCode = "300", FlowType = 1 };
                string mnGui = "K0311357436";
                var ttChung = InvoiceXmlMapper.GenerateTTChung(messageCode, 1, mnGui);
                var referenceId = ttChung.MaThongDiep;
                // 2.2 Tạo Body (DLieu -> TBao -> DLTBao -> DSHDon)
                var tBao = new TBao04
                {
                    PBan = "2.1.0",
                    MSo = "04/SS-HĐĐT",
                    Ten = "Thông báo hóa đơn điện tử có sai sót",
                    Loai = 1,
                    MCQT = "10925",
                    TCQT = "Cục Thuế TP. Hồ Chí Minh",
                    So = await _invoiceXmlService.GenerateNextNotificationNumberAsync(), // Số thông báo: TB/001...
                    NTBCCQT = DateTime.Now.ToString("yyyy-MM-dd"), 
                    DLTBao = new DLTBao
                    {
                        MST = "0311357436",
                        MDVQHNSach = "", 
                        DDanh = "Hồ Chí Minh",
                        NTBao = DateTime.Now.ToString("yyyy-MM-dd"),

                        DSHDon = new DSHDonWrapper
                        {
                            HDon = new List<HDonTB04>
                            {
                                new HDonTB04
                                {
                                    STT = 1,
                                    MCCQT = invoice.TaxAuthorityCode, 
                                    KHMSHDon = khmsHDon, 
                                    KHHDon = khHDon,         
                                    SHDon = invoice.InvoiceNumber.HasValue ? invoice.InvoiceNumber.Value.ToString("D7") : "",
                                    Ngay = invoice.IssuedDate?.ToString("yyyy-MM-dd") ?? DateTime.Now.ToString("yyyy-MM-dd"),
                                    LADHDDT = 1, // 1: Có mã theo NĐ123
                                    TCTBao = request.ErrorType, // 1: Hủy, 2: ĐC, 3: TT, 4: GT
                                    LDo = request.Reason
                                }
                            }
                        }
                    }
                };

                var tDiep = new TDiepTB04
                {
                    TTChung = ttChung,
                    DLieu = new DLieuTB04 { TBao = tBao }
                };
                string rawXml = XmlHelpers.Serialize(tDiep);
                var certResult = _invoiceXmlService.GetCertificate();
                if (certResult.IsFailed) return Result.Fail(certResult.Errors);
                var cert = certResult.Value;
                var signedXmlResult = XmlHelpers.SignTB04Xml(rawXml, cert);
                string signedXmlPayload = signedXmlResult.SignedXml;

                var apiLog = new TaxApiLog
                {
                    InvoiceID = invoice.InvoiceID,
                    MTDiep = referenceId,      
                    RequestPayload = signedXmlPayload,
                    Timestamp = DateTime.UtcNow,
                    TaxApiStatusID = 1       
                };

                await _uow.TaxApiLogRepository.CreateAsync(apiLog);
                await _uow.SaveChanges();
                var taxResponse = await _taxClient.SendTaxMessageAsync(signedXmlPayload, referenceId);
                string apiStatusCode = taxResponse.MLTDiep == "202" ? "KQ01" :
                      taxResponse.MLTDiep == "204" ? "TBxx" :
                      "TB01";
                var responseLog = new TaxApiLog
                {
                    RequestPayload = signedXmlPayload,
                    ResponsePayload = taxResponse.RawResponse,
                    MTDiep = taxResponse.MTDiep,
                    SoTBao = taxResponse.SoTBao,
                    MCCQT = taxResponse.MCCQT,
                    InvoiceID = request.InvoiceId,
                    TaxApiStatusID = XmlHelpers.MapApiCodeToStatusId(apiStatusCode)
                };
                await _uow.TaxApiLogRepository.CreateAsync(responseLog);
                await _uow.SaveChanges();
                await _uow.TaxApiLogRepository.UpdateAsync(responseLog);
                if (taxResponse.IsSuccess) // Nếu CQT nhận thành công (Thường trả về 301 hoặc 204 OK)
                {
                    switch (request.ErrorType)
                    {
                        case 1: // Hủy (Cancel)
                            invoice.InvoiceStatusID = 3; // Cancelled
                            break;

                        case 2: // Điều chỉnh (Adjustment)
                            invoice.InvoiceStatusID = 10; // Adjustment_In_Progress
                            break;

                        case 3: // Thay thế (Replacement)
                            // Đánh dấu hóa đơn gốc là đang bị thay thế
                            invoice.InvoiceStatusID = 11; // Replacement_In_Progress
                            break;

                        case 4:
                            break;
                    }

                    await _uow.InvoicesRepository.UpdateAsync(invoice);
                    await _uow.SaveChanges();

                    return Result.Ok();
                }
                else
                {
                    // Xử lý lỗi từ CQT (Ví dụ: Mã CQT không tồn tại, XML sai...)
                    return Result.Fail($"CQT từ chối tiếp nhận TB04: {taxResponse.SoTBao} - {taxResponse.RawResponse}");
                }
            }
            catch (Exception ex)
            {
                return Result.Fail($"Lỗi hệ thống khi gửi thông báo sai sót: {ex.Message}");
            }
        }
        private Result ValidateInvoiceStatus(Invoice invoice, int errorType)
        {
            // CHECK 1: Kiểm tra Mã CQT (Điều kiện tiên quyết)
            if (string.IsNullOrEmpty(invoice.TaxAuthorityCode))
            {
                return Result.Fail($"Hóa đơn hiện tại chưa được cấp Mã CQT (Trạng thái: {invoice.InvoiceStatusID}). Bạn không thể gửi thông báo sai sót. Hãy kiểm tra lại quy trình phát hành.");
            }

            // CHECK 2: Kiểm tra Logic theo từng loại sai sót
            switch (errorType)
            {
                case 1: // Hủy (Cancel)
                    if (invoice.InvoiceStatusID == 3) // 3 = Cancelled
                    {
                        return Result.Fail("Hóa đơn này ĐÃ BỊ HỦY trước đó. Không thể thực hiện hủy lần nữa.");
                    }
                    // Có thể hủy hóa đơn đang điều chỉnh/thay thế dở dang, nhưng thường là hủy hóa đơn Issued (6)
                    break;

                case 2: // Điều chỉnh (Adjustment)
                    if (invoice.InvoiceStatusID == 3)
                    {
                        return Result.Fail("Hóa đơn này ĐÃ BỊ HỦY. Hóa đơn hủy không còn giá trị sử dụng nên KHÔNG THỂ ĐIỀU CHỈNH.");
                    }
                    if (invoice.InvoiceStatusID == 11) 
                    {
                        return Result.Fail("Hóa đơn này đang trong quá trình Thay thế. Không nên thực hiện Điều chỉnh.");
                    }
                    // Lưu ý: Có thể điều chỉnh tiếp một hóa đơn đã điều chỉnh (11)
                    break;

                case 3: // Thay thế (Replacement)
                    if (invoice.InvoiceStatusID == 3)
                    {
                        return Result.Fail("Hóa đơn này ĐÃ BỊ HỦY. Vui lòng lập hóa đơn mới hoàn toàn thay vì dùng nghiệp vụ Thay thế.");
                    }
                    if (invoice.InvoiceStatusID == 10)
                    {
                        return Result.Fail("Hóa đơn này đang được Điều chỉnh. Không nên thực hiện Thay thế.");
                    }
                    if (invoice.InvoiceStatusID == 4 || invoice.InvoiceStatusID == 5)
                    {
                        return Result.Fail("Hóa đơn này đã được Điều chỉnh/ Thay thế từ trước. Không nên thực hiện Thay thế.");
                    }
                    break;

                case 4: // Giải trình (Explanation)
                        // Giải trình thường được chấp nhận ở mọi trạng thái có Mã CQT
                    break;

                default:
                    return Result.Fail($"Loại sai sót không hợp lệ (ErrorType: {errorType}). Chỉ chấp nhận 1, 2, 3, 4.");
            }
            return Result.Ok();
        }
    }
}
