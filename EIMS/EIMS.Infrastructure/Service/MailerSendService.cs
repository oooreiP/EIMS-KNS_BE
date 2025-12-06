using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs;
using EIMS.Application.DTOs.Mails;
using EIMS.Domain.Entities;
using FluentResults;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EIMS.Infrastructure.Service
{
    public class MailerSendService : IEmailService
    {
        private readonly HttpClient _httpClient;
        private readonly EmailSettings _settings;
        private readonly ILogger<MailerSendService> _logger;

        public MailerSendService(HttpClient httpClient, IOptions<EmailSettings> settings, ILogger<MailerSendService> logger)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _logger = logger;
        }
        public async Task<Result> SendMailAsync(Application.DTOs.Mails.MailRequest mailRequest)
        {
            try
            {
                var url = "https://api.mailersend.com/v1/email";

                // 1. Prepare Attachments (if any)
                var attachments = new List<object>();
                if (mailRequest.CloudinaryUrls != null && mailRequest.CloudinaryUrls.Any())
                {
                    foreach (var fileUrl in mailRequest.CloudinaryUrls)
                    {
                        // Download file from Cloudinary
                        var fileBytes = await _httpClient.GetByteArrayAsync(fileUrl);
                        var base64Content = Convert.ToBase64String(fileBytes);
                        var fileName = Path.GetFileName(new Uri(fileUrl).LocalPath);

                        attachments.Add(new
                        {
                            content = base64Content,
                            filename = fileName,
                            disposition = "attachment"
                        });
                    }
                }

                // 2. Build Request Body (MailerSend Format)
                var payload = new
                {
                    from = new { email = _settings.FromEmail, name = _settings.FromName },
                    to = new[] { new { email = mailRequest.Email } },
                    subject = mailRequest.Subject,
                    html = mailRequest.EmailBody,
                    attachments = attachments // Add attachments list
                };

                var jsonPayload = JsonSerializer.Serialize(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                // 3. Setup Request Authorization
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = content
                };
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _settings.ApiToken);

                // 4. Send
                var response = await _httpClient.SendAsync(requestMessage);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("MailerSend Error: {StatusCode} - {Content}", response.StatusCode, errorContent);
                    return Result.Fail($"Email failed: {response.StatusCode}");
                }

                _logger.LogInformation("Email sent successfully to {Email} via MailerSend", mailRequest.Email);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception sending email via MailerSend");
                return Result.Fail(new Error(ex.Message));
            }
        }

        // Helper for Invoice Email (reuses the main method)
        public async Task<Result> SendInvoiceEmailAsync(string recipientEmail, string customerName, string invoiceNumber, decimal totalAmount, string message, List<string> cloudinaryUrls)
        {
            var emailBody = $@"
<div style='font-family:Arial,Helvetica,sans-serif; font-size:14px; color:#333; line-height:1.6;'>
    <h2 style='color:#007BFF;'>Xin chào {customerName},</h2>

    <p>{message}</p>

    <table style='margin:15px 0; border-collapse:collapse;'>
        <tr>
            <td style='padding:5px 10px; font-weight:bold;'>Mã hóa đơn:</td>
            <td style='padding:5px 10px; color:#000;'>{invoiceNumber}</td>
        </tr>
        <tr>
            <td style='padding:5px 10px; font-weight:bold;'>Tổng tiền:</td>
            <td style='padding:5px 10px; color:#D63384;'>{totalAmount:n0} VND</td>
        </tr>
    </table>

    <p>
        🧾 File hóa đơn (PDF & XML) đã được đính kèm trong email này.<br/>
        Bạn có thể tải xuống hoặc xem trực tuyến qua các liên kết bên dưới.
    </p>

    <ul>
        {string.Join("", cloudinaryUrls.Select(u => $"<li><a href='{u}'>{Path.GetFileName(u)}</a></li>"))}
    </ul>

    <p style='margin-top:20px;'>Trân trọng,<br/>
    <strong>Đội ngũ E-Invoice System</strong><br/>
    <span style='font-size:12px; color:#777;'>Đây là email tự động, vui lòng không trả lời.</span></p>
</div>";

            var request = new Application.DTOs.Mails.MailRequest
            {
                Email = recipientEmail,
                Subject = $"Invoice #{invoiceNumber}",
                EmailBody = emailBody,
                CloudinaryUrls = cloudinaryUrls
            };

            return await SendMailAsync(request);
        }

        public Task<Result> SendEmailCoreAsync(Invoice invoice, string subjectPrefix, string message)
        {
            throw new NotImplementedException();
        }

        public Task<Result> SendInvoiceEmailAsync(string recipientEmail, int invoiceId, string message)
        {
            throw new NotImplementedException();
        }

        public Task<Result> SendStatusUpdateNotificationAsync(int invoiceId, int newStatusId)
        {
            throw new NotImplementedException();
        }
    }
}