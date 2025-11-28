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
            var invoice = await _uow.InvoicesRepository.GetByIdAsync(request.SubmitInvoiceRequest.InvoiceId, "Customer,InvoiceItems.Product,Template.Serial.Prefix,Template.Serial.SerialStatus, Template.Serial.InvoiceType");
            var messageCode = await _uow.TaxMessageCodeRepository.GetByIdAsync(request.SubmitInvoiceRequest.MessageCodeId);

            if (invoice == null)
                return Result.Fail("Invoice not found");

            // 2. Map và Serialize
            var tDiep = InvoiceXmlMapper.MapThongDiepToXmlModel(invoice, messageCode, request.SubmitInvoiceRequest.DataCount, null);
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
            var taxResponse = await _taxClient.SendInvoiceAsync(xmlPayload, referenceId);
            string apiStatusCode = taxResponse.MLTDiep == "202" ? "KQ01" :
                       taxResponse.MLTDiep == "204" ? "TBxx" :
                       "TB01"; 
            var responseLog = new TaxApiLog
            {
                ResponsePayload = taxResponse.RawResponse,
                MTDiep = taxResponse.MTDiep,
                SoTBao = taxResponse.SoTBao, // Chứa mã TBxx/KQxx chi tiết
                MCCQT = taxResponse.MCCQT,
                InvoiceID = request.SubmitInvoiceRequest.InvoiceId,
                TaxApiStatusID = MapApiCodeToSystemStatusId(apiStatusCode)
            };
            // Ánh xạ mã TB/KQ (trong SoTBao) về TaxApiStatusID logic của hệ thống (1-7)
            // Giả định hàm này lấy mã TBxx/KQxx từ taxResponse.SoTBao để tra cứu
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
            // 7. CẬP NHẬT TRẠNG THÁI HÓA ĐƠN
            if (responseLog.TaxApiStatusID == 2) // RECEIVED: CQT đã tiếp nhận (TB01)
            {
                invoice.InvoiceStatusID = 3; // Ví dụ: Đã gửi/CQT tiếp nhận
            }
            else if (responseLog.TaxApiStatusID == 4) // APPROVED: CQT đã cấp mã (KQ01)
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
        public int MapApiCodeToSystemStatusId(string apiStatusCode)
        {
            // Tìm kiếm trong các mã TBxx và KQxx đã định nghĩa
            var status = _uow.TaxApiStatusRepository.FindAsync(s => s.Code == apiStatusCode);

            // Nếu tìm thấy mã CQT cụ thể (ví dụ: TB01, KQ02)
            if (status != null)
            {
                // Ánh xạ đến mã trạng thái logic tương ứng
                return apiStatusCode switch
                {
                    "TB01" => 2, // RECEIVED
                    "KQ01" => 4, // APPROVED
                    "KQ03" => 6, // PROCESSING
                    "KQ04" => 7, // NOT_FOUND
                                 // Tất cả các lỗi TB02-TB11, KQ02 ánh xạ về REJECTED
                    _ when apiStatusCode.StartsWith("TB") && apiStatusCode != "TB01" => 3, // REJECTED
                    "KQ02" => 3, // REJECTED
                    "TB12" => 5, // FAILED (Lỗi kỹ thuật)
                    _ => 5 // Default: FAILED (Nếu mã không xác định)
                };
            }

            // Trường hợp không khớp với bất kỳ mã nào trong TaxApiStatus
            return 5; // Mặc định là FAILED (Lỗi hệ thống)
        }
    }
}
