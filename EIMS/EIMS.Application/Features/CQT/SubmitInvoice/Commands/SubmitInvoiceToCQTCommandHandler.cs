using EIMS.Application.Commons;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Mapping;
using EIMS.Application.DTOs.Results;
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

        public SubmitInvoiceToCQTCommandHandler(IUnitOfWork uow, ITaxApiClient taxClient, IInvoiceXMLService invoiceXMLService)
        {
            _uow = uow;
            _taxClient = taxClient;
            _invoiceXMLService = invoiceXMLService;
        }

        public async Task<Result<SubmitInvoiceToCQTResult>> Handle(
        SubmitInvoiceToCQTCommand request,
        CancellationToken cancellationToken)
        {
            // 1. Lấy dữ liệu và kiểm tra
            var invoice = await _uow.InvoicesRepository.GetByIdAsync(request.invoiceId, "Customer,InvoiceItems.Product,Template.Serial.Prefix,Template.Serial.SerialStatus, Template.Serial.InvoiceType");
            var messageCode = await _uow.TaxMessageCodeRepository.GetByIdAsync(19);

            if (invoice == null)
                return Result.Fail("Invoice not found");

            // 2. Map và Serialize
            var tDiep = InvoiceXmlMapper.MapThongDiepToXmlModel(invoice, messageCode,1, null);
            var referenceId = tDiep.TtinChung.MaThongDiep;
            var xmlPayload = XmlHelpers.Serialize(tDiep);

            // 3. TẠO LOG GỐC (Trạng thái: PENDING - Đang gửi CQT)
            var log = new TaxApiLog
            {
                InvoiceID = invoice.InvoiceID,
                RequestPayload = xmlPayload,
                TaxApiStatusID = 1, // PENDING: Đang gửi CQT
                Timestamp = DateTime.UtcNow
            };

            await _uow.TaxApiLogRepository.CreateAsync(log);
            await _uow.SaveChanges();
            var taxResponse = await _taxClient.SendTaxMessageAsync(xmlPayload, referenceId);
            string apiStatusCode = taxResponse.MLTDiep == "202" ? "KQ01" :
                       taxResponse.MLTDiep == "204" ? "TBxx" :
                       "TB01"; 
            var responseLog = new TaxApiLog
            {
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
                invoice.InvoiceStatusID = 3; // Ví dụ: Đã cấp mã CQT
            }
            else if (responseLog.TaxApiStatusID == 3) // REJECTED: CQT từ chối (TB02-TB11, KQ02)
            {
                invoice.InvoiceStatusID = 3; // Ví dụ: Bị từ chối
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
