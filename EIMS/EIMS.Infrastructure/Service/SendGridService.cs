using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Mails;
using EIMS.Application.Features.Emails.Commands;
using EIMS.Domain.Entities;
using FluentResults;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net; // Required for HttpStatusCode
using System.Threading.Tasks;

namespace EIMS.Infrastructure.Service
{
    public class SendGridService : IEmailService, IEmailSenderService
    {
        private readonly EmailSettings _settings;
        private readonly ILogger<SendGridService> _logger;
        private readonly SendGridClient _client;
        private readonly IUnitOfWork _unitOfWork;

        public SendGridService(
            IOptions<EmailSettings> settings,
            ILogger<SendGridService> logger,
            IUnitOfWork unitOfWork)
        {
            _settings = settings.Value;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _client = new SendGridClient(_settings.ApiToken);
        }

        // --- CORE SENDING LOGIC ---
        public async Task<Result> SendMailAsync(MailRequest mailRequest)
        {
            try
            {
                var from = new EmailAddress(_settings.FromEmail, _settings.FromName);
                var to = new EmailAddress(mailRequest.Email);
                var plainTextContent = "Notification from EIMS";
                var htmlContent = !string.IsNullOrWhiteSpace(mailRequest.EmailBody)
                                  ? mailRequest.EmailBody
                                  : "<p>Notification from EIMS (No content provided)</p>";

                var msg = MailHelper.CreateSingleEmail(from, to, mailRequest.Subject, plainTextContent, htmlContent);
                // Add Attachments (Cloudinary URLs)
                if (mailRequest.CloudinaryUrls != null && mailRequest.CloudinaryUrls.Any())
                {
                    using var httpClient = new System.Net.Http.HttpClient();
                    foreach (var url in mailRequest.CloudinaryUrls)
                    {
                        var fileBytes = await httpClient.GetByteArrayAsync(url);
                        var base64Content = Convert.ToBase64String(fileBytes);
                        var fileName = Path.GetFileName(new Uri(url).LocalPath);

                        msg.AddAttachment(fileName, base64Content);
                    }
                }

                var response = await _client.SendEmailAsync(msg);

                // FIX 1: Check StatusCode directly (SendGrid doesn't have IsSuccessStatusCode)
                if (response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.OK)
                {
                    _logger.LogInformation($"Email sent to {mailRequest.Email}");
                    return Result.Ok();
                }

                var body = await response.Body.ReadAsStringAsync();
                _logger.LogError($"SendGrid Error: {response.StatusCode} - {body}");
                return Result.Fail($"Email failed: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SendGrid Exception");
                return Result.Fail(ex.Message);
            }
        }

        // --- ADAPTER METHODS ---

        // 1. For Registration (FEMailRequest)
        public async Task<Result> SendMailAsync(FEMailRequest mailRequest)
        {
            return await SendMailAsync(new MailRequest
            {
                Email = mailRequest.ToEmail,
                Subject = mailRequest.Subject,
                EmailBody = mailRequest.EmailBody
            });
        }

        // 2. For Invoice Sending (Command)
        public async Task<Result> SendInvoiceEmailAsync(SendInvoiceEmailCommand request)
        {
            // FIX 2: Changed 'request.Message' to 'request.CustomMessage'
            return await SendInvoiceEmailAsync(request.RecipientEmail, request.InvoiceId, request.CustomMessage);
        }

        // 3. For Invoice Sending (Direct)
        public async Task<Result> SendInvoiceEmailAsync(string recipientEmail, int invoiceId, string message)
        {
            try
            {
                var invoice = await _unitOfWork.InvoicesRepository.GetByIdAsync(invoiceId, "Customer");
                if (invoice == null) return Result.Fail("Invoice not found");

                List<string> attachments = new List<string>();
                if (!string.IsNullOrEmpty(invoice.FilePath)) attachments.Add(invoice.FilePath);
                if (!string.IsNullOrEmpty(invoice.XMLPath)) attachments.Add(invoice.XMLPath);

                string subject = $"[EIMS] Hóa đơn #{invoice.InvoiceNumber}";

                var emailBody = $@"
                    <div style='font-family:Arial,Helvetica,sans-serif; font-size:14px; color:#333;'>
                        <h2 style='color:#007BFF;'>Xin chào {invoice.Customer?.CustomerName},</h2>
                        <p>{message}</p>
                        <table style='margin:15px 0; border-collapse:collapse;'>
                            <tr><td style='font-weight:bold;'>Mã hóa đơn:</td><td>{invoice.InvoiceNumber}</td></tr>
                            <tr><td style='font-weight:bold;'>Tổng tiền:</td><td style='color:#D63384;'>{invoice.TotalAmount:n0} VND</td></tr>
                        </table>
                        <p>Hóa đơn được đính kèm bên dưới.</p>
                    </div>";

                return await SendMailAsync(new MailRequest
                {
                    Email = recipientEmail,
                    Subject = subject,
                    EmailBody = emailBody,
                    CloudinaryUrls = attachments
                });
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        // 4. Status Updates
        public async Task<Result> SendStatusUpdateNotificationAsync(int invoiceId, int newStatusId)
        {
            return Result.Ok();
        }

        public Task<Result> SendEmailCoreAsync(Invoice invoice, string subjectPrefix, string message)
        {
            return Task.FromResult(Result.Ok());
        }
    }
}