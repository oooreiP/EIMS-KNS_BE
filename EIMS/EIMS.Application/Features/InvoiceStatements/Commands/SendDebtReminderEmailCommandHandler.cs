using EIMS.Application.Commons.Helpers;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Mails;
using EIMS.Application.Features.InvoiceStatements.Queries;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceStatements.Commands
{
    public class SendDebtReminderEmailCommandHandler : IRequestHandler<SendDebtReminderEmailCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        private readonly IEmailSenderService _emailSender;
        private readonly ISender _sender;

        public SendDebtReminderEmailCommandHandler(IUnitOfWork uow, IEmailSenderService emailSender, ISender sender)
        {
            _uow = uow;
            _emailSender = emailSender;
            _sender = sender;
        }

        public async Task<Result> Handle(SendDebtReminderEmailCommand request, CancellationToken cancellationToken)
        {
            var statement = await _uow.InvoiceStatementRepository.GetByIdWithInvoicesAsync(request.StatementId);
            if (statement == null)
                return Result.Fail($"Statement with id {request.StatementId} not found.");

            if (statement.StatusID != 3 && statement.StatusID != 4)
                return Result.Fail("Statement must be in 'Wait for payment' or 'Partially Paid' status to send a debt reminder.");

            if (statement.PaidAmount >= statement.TotalAmount)
                return Result.Fail("Statement is already fully paid.");

            var toEmail = statement.Customer?.ContactEmail;
            if (string.IsNullOrWhiteSpace(toEmail))
                return Result.Fail("No recipient email found for this statement.");

            var balanceDue = statement.TotalAmount - statement.PaidAmount;

            var template = await _uow.EmailTemplateRepository.GetAllQueryable()
                .FirstOrDefaultAsync(x => x.TemplateCode == "STATEMENT_SEND" && x.LanguageCode == "vi" && x.IsActive, cancellationToken);
            if (template == null)
                return Result.Fail("Không tìm thấy mẫu email 'STATEMENT_SEND' đang hoạt động.");

            var company = await _uow.CompanyRepository.GetByIdAsync(1);
            var companyName = company?.CompanyName ?? "EIMS";

            var periodMonth = statement.PeriodMonth > 0 ? statement.PeriodMonth : statement.DueDate.Month;
            var periodYear = statement.PeriodYear > 0 ? statement.PeriodYear : statement.DueDate.Year;

            string attachmentListHtml = string.Empty;

            var mailRequest = new FEMailRequest
            {
                ToEmail = toEmail,
                Subject = template.Subject,
                EmailBody = template.BodyContent
            };

            if (request.IncludePdf)
            {
                var pdfResult = await _sender.Send(new GetStatementPdfQuery(request.StatementId, request.RootPath), cancellationToken);
                if (pdfResult.IsFailed)
                    return Result.Fail(pdfResult.Errors);

                var attachment = pdfResult.Value;
                mailRequest.AttachmentUrls.Add(new FileAttachment
                {
                    FileName = attachment.FileName,
                    FileContent = attachment.FileContent
                });

                attachmentListHtml = $@"
                <div style='margin-top:20px'>
                    <p><strong>📎 File đính kèm:</strong></p>
                    <ul style='padding-left:20px;margin:5px 0'>
                        <li>{attachment.FileName}</li>
                    </ul>
                </div>";
            }

            var replacements = new Dictionary<string, string>
            {
                { "{{CustomerName}}", statement.Customer?.CustomerName ?? "Quý khách" },
                { "{{CompanyName}}", companyName },
                { "{{Month}}", periodMonth.ToString("00") },
                { "{{Year}}", periodYear.ToString() },
                { "{{DueDate}}", statement.DueDate.ToString("dd/MM/yyyy") },
                { "{{TotalAmount}}", balanceDue.ToString("N0") },
                { "{{AttachmentList}}", attachmentListHtml }
            };

            mailRequest.Subject = EmailHelper.ReplacePlaceholders(mailRequest.Subject, replacements);
            mailRequest.EmailBody = EmailHelper.ReplacePlaceholders(mailRequest.EmailBody, replacements);

            var sendResult = await _emailSender.SendMailAsync(mailRequest);
            if (sendResult.IsFailed)
                return sendResult;

            return Result.Ok();
        }       
    }
}