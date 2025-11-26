using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Mails;
using FluentResults;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using ContentType = MimeKit.ContentType;

namespace EIMS.Infrastructure.Service
{
    public class EmailService
    {
        private readonly EmailSettings _settings;
        private readonly ILogger<EmailService> _logger;
        private readonly HttpClient _httpClient;

        public EmailService(IOptions<EmailSettings> options, ILogger<EmailService> logger, HttpClient httpClient)
        {
            _settings = options.Value;
            _logger = logger;
            _httpClient = httpClient;
        }

        // ✅ Gửi mail chung (hỗ trợ đính kèm file IFormFile hoặc byte[])
        // public async Task<Result> SendMailAsync(MailRequest mailRequest)
        // {
        //     try
        //     {
        //         var email = new MimeMessage();
        //         email.Sender = MailboxAddress.Parse(_settings.Email);
        //         email.To.Add(MailboxAddress.Parse(mailRequest.Email));
        //         email.Subject = mailRequest.Subject;

        //         var builder = new BodyBuilder
        //         {
        //             HtmlBody = mailRequest.EmailBody
        //         };

        //         // Tự động tải file từ Cloudinary nếu có URL
        //         if (mailRequest.CloudinaryUrls != null && mailRequest.CloudinaryUrls.Any())
        //         {
        //             foreach (var url in mailRequest.CloudinaryUrls)
        //             {
        //                 try
        //                 {
        //                     var fileName = Path.GetFileName(new Uri(url).LocalPath);
        //                     var fileBytes = await _httpClient.GetByteArrayAsync(url);
        //                     builder.Attachments.Add(fileName, fileBytes);
        //                     _logger.LogInformation("Fetched and attached file from Cloudinary: {FileName}", fileName);
        //                 }
        //                 catch (Exception ex)
        //                 {
        //                     _logger.LogWarning(ex, "Failed to fetch file from Cloudinary: {Url}", url);
        //                 }
        //             }
        //         }
        //         email.Body = builder.ToMessageBody();

        //         using var smtp = new SmtpClient();
        //         await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
        //         await smtp.AuthenticateAsync(_settings.Email, _settings.Password);
        //         await smtp.SendAsync(email);
        //         await smtp.DisconnectAsync(true);

        //         _logger.LogInformation(" Invoice email sent successfully to {Recipient}", mailRequest.Email);
        //         return Result.Ok();
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, " Failed to send email to {Recipient}", mailRequest.Email);
        //         return Result.Fail(new Error("Failed to send invoice email").CausedBy(ex));
        //     }
        // }

        // ✅ Hàm gửi email hóa đơn (sử dụng Cloudinary URLs)
        public async Task<Result> SendInvoiceEmailAsync(
            string recipientEmail,
            string customerName,
            string invoiceNumber,
            decimal totalAmount,
            string message,
            List<string> cloudinaryUrls)
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

            var mailRequest = new MailRequest
            {
                Email = recipientEmail,
                Subject = $"[Hóa đơn điện tử] #{invoiceNumber}",
                EmailBody = emailBody,
                CloudinaryUrls = cloudinaryUrls
            };

            return await SendMailAsync(mailRequest);
        }

        public Task<Result> SendMailAsync(MailRequest mailRequest)
        {
            throw new NotImplementedException();
        }
    }
}
