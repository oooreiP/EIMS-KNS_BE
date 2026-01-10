using EIMS.Application.Commons.Helpers;
using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.CQT.NotifyInvoiceError
{
    public class SendErrorNotificationHandler : IRequestHandler<SendErrorNotificationCommand, Result<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IFileStorageService _fileService;
        private readonly IInvoiceXMLService _invoiceXmlService;
        private readonly ITaxApiClient _taxClient; 

        public SendErrorNotificationHandler(IUnitOfWork uow, IFileStorageService fileService, ITaxApiClient taxClient, IInvoiceXMLService invoiceXmlService)
        {
            _uow = uow;
            _fileService = fileService;
            _taxClient = taxClient;
            _invoiceXmlService = invoiceXmlService;
        }

            public async Task<Result<string>> Handle(SendErrorNotificationCommand request, CancellationToken cancellationToken)
            {
               var noti = await _uow.ErrorNotificationRepository.GetByIdAsync(request.NotificationID, "Details.Invoice");
                if (noti == null) return Result.Fail("Notification not found");
                if (noti.Status != 1) return Result.Fail("Chỉ gửi được tờ khai ở trạng thái Nháp/Lỗi.");

                try
                {
                    // 1. Download XML from Cloud
                    var rawXml = await _invoiceXmlService.DownloadStringAsync(noti.XMLPath);
                    var certResult = _invoiceXmlService.GetCertificate();
                    if (certResult.IsFailed) return Result.Fail(certResult.Errors);
                    var cert = certResult.Value;
                    // 2. Digital Sign (Ký số)
                    var signedXml = XmlHelpers.SignTB04Xml(rawXml, cert);
                    string signedXmlPayload = signedXml.SignedXml;
                    // 3. Send to T-VAN / CQT (Gửi thông điệp 300)
                    var taxResponse = await _taxClient.SendTaxMessageAsync(signedXmlPayload, noti.MTDiep);
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
                    TaxApiStatusID = XmlHelpers.MapApiCodeToStatusId(apiStatusCode),
                    Timestamp = DateTime.UtcNow
                };

                await _uow.TaxApiLogRepository.CreateAsync(responseLog);
                await _uow.SaveChanges();
                if (taxResponse.IsSuccess)
                {
                    // A. Cập nhật trạng thái Tờ khai 04/SS
                    noti.Status = 3; // 2 = Sent (Đã gửi/Chờ phản hồi) - Lưu ý: 3 thường là Accepted
                    noti.SignedData = signedXml.SignatureValue;

                    // B. Cập nhật trạng thái các Hóa đơn liên quan (Vòng lặp)
                    if (noti.Details != null)
                    {
                        foreach (var detail in noti.Details)
                        {
                            var invoice = detail.Invoice;
                            if (invoice == null) continue; // Bỏ qua nếu không tìm thấy hóa đơn gốc

                            // Logic cập nhật trạng thái hóa đơn dựa trên loại sai sót
                            switch (detail.ErrorType)
                            {
                                case 1: // Hủy (Cancel)
                                    // Khi gửi thông báo Hủy thành công -> Hóa đơn chuyển thành Hủy
                                    invoice.InvoiceStatusID = 3; // Cancelled
                                    break;

                                case 2: // Điều chỉnh (Adjustment)
                                    // Đánh dấu là đang có biến động điều chỉnh
                                    invoice.InvoiceStatusID = 10; // Adjustment_In_Progress
                                    break;

                                case 3: // Thay thế (Replacement)
                                    // Đánh dấu hóa đơn gốc là bị thay thế (hoặc đang xử lý thay thế)
                                    invoice.InvoiceStatusID = 11; // Replacement_In_Progress
                                    break;

                                case 4: // Giải trình (Explanation)
                                    // Giải trình thường không đổi trạng thái hóa đơn, hoặc có thể set flag cảnh báo
                                    break;
                            }
                            // Add vào context để update
                            await _uow.InvoicesRepository.UpdateAsync(invoice);
                        }
                    }

                    // C. Lưu thay đổi cuối cùng
                    await _uow.SaveChanges();

                    return Result.Ok(taxResponse.MTDiep);
                }
                else
                {
                    // Gửi thất bại (Lỗi kỹ thuật từ T-VAN hoặc Validation sync)
                    noti.Status = 5; // Error/Rejected locally
                    await _uow.SaveChanges();

                    return Result.Fail($"Gửi thất bại: {taxResponse.RawResponse} - {taxResponse.SoTBao}");
                }
            }
            catch (Exception ex)
                {
                    return Result.Fail($"Lỗi hệ thống: {ex.Message}");
                }
            }
        }
    }
