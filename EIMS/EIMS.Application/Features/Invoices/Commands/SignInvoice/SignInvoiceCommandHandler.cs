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
    public class SignInvoiceCommandHandler : IRequestHandler<SignInvoiceCommand, Result<long?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInvoiceXMLService _invoiceXmlService;

        public SignInvoiceCommandHandler(IUnitOfWork unitOfWork, IInvoiceXMLService invoiceXmlService)
        {
            _unitOfWork = unitOfWork;
            _invoiceXmlService = invoiceXmlService;
        }
        public async Task<Result<long?>> Handle(SignInvoiceCommand request, CancellationToken cancellationToken)
        {
            // BƯỚC 1: VALIDATION NGHIỆP VỤ
            var invoice = await _unitOfWork.InvoicesRepository.GetByIdAsync(request.InvoiceId, "Customer,InvoiceItems.Product,Template.Serial.Prefix,Template.Serial.SerialStatus, Template.Serial.InvoiceType,InvoiceStatus");
            var template = await _unitOfWork.InvoiceTemplateRepository.GetByIdAsync(invoice.TemplateID);
            var serial = await _unitOfWork.SerialRepository.GetByIdAndLockAsync(template.SerialID);
            if (serial == null)
                return Result.Fail(new Error($"Template {serial.SerialID} not found").WithMetadata("ErrorCode", "Invoice.Sign.Failed"));
            if (invoice == null)
                return Result.Fail("Không tìm thấy hóa đơn.");
            if (string.IsNullOrEmpty(invoice.XMLPath))
                return Result.Fail("Chưa có file XML gốc để ký.");
            if (invoice.InvoiceStatusID != 7)
            {
                try
                {
                    invoice.InvoiceNumber = await GenerateInvoiceNumberAsync(serial.SerialID);
                    invoice.XMLPath = await _invoiceXmlService.GenerateAndUploadXmlAsync(invoice);
                    invoice.InvoiceStatusID = 7;
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message);
                }

                await _unitOfWork.InvoicesRepository.UpdateAsync(invoice);
                await _unitOfWork.SaveChanges();
            }
            var certResult = _invoiceXmlService.GetCertificate(request.CertificateSerial);
            if (certResult.IsFailed)
                return Result.Fail(certResult.Errors);
            var signingCert = certResult.Value;
            var xmlDoc = await _invoiceXmlService.LoadXmlFromUrlAsync(invoice.XMLPath);
            var signedXmlContent = XmlHelpers.SignInvoiceXml(xmlDoc.OuterXml, signingCert);
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

            return Result.Ok(invoice.InvoiceNumber);
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
