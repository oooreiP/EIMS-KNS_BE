using EIMS.Application.DTOs.Mails;
using EIMS.Domain.Entities;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IEmailSenderService
    {
        Task<Result> SendEmailCoreAsync(Invoice invoice, string subjectPrefix, string message);
        Task<Result> SendMailAsync(MailRequest mailRequest);
        Task<Result> SendInvoiceEmailAsync(string recipientEmail, int invoiceId, string message);
        Task<Result> SendStatusUpdateNotificationAsync(int invoiceId, int newStatusId);
        Task<Result> SendMailAsync(FEMailRequest mailRequest);
        
    }
}
