using EIMS.Application.Features.Invoices.Commands;
using EIMS.Application.Features.Invoices.Commands.CreateInvoice;
using EIMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IPdfService
    {
        Task<byte[]> ConvertHtmlToPdfAsync(string htmlContent);
        Task<byte[]> ConvertXmlToPdfAsync(int invoiceId, string rootPath);
        Task<string> GenerateInvoiceHtmlAsync(int invoiceId, string rootPath);
        Task<string> PreviewInvoiceHtmlAsync(BaseInvoiceCommand request, string rootPath);
        Task<string> GenerateNotificationHtmlAsync(int notificationId, string rootPath);
        Task<byte[]> ConvertNotificationToPdfAsync(int notificationId, string rootPath);
        string TransformXmlToHtml(string xmlContent, string xsltPath, XsltArgumentList? args = null);
        Task<string> GetBlankInvoicePreviewAsync(int templateId, int companyId, string rootPath);
        byte[] SignPdfUsingSpire(byte[] pdfBytes, X509Certificate2 signingCert);
        string SerializeInvoiceToXml(Invoice invoice);
    }
}