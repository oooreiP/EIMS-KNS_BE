using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Mails;
using EIMS.Application.Features.InvoiceStatements.Queries;
using FluentResults;
using MediatR;
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

            var subject = string.IsNullOrWhiteSpace(request.Subject)
                ? $"Statement {statement.StatementCode}"
                : request.Subject;

            var message = string.IsNullOrWhiteSpace(request.Message)
                ? $@"Kính gửi Quý khách,

Vui lòng xem đính kèm bảng kê công nợ {statement.StatementCode} để Quý khách đối chiếu.
Nếu cần hỗ trợ thêm, vui lòng liên hệ bộ phận chăm sóc khách hàng của chúng tôi.

Trân trọng cảm ơn Quý khách.

Trân trọng,
EIMS Support Team"
                : request.Message;

            var mailRequest = new FEMailRequest
            {
                ToEmail = toEmail,
                Subject = subject,
                EmailBody = message,
                CcEmails = request.CcEmails ?? new(),
                BccEmails = request.BccEmails ?? new()
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
            }

            var sendResult = await _emailSender.SendMailAsync(mailRequest);
            if (sendResult.IsFailed)
                return sendResult;

            if (statement.StatusID != 5 && statement.StatusID != 6 && statement.StatusID != 7)
            {
                statement.StatusID = 3; // Sent
                await _uow.InvoiceStatementRepository.UpdateAsync(statement);
                await _uow.SaveChanges();
            }

            return Result.Ok();
        }
    }
}
