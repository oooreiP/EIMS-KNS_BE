using EIMS.Application.Commons;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Mapping;
using EIMS.Application.DTOs.Results;
using EIMS.Application.DTOs.XMLModels;
using EIMS.Application.DTOs.XMLModels.ThongDiep;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text;
using System.Xml.Serialization;

namespace EIMS.Application.Features.CQT.SubmitInvoice.Commands
{
    public class SubmitInvoiceToCQTCommandHandler : IRequestHandler<SubmitInvoiceToCQTCommand, Result<SubmitInvoiceToCQTResult>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IInvoiceXMLService _invoiceXMLService;
        private readonly ITaxApiClient _taxClient;
        private readonly HttpClient _httpClient;
        public SubmitInvoiceToCQTCommandHandler(IUnitOfWork uow, ITaxApiClient taxClient, IInvoiceXMLService invoiceXMLService, HttpClient httpClient)
        {
            _uow = uow;
            _taxClient = taxClient;
            _invoiceXMLService = invoiceXMLService;
            _httpClient = httpClient;
        }

        public async Task<Result<SubmitInvoiceToCQTResult>> Handle(
        SubmitInvoiceToCQTCommand request,
        CancellationToken cancellationToken)
        {
            // 1. Lấy dữ liệu và kiểm tra
            var invoice = await _uow.InvoicesRepository.GetByIdAsync(request.invoiceId, "Customer,InvoiceItems.Product,Template.Serial.Prefix,Template.Serial.SerialStatus, Template.Serial.InvoiceType");
            var messageCode = await _uow.TaxMessageCodeRepository.GetByIdAsync(19);

            if (invoice == null)
                return Result.Fail("Không tìm thấy hóa đơn");
            if (invoice.InvoiceStatusID != 8 && invoice.DigitalSignature == null)
            {
                return Result.Fail("Hóa đơn phải kí trước khi gửi");
            }
            // 2. Map và Serialize
            var tDiep = InvoiceXmlMapper.MapThongDiepToXmlModel(invoice, messageCode,1, null);
            if (!string.IsNullOrEmpty(invoice.XMLPath))
            {
                try
                {
                    string hdonXmlContent = "";
                    if (invoice.XMLPath.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                    {
                        // Nếu là link Cloudinary/S3
                        hdonXmlContent = await _httpClient.GetStringAsync(invoice.XMLPath);
                    }
                    else if (File.Exists(invoice.XMLPath))
                    {
                        // Nếu là file lưu trên ổ cứng Server
                        hdonXmlContent = await File.ReadAllTextAsync(invoice.XMLPath);
                    }
                    if (!string.IsNullOrEmpty(hdonXmlContent))
                    {
                        // B. Deserialize chuỗi XML đó ngược lại thành Object HDon
                        // Lưu ý: Cần thêm hàm Deserialize vào XmlHelper (xem bên dưới)
                        var existingHDon = XmlHelpers.DeserializeFlexible<HDon>(hdonXmlContent);

                        // C. Gán đè Hóa đơn cũ vào Thông điệp mới
                        // Việc này giữ nguyên chữ ký số (nếu class HDon map đúng Signature)
                        tDiep.TDiepDLieu.HDon = existingHDon;
                    }
                }
                catch (Exception ex)
                {
                     return Result.Fail("Không đọc được file XML hóa đơn gốc"); // Nếu muốn chặn luôn
                }
            }
            var referenceId = tDiep.TtinChung.MaThongDiep;
            var xmlPayload = XmlHelpers.Serialize(tDiep);
            var log = new TaxApiLog
            {
                InvoiceID = invoice.InvoiceID,
                RequestPayload = xmlPayload,
                TaxApiStatusID = 1, // PENDING: Đang gửi CQT
                MTDiep = "200",
                Timestamp = DateTime.UtcNow
            };

            await _uow.TaxApiLogRepository.CreateAsync(log);
            await _uow.SaveChanges();
            var taxResponse = await _taxClient.SendTaxMessageAsync(xmlPayload, referenceId);
            string apiStatusCode = taxResponse.MLTDiep == "202" ? "KQ01" :
                       taxResponse.MLTDiep == "204" ? "TBxx" :
                       "TB01"; 
            log.ResponsePayload = taxResponse.RawResponse;
            await _uow.TaxApiLogRepository.UpdateAsync(log);
            await _uow.SaveChanges();
            var responseLog = new TaxApiLog
            {
                RequestPayload = xmlPayload,
                ResponsePayload = taxResponse.RawResponse,
                MTDiep = taxResponse.MTDiep,
                SoTBao = taxResponse.SoTBao, 
                MCCQT = taxResponse.MCCQT,
                InvoiceID = request.invoiceId,
                TaxApiStatusID = XmlHelpers.MapApiCodeToStatusId(apiStatusCode)
            };
            await _uow.TaxApiLogRepository.CreateAsync(responseLog);
            await _uow.SaveChanges();
            if (taxResponse.MCCQT != null) {
                var xmlDoc = await _invoiceXMLService.LoadXmlFromUrlAsync(invoice.XMLPath);
                _invoiceXMLService.EmbedMccqtIntoXml(xmlDoc,taxResponse.MCCQT);
                var signedXmlDoc = new System.Xml.XmlDocument();
                signedXmlDoc.PreserveWhitespace = true;
                signedXmlDoc.LoadXml(xmlDoc.OuterXml);
                var newFileName = $"Invoice_{invoice.InvoiceNumber}_Signed.xml";
                var newUrl = await _invoiceXMLService.UploadXmlAsync(signedXmlDoc, newFileName);
                invoice.XMLPath = newUrl; // Cập nhật đường dẫn mới
                invoice.InvoiceStatusID = 5; // Trạng thái: Signed (Đã ký)
                await _uow.InvoicesRepository.UpdateAsync(invoice);
                await _uow.SaveChanges();
            }
            if (responseLog.TaxApiStatusID == 2) 
            {
                invoice.InvoiceStatusID = 3; 
            }
            else if (responseLog.TaxApiStatusID == 30) 
            {
                invoice.TaxAuthorityCode = taxResponse.MCCQT;
                invoice.InvoiceStatusID = 12; // Ví dụ: Đã cấp mã CQT
            }
            else if (responseLog.TaxApiStatusID == 3) // REJECTED: CQT từ chối (TB02-TB11, KQ02)
            {
                invoice.InvoiceStatusID = 8; // Ví dụ: Bị từ chối
            }
            // ... Thêm các trạng thái InvoiceStatusID khác (PROCESSING, FAILED) nếu cần

            await _uow.InvoicesRepository.UpdateAsync(invoice);
            await _uow.SaveChanges();

            // 8. TRẢ VỀ KẾT QUẢ
            if (log.TaxApiStatusID == 3 || log.TaxApiStatusID == 5)
            {
                return Result.Fail($"Gửi hóa đơn thất bại. Mã lỗi CQT: {taxResponse.SoTBao} - {taxResponse.RawResponse}");
            }

            return Result.Ok(new SubmitInvoiceToCQTResult
            {
                MTDiep = taxResponse.MTDiep,
                MLTDiep = taxResponse.MLTDiep,
                SoTBao = taxResponse.SoTBao,
                MCCQT = taxResponse.MCCQT,
                Status = log.TaxApiStatusID.ToString()
            });
        }
    }
}
