using EIMS.Application.Commons.Helpers;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Mapping;
using EIMS.Application.DTOs.Results;
using EIMS.Application.DTOs.XMLModels;
using EIMS.Application.DTOs.XMLModels.ThongDiep;
using EIMS.Domain.Constants;
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
        private readonly ICurrentUserService _currentUser;
        private readonly HttpClient _httpClient;
        public SubmitInvoiceToCQTCommandHandler(IUnitOfWork uow, ITaxApiClient taxClient, IInvoiceXMLService invoiceXMLService, HttpClient httpClient, ICurrentUserService currentUser)
        {
            _uow = uow;
            _taxClient = taxClient;
            _invoiceXMLService = invoiceXMLService;
            _httpClient = httpClient;
            _currentUser = currentUser;
        }

        public async Task<Result<SubmitInvoiceToCQTResult>> Handle(
        SubmitInvoiceToCQTCommand request,
        CancellationToken cancellationToken)
        {
            var userId = int.Parse(_currentUser.UserId);
            var invoice = await _uow.InvoicesRepository.GetByIdAsync(request.invoiceId, "Customer,InvoiceItems.Product,Template.Serial.Prefix,Template.Serial.SerialStatus, Template.Serial.InvoiceType,Company");
            var original = new Invoice();
            if (invoice.OriginalInvoiceID != null)
            {
                original = await _uow.InvoicesRepository.GetAllQueryable()
               .Include(x => x.InvoiceItems)
               .Include(x => x.Customer)
               .Include(x => x.Company)
               .OrderByDescending(x => x.InvoiceID)
               .FirstOrDefaultAsync(x => x.InvoiceID == invoice.OriginalInvoiceID);
            }
            var messageCode = await _uow.TaxMessageCodeRepository.GetByIdAsync(19);

            if (invoice == null)    
                return Result.Fail("Không tìm thấy hóa đơn");
            if (invoice.InvoiceStatusID != 8 && invoice.DigitalSignature == null)
            {
                return Result.Fail("Hóa đơn phải kí trước khi gửi");
            }
            var tDiep = InvoiceXmlMapper.MapThongDiepToXmlModel(invoice, messageCode,1, null);
            if (!string.IsNullOrEmpty(invoice.XMLPath))
            {
                try
                {
                    string hdonXmlContent = "";
                    if (invoice.XMLPath.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                    {
                        hdonXmlContent = await _httpClient.GetStringAsync(invoice.XMLPath);
                    }
                    else if (File.Exists(invoice.XMLPath))
                    {
                        hdonXmlContent = await File.ReadAllTextAsync(invoice.XMLPath);
                    }
                    if (!string.IsNullOrEmpty(hdonXmlContent))
                    {
                        var existingHDon = XmlHelpers.DeserializeFlexible<HDon>(hdonXmlContent);
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
                invoice.InvoiceStatusID = 12; 
            }
            else if (responseLog.TaxApiStatusID == 3) // REJECTED: CQT từ chối (TB02-TB11, KQ02)
            {
                invoice.InvoiceStatusID = 8;
                if (original != null)
                {
                    original.InvoiceStatusID = 2; // Trả lại status về phát hành để tạo lại hóa đơn thay thế
                    await _uow.InvoicesRepository.UpdateAsync(original);
                }
            }
            await _uow.InvoicesRepository.UpdateAsync(invoice);           
            var history = new InvoiceHistory
            {
                InvoiceID = request.invoiceId,
                ActionType = taxResponse.MLTDiep == "202" ? InvoiceActionTypes.CqtAccepted : InvoiceActionTypes.CqtRejected,
                PerformedBy = userId,
                Date = DateTime.UtcNow
            };
            await _uow.InvoiceHistoryRepository.CreateAsync(history);
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
