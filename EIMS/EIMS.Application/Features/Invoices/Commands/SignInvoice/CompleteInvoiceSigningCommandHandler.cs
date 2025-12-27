using EIMS.Application.Commons;
using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Constants;
using EIMS.Domain.Entities;
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
        private readonly IPdfService _pdfService;
        private readonly IFileStorageService _fileStorageService;

        public CompleteInvoiceSigningCommandHandler(IUnitOfWork uow, IInvoiceXMLService invoiceXmlService, IPdfService pdfService, IFileStorageService fileStorageService)
        {
            _unitOfWork = uow;
            _invoiceXmlService = invoiceXmlService;
            _pdfService = pdfService;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result> Handle(CompleteInvoiceSigningCommand request, CancellationToken cancellationToken)
        {
            var invoice = await _unitOfWork.InvoicesRepository.GetByIdAsync(request.InvoiceId);
            if (invoice == null) return Result.Fail("Không tìm thấy hóa đơn.");
            var xmlDoc = await _invoiceXmlService.LoadXmlFromUrlAsync(invoice.XMLPath);
            var signedXmlDoc = XmlHelpers.EmbedSignatureToXml(xmlDoc, request.SignatureBase64, request.CertificateBase64);
            bool isValid = XmlHelpers.ValidateXmlSignature(signedXmlDoc);
            if (!isValid) return Result.Fail("Chữ ký số không hợp lệ hoặc dữ liệu đã bị thay đổi.");
            var newFileName = $"Invoice_{invoice.InvoiceNumber}_Signed.xml";
            var newUrl = await _invoiceXmlService.UploadXmlAsync(signedXmlDoc, newFileName);
            invoice.XMLPath = newUrl;
            invoice.InvoiceStatusID = 8;
            invoice.SignDate = DateTime.UtcNow;
            invoice.DigitalSignature = request.SignatureBase64;
            await _unitOfWork.InvoicesRepository.UpdateAsync(invoice);
            await _unitOfWork.SaveChanges();
            try
            {
                byte[] pdfBytes = await _pdfService.ConvertXmlToPdfAsync(
                 invoice.InvoiceID,
                 request.RootPath);
                string pdfFileName = $"Invoice_{invoice.InvoiceNumber}.pdf";

                using (var pdfStream = new MemoryStream(pdfBytes))
                {
                    var pdfUpload = await _fileStorageService.UploadFileAsync(pdfStream, pdfFileName, "invoices/pdf");

                    if (pdfUpload.IsSuccess)
                    {
                        // 3. Lưu link PDF vào DB
                        invoice.FilePath = pdfUpload.Value.Url;
                        await _unitOfWork.InvoicesRepository.UpdateAsync(invoice);
                        await _unitOfWork.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                // _logger.LogError($"Ký thành công nhưng lỗi tạo PDF: {ex.Message}");
            }
            var history = new InvoiceHistory
            {
                InvoiceID = request.InvoiceId,
                ActionType = InvoiceActionTypes.Signed,
                PerformedBy = invoice.IssuerID,
                Date = DateTime.UtcNow
            };
            await _unitOfWork.InvoiceHistoryRepository.CreateAsync(history);
            await _unitOfWork.SaveChanges();
            return Result.Ok();
        }
    }
}
