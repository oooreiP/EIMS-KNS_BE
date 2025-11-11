using EIMS.Application.DTOs.Mails;
using FluentResults;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IEmailService
    {
        Task<Result> SendInvoiceEmailAsync(
             string recipientEmail,
             string customerName,
             string invoiceNumber,
             decimal totalAmount,
             string message,
             List<string> cloudinaryUrls);
        Task<Result> SendMailAsync(MailRequest mailRequest);
    }
}
