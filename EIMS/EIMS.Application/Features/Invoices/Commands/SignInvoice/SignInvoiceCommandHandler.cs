using EIMS.Application.Commons.Helpers;
using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Constants;
using EIMS.Domain.Entities;
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
    public class SignInvoiceCommandHandler : IRequestHandler<SignInvoiceCommand, Result<long>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInvoiceXMLService _invoiceXmlService;
        private readonly IPdfService _pdfService;
        private readonly IFileStorageService _fileStorageService;
        private readonly ICurrentUserService _currentUser;
        public SignInvoiceCommandHandler(IUnitOfWork unitOfWork, IInvoiceXMLService invoiceXmlService, IPdfService pdfService, IFileStorageService fileStorageService, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _invoiceXmlService = invoiceXmlService;
            _pdfService = pdfService;
            _fileStorageService = fileStorageService;
            _currentUser = currentUser;
        }
        public async Task<Result<long>> Handle(SignInvoiceCommand request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(_currentUser.UserId);
            // BƯỚC 1: VALIDATION NGHIỆP VỤ
            var invoice = await _unitOfWork.InvoicesRepository.GetByIdAsync(request.InvoiceId, "Customer,InvoiceItems.Product,Template.Serial.Prefix,Template.Serial.SerialStatus, Template.Serial.InvoiceType,InvoiceStatus,Company");
            var template = await _unitOfWork.InvoiceTemplateRepository.GetByIdAsync(invoice.TemplateID);
            var serial = await _unitOfWork.SerialRepository.GetByIdAndLockAsync(template.SerialID);
            if (serial == null)
                return Result.Fail(new Error($"Template {serial.SerialID} not found").WithMetadata("ErrorCode", "Invoice.Sign.Failed"));
            if (invoice == null)
                return Result.Fail("Không tìm thấy hóa đơn.");
            if (string.IsNullOrEmpty(invoice.XMLPath))
                return Result.Fail("Chưa có file XML gốc để ký.");
            if( invoice.InvoiceStatusID == 8)
            return Result.Fail(new Error("Invoice is signed"));
            // if (invoice.InvoiceStatusID != 7)
            // {
            //     try
            //     {
                    invoice.InvoiceNumber = await GenerateInvoiceNumberAsync(serial.SerialID);
                    invoice.XMLPath = await _invoiceXmlService.GenerateAndUploadXmlAsync(invoice);
                    invoice.InvoiceStatusID = 7;
                // }
                // catch (Exception ex)
                // {
                //     return Result.Fail(ex.Message);
                // }

                await _unitOfWork.InvoicesRepository.UpdateAsync(invoice);
                await _unitOfWork.SaveChanges();
            // }
            var certResult = await _invoiceXmlService.GetCertificateAsync(invoice.CompanyId ?? 1);
            if (certResult.IsFailed)
                return Result.Fail(certResult.Errors);
            var signingCert = certResult.Value;
            var xmlDoc = await _invoiceXmlService.LoadXmlFromUrlAsync(invoice.XMLPath);
            var signedXmlContent = XmlHelpers.SignElectronicDocument(xmlDoc.OuterXml, signingCert);
            var signedXmlDoc = new XmlDocument();
            signedXmlDoc.PreserveWhitespace = true;
            signedXmlDoc.LoadXml(signedXmlContent.SignedXml);
            var newFileName = $"Invoice_{invoice.InvoiceNumber}_Signed.xml";
            var newUrl = await _invoiceXmlService.UploadXmlAsync(signedXmlDoc, newFileName);
            // BƯỚC 6: CẬP NHẬT DB          
            invoice.XMLPath = newUrl; // Cập nhật đường dẫn mới
            invoice.InvoiceStatusID = 8; // Trạng thái: Signed (Đã ký)
            invoice.DigitalSignature = signedXmlContent.SignatureValue;
            invoice.SignDate = DateTime.UtcNow;
            await _unitOfWork.InvoicesRepository.UpdateAsync(invoice);
            await _unitOfWork.SaveChanges();
            try
            {
                byte[] pdfBytes = await _pdfService.ConvertXmlToPdfAsync(invoice.InvoiceID, request.RootPath);
                double sizeInKB = pdfBytes.Length / 1024.0;
                using (var pdfStream = new MemoryStream(pdfBytes))
                {
                    string fileName = $"Invoice_{invoice.InvoiceNumber}_{Guid.NewGuid()}.pdf";
                    var uploadResult = await _fileStorageService.UploadFileAsync(pdfStream, fileName, "invoices");

                    if (uploadResult.IsSuccess)
                    {
                        invoice.FilePath = uploadResult.Value.Url;
                        await _unitOfWork.InvoicesRepository.UpdateAsync(invoice);
                        await _unitOfWork.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            var history = new InvoiceHistory
            {
                InvoiceID = request.InvoiceId,
                ActionType = InvoiceActionTypes.Signed,
                PerformedBy = userId,
                Date = DateTime.UtcNow
            };
            await _unitOfWork.InvoiceHistoryRepository.CreateAsync(history);
            await _unitOfWork.SaveChanges();

            if (invoice.InvoiceNumber == null) return Result.Fail("Lỗi: Không có số hóa đơn.");
            return Result.Ok((long)invoice.InvoiceNumber);
        }
        private async Task<long?> GenerateInvoiceNumberAsync(int serialId)
        {
            var serial = await _unitOfWork.SerialRepository.GetByIdAndLockAsync(serialId);

            if (serial == null)
            {
                throw new Exception($"Không tìm thấy dải ký hiệu (Serial) với ID: {serialId}");
            }
            serial.CurrentInvoiceNumber += 1;
            await _unitOfWork.SerialRepository.UpdateAsync(serial);
            await _unitOfWork.SaveChanges();
            return serial.CurrentInvoiceNumber;
        }
    }
}
