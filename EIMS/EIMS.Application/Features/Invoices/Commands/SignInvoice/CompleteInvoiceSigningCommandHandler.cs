using EIMS.Application.Commons;
using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Commands.SignInvoice
{
    public class CompleteInvoiceSigningCommandHandler : IRequestHandler<CompleteInvoiceSigningCommand,Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInvoiceXMLService _invoiceXmlService;

        public CompleteInvoiceSigningCommandHandler(IUnitOfWork uow, IInvoiceXMLService invoiceXmlService)
        {
            _unitOfWork = uow;
            _invoiceXmlService = invoiceXmlService;
        }

        public async Task<Result> Handle(CompleteInvoiceSigningCommand request, CancellationToken cancellationToken)
        {
            // BƯỚC 1: VALIDATION
            var invoice = await _unitOfWork.InvoicesRepository.GetByIdAsync(request.InvoiceId);
            if (invoice == null) return Result.Fail("Không tìm thấy hóa đơn.");

            // BƯỚC 2: TẢI XML GỐC
            var xmlDoc = await _invoiceXmlService.LoadXmlFromUrlAsync(invoice.XMLPath);

            // BƯỚC 3: GHÉP CHỮ KÝ VÀO XML (Merge)
            // Thay vì tự ký, ta dùng hàm InsertSignature để nhúng chữ ký FE gửi lên
            var signedXmlDoc = XmlHelpers.EmbedSignatureToXml(xmlDoc, request.SignatureBase64, request.CertificateBase64);
            // BƯỚC 4: VALIDATE CHỮ KÝ (Quan trọng)
            // Phải kiểm tra xem chữ ký này có đúng là ký cho nội dung XML này không
            bool isValid = XmlHelpers.ValidateXmlSignature(signedXmlDoc);
            if (!isValid) return Result.Fail("Chữ ký số không hợp lệ hoặc dữ liệu đã bị thay đổi.");

            // BƯỚC 5: UPLOAD & UPDATE DB (Giống code cũ)
            var newFileName = $"Invoice_{invoice.InvoiceNumber}_Signed.xml";
            var newUrl = await _invoiceXmlService.UploadXmlAsync(signedXmlDoc, newFileName);

            invoice.XMLPath = newUrl;
            invoice.InvoiceStatusID = 8;
            invoice.SignDate = DateTime.UtcNow;
            invoice.DigitalSignature = request.SignatureBase64;

            await _unitOfWork.InvoicesRepository.UpdateAsync(invoice);
            await _unitOfWork.SaveChanges();

            return Result.Ok();
        }
    }
}
