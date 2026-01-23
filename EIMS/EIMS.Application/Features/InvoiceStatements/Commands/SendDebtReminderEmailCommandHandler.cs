using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Mails;
using EIMS.Application.Features.InvoiceStatements.Queries;
using FluentResults;
using MediatR;
using System;
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

            var subject = string.IsNullOrWhiteSpace(request.Subject)
                ? $"Nhắc nợ: Bảng kê công nợ {statement.StatementCode}"
                : request.Subject;

            var message = string.IsNullOrWhiteSpace(request.Message)
                ? $@"Kính gửi Quý khách,

Chúng tôi xin thông báo bảng kê công nợ {statement.StatementCode} đang chờ thanh toán.
Số tiền còn lại: {balanceDue:N0}đ.
Hạn thanh toán: {statement.DueDate:dd/MM/yyyy}.

Vui lòng xem bảng kê đính kèm để đối chiếu và thực hiện thanh toán.
Nếu cần hỗ trợ thêm, xin liên hệ bộ phận chăm sóc khách hàng.

Trân trọng,
EIMS Support Team"
                : request.Message;

            var mailRequest = new FEMailRequest
            {
                ToEmail = toEmail,
                Subject = subject,
                EmailBody = message
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

            return Result.Ok();
        }
    }
}