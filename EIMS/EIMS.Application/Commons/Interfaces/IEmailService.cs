using EIMS.Application.DTOs.Mails;
using EIMS.Application.Features.Emails.Commands;
using EIMS.Domain.Entities;
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
        Task<Result> SendMailAsync(MailRequest mailRequest);
        Task<Result> SendMailAsync(FEMailRequest mailRequest);
        Task<Result> SendInvoiceEmailAsync(SendInvoiceEmailCommand request);
        Task<Result> SendStatusUpdateNotificationAsync(int invoiceId, int newStatusId);
    }
}
