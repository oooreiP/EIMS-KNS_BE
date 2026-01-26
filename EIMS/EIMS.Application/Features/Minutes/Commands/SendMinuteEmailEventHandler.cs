using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Mails;
using EIMS.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Minutes.Commands
{
    public class SendMinuteEmailEventHandler : INotificationHandler<MinuteSignedEvent>
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<SendMinuteEmailEventHandler> _logger;

        public SendMinuteEmailEventHandler(IServiceScopeFactory scopeFactory, ILogger<SendMinuteEmailEventHandler> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        public Task Handle(MinuteSignedEvent notification, CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                await ProcessEmailInBackground(notification.MinuteId);
            });

            return Task.CompletedTask;
        }

        private async Task ProcessEmailInBackground(int minuteId)
        {
            using var scope = _scopeFactory.CreateScope();
            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

            try
            {
                var minute = await uow.MinuteInvoiceRepository.GetByIdAsync(minuteId, "Invoice.Customer");
                if (minute == null) return;
                string defaultTemplateCode = (minute.MinutesType == MinutesType.Replacement)
                                            ? "MINUTES_REPLACE"
                                            : "MINUTES_ADJUST";

                var emailTemplate = await uow.EmailTemplateRepository.GetAllQueryable()
                    .FirstOrDefaultAsync(x => x.TemplateCode == defaultTemplateCode && x.LanguageCode == "vi");

                if (emailTemplate == null)
                {
                    _logger.LogWarning($"Không tìm thấy mẫu email: {defaultTemplateCode}");
                    return;
                }

                var attachmentList = new List<FileAttachment>
            {
                new FileAttachment
                {
                    FileUrl = minute.FilePath, // URL file đã ký
                    FileName = GetFileNameFromUrl(minute.FilePath)
                }
            };

                string attachmentHtmlList = $"<li style='margin-bottom: 5px;'>📎 <strong>{attachmentList[0].FileName}</strong></li>";
                attachmentHtmlList += "<br/><em style='color: #666; font-size: 12px;'>(File được đính kèm theo email)</em>";

                var replacements = new Dictionary<string, string>
            {
                { "{{CustomerName}}", minute.Invoice.InvoiceCustomerName },
                { "{{InvoiceNumber}}", $"{minute.Invoice.InvoiceSymbol}_{minute.Invoice.InvoiceNumber}" },
                { "{{CreatedDate}}", DateTime.Now.ToString("dd/MM/yyyy") },
                { "{{Reason}}", minute.Description },
                { "{{AttachmentList}}", attachmentHtmlList },
                { "{{IssuedDate}}", minute.Invoice.IssuedDate?.ToString("dd/MM/yyyy") ?? "" },
            };

                string subject = ReplacePlaceholders(emailTemplate.Subject, replacements);
                string body = ReplacePlaceholders(emailTemplate.BodyContent, replacements);
                string toEmail = minute.Invoice.Customer.ContactEmail;

                var mailRequest = new FEMailRequest
                {
                    ToEmail = toEmail,
                    Subject = subject,
                    EmailBody = body,
                    AttachmentUrls = attachmentList
                };

                await emailService.SendMailAsync(mailRequest);
                _logger.LogInformation($"Đã gửi mail biên bản {minute.MinuteCode} tới {toEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Lỗi gửi mail background cho biên bản ID: {minuteId}");
            }
        }

        // Các hàm Helper giữ nguyên
        private string GetFileNameFromUrl(string url)
        {
            try { return Path.GetFileName(new Uri(url).LocalPath); }
            catch { return "document.pdf"; }
        }

        private string ReplacePlaceholders(string text, Dictionary<string, string> replacements)
        {
            if (string.IsNullOrEmpty(text)) return "";
            foreach (var item in replacements)
            {
                text = text.Replace(item.Key, item.Value ?? "");
            }
            return text;
        }
    }
}
