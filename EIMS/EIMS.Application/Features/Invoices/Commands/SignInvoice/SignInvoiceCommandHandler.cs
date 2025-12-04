using EIMS.Application.Commons;
using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EIMS.Application.Features.Invoices.Commands.SignInvoice
{
    public class SignInvoiceCommandHandler : IRequestHandler<SignInvoiceCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInvoiceXMLService _invoiceXmlService;

        public SignInvoiceCommandHandler(IUnitOfWork unitOfWork, IInvoiceXMLService invoiceXmlService)
        {
            _unitOfWork = unitOfWork;
            _invoiceXmlService = invoiceXmlService;
        }
        public async Task<Result> Handle(SignInvoiceCommand request, CancellationToken cancellationToken)
        {
            // BƯỚC 1: VALIDATION NGHIỆP VỤ
            var invoice = await _unitOfWork.InvoicesRepository.GetByIdAsync(request.InvoiceId);
            if (invoice == null)
                return Result.Fail("Không tìm thấy hóa đơn.");

            // Kiểm tra trạng thái: Chỉ ký được khi đang ở trạng thái hợp lệ (VD: Valid, Signing_In_Progress)
            // Không thể ký lại hóa đơn đã ký (Signed) hoặc đã gửi (Sent)
            if (invoice.InvoiceStatusID != 4 && invoice.InvoiceStatusID != 3 && invoice.InvoiceStatusID != 1)
                return Result.Fail($"Hóa đơn đang ở trạng thái {invoice.InvoiceStatusID}, không thể ký.");

            if (string.IsNullOrEmpty(invoice.XMLPath))
                return Result.Fail("Chưa có file XML gốc để ký.");

            // BƯỚC 2: LẤY CHỨNG THƯ SỐ (X509Certificate2)
            // Lấy từ KeyVault, File PFX, hoặc Windows Store dựa trên cấu hình hoặc request.CertificateSerial
            var certResult = _invoiceXmlService.GetCertificate(request.CertificateSerial);
            if (certResult.IsFailed)
                return Result.Fail(certResult.Errors);

            var signingCert = certResult.Value;

            // BƯỚC 3: TẢI XML GỐC VỀ (Download)
            var xmlDoc = await _invoiceXmlService.LoadXmlFromUrlAsync(invoice.XMLPath);

            // BƯỚC 4: THỰC HIỆN KÝ SỐ (Hàm SignInvoiceXml bạn đã viết)
            // Lưu ý: Hàm này trả về string XML đã ký
            var signedXmlContent = XmlHelpers.SignInvoiceXml(xmlDoc.OuterXml, signingCert);

            // Convert lại sang XmlDocument để upload
            var signedXmlDoc = new XmlDocument();
            signedXmlDoc.PreserveWhitespace = true;
            signedXmlDoc.LoadXml(signedXmlContent.SignedXml);

            // BƯỚC 5: UPLOAD FILE ĐÃ KÝ LÊN CLOUD (Re-upload)
            // Đặt tên file mới để phân biệt
            var newFileName = $"Invoice_{invoice.InvoiceNumber}_Signed.xml";
            var newUrl = await _invoiceXmlService.UploadXmlAsync(signedXmlDoc, newFileName);

            // BƯỚC 6: CẬP NHẬT DB
            invoice.XMLPath = newUrl; // Cập nhật đường dẫn mới
            invoice.InvoiceStatusID = 5; // Trạng thái: Signed (Đã ký)
            invoice.SignDate = DateTime.UtcNow;
            invoice.DigitalSignature = signedXmlContent.SignatureValue;

            // (Tùy chọn) Lưu thông tin người ký, thời điểm ký vào Audit Log hoặc bảng Invoice
            // invoice.SignedDate = DateTime.UtcNow;
            // invoice.SignedBy = _currentUserService.UserId;

            await _unitOfWork.InvoicesRepository.UpdateAsync(invoice);
            await _unitOfWork.SaveChanges();

            return Result.Ok();
        }
    }
}
