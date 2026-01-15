using EIMS.Application.Features.Invoices.Commands;
using EIMS.Application.Features.Invoices.Commands.CreateInvoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

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
        byte[] SignPdfUsingSpire(byte[] pdfBytes, X509Certificate2 signingCert);
    }
}