using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using EIMS.Domain.Entities;
using EIMS.Application.Commons.Helpers;

namespace EIMS.Application.Features.Invoices.Commands.SignInvoice
{
    public class GetHashToSignCommandHandler : IRequestHandler<GetHashToSignCommand, Result<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IInvoiceXMLService _invoiceXmlService;

        public GetHashToSignCommandHandler(IUnitOfWork uow, IInvoiceXMLService invoiceXmlService)
        {
            _uow = uow;
            _invoiceXmlService = invoiceXmlService;
        }

        public async Task<Result<string>> Handle(GetHashToSignCommand request, CancellationToken cancellationToken)
        {
            var invoice = await _uow.InvoicesRepository.GetByIdAsync(request.InvoiceId, "Customer,Company,InvoiceItems.Product,Template.Serial.Prefix,Template.Serial.SerialStatus, Template.Serial.InvoiceType,InvoiceStatus");
            if (invoice == null)
            {
                return Result.Fail("Không tìm thấy hóa đơn.");
            }
            var template = await _uow.InvoiceTemplateRepository.GetByIdAsync(invoice.TemplateID);
            if (template == null)
            {
                return Result.Fail(new Error("Không tìm thấy mẫu hóa đơn.").WithMetadata("ErrorCode", "Invoice.Sign.Failed"));
            }
            var serial = await _uow.SerialRepository.GetByIdAndLockAsync(template.SerialID);
            if (serial == null)
            {
                return Result.Fail(new Error($"Không tìm thấy dải ký hiệu (Serial) với ID: {template.SerialID}").WithMetadata("ErrorCode", "Invoice.Sign.Failed"));
            }
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
            await _uow.InvoicesRepository.UpdateAsync(invoice);
            await _uow.SaveChanges();
            if (string.IsNullOrEmpty(invoice.XMLPath)) return Result.Fail("Hóa đơn chưa có file XML gốc.");
            var xmlContent = await _invoiceXmlService.DownloadStringAsync(invoice.XMLPath);

            var xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.LoadXml(xmlContent);

            // 3. Tạo SignedInfo để client ký (RSA-SHA256)
            try
            {
                string signedInfoToSign = XmlHelpers.BuildSignedInfoToSign(xmlDoc);
                return Result.Ok(signedInfoToSign);
            }
            catch (Exception ex)
            {
                return Result.Fail($"Lỗi khi tạo dữ liệu ký số: {ex.Message}");
            }
        }
        private async Task<long?> GenerateInvoiceNumberAsync(int serialId)
        {          
            var serial = await _uow.SerialRepository.GetByIdAndLockAsync(serialId);

            if (serial == null)
            {
                throw new Exception($"Không tìm thấy dải ký hiệu (Serial) với ID: {serialId}");
            }
            serial.CurrentInvoiceNumber += 1;
            await _uow.SerialRepository.UpdateAsync(serial);
            await _uow.SaveChanges();
            return serial.CurrentInvoiceNumber;
        }
    }
}
