using EIMS.Application.Commons.Helpers;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Mails;
using EIMS.Application.Features.InvoiceStatements.Queries;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceStatements.Commands
{
    public class SendStatementEmailCommandHandler : IRequestHandler<SendStatementEmailCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        private readonly IEmailSenderService _emailSender;
        private readonly ISender _sender;

        public SendStatementEmailCommandHandler(IUnitOfWork uow, IEmailSenderService emailSender, ISender sender)
        {
            _uow = uow;
            _emailSender = emailSender;
            _sender = sender;
        }

        public async Task<Result> Handle(SendStatementEmailCommand request, CancellationToken cancellationToken)
        {
            var statement = await _uow.InvoiceStatementRepository.GetByIdWithInvoicesAsync(request.StatementId);
            if (statement == null)
                return Result.Fail($"Statement with id {request.StatementId} not found.");

            var toEmail = !string.IsNullOrWhiteSpace(request.RecipientEmail)
                ? request.RecipientEmail
                : statement.Customer?.ContactEmail;

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

                attachmentListHtml = $"<li>{attachment.FileName}</li>";
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

            if (statement.StatusID != 1)
                return Result.Fail(new Error("Statement must be draft to be wait for payment"));                                          
            statement.StatusID = 3; // Sent
            await _uow.InvoiceStatementRepository.UpdateAsync(statement);
            await _uow.SaveChanges();
            return Result.Ok();
        }
    }
}
