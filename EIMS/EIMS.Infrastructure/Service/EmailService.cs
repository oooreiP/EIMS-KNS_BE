using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Mails;
using EIMS.Domain.Entities;
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
        private readonly IUnitOfWork _uow;
        private readonly ILogger<EmailService> _logger;
        private readonly HttpClient _httpClient;

        public EmailService(IOptions<EmailSettings> options, ILogger<EmailService> logger, HttpClient httpClient, IUnitOfWork uow)
        {
            _settings = options.Value;
            _logger = logger;
            _httpClient = httpClient;
            _uow = uow;
        }

        public async Task<Result> SendMailAsync(MailRequest mailRequest)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_settings.Email);
                email.To.Add(MailboxAddress.Parse(mailRequest.Email));
                email.Subject = mailRequest.Subject;

                var builder = new BodyBuilder
                {
                    HtmlBody = mailRequest.EmailBody
                };
                if (mailRequest.CloudinaryUrls != null && mailRequest.CloudinaryUrls.Any())
                {
                    foreach (var url in mailRequest.CloudinaryUrls)
                    {
                        try
                        {
                            var fileName = Path.GetFileName(new Uri(url).LocalPath);
                            var fileBytes = await _httpClient.GetByteArrayAsync(url);
                            builder.Attachments.Add(fileName, fileBytes);
                            _logger.LogInformation("Fetched and attached file from Cloudinary: {FileName}", fileName);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ex, "Failed to fetch file from Cloudinary: {Url}", url);
                        }
                    }
                }
                email.Body = builder.ToMessageBody();

        //         using var smtp = new SmtpClient();
        //         await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
        //         await smtp.AuthenticateAsync(_settings.Email, _settings.Password);
        //         await smtp.SendAsync(email);
        //         await smtp.DisconnectAsync(true);

                _logger.LogInformation(" Invoice email sent successfully to {Recipient}", mailRequest.Email);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Failed to send email to {Recipient}", mailRequest.Email);
                return Result.Fail(new Error("Failed to send invoice email").CausedBy(ex));
            }
        }
        public async Task<Result> SendInvoiceEmailAsync(string recipientEmail, int invoiceId, string message)
        {
            var invoice = await _uow.InvoicesRepository.GetByIdAsync(invoiceId, includeProperties: "Customer");
            if (invoice == null) return Result.Fail("Invoice not found");

            // Gán tạm email vào object để hàm core dùng (trường hợp muốn gửi cho email khác email mặc định)
            if (invoice.Customer != null) invoice.Customer.ContactEmail = recipientEmail;

            return await SendEmailCoreAsync(invoice, "🔔 [Thông báo]", message);
        }
        public async Task<Result> SendEmailCoreAsync(Invoice invoice, string subjectPrefix, string message)
        {
            if (invoice.Customer == null || string.IsNullOrEmpty(invoice.Customer.ContactEmail))
                return Result.Fail("Customer email missing");
            var attachmentUrls = new List<string>();
            if (!string.IsNullOrEmpty(invoice.FilePath)) attachmentUrls.Add(invoice.FilePath);
            if (!string.IsNullOrEmpty(invoice.XMLPath)) attachmentUrls.Add(invoice.XMLPath); // Lưu ý tên biến XMLPath hay XmlPath
            string formattedAmount = invoice.TotalAmount.ToString("N0");

            var emailBody = $@"
        <div style='font-family:Arial,Helvetica,sans-serif; font-size:14px; color:#333; line-height:1.6; border: 1px solid #ddd; padding: 20px; max-width: 600px; margin: 0 auto;'>
            <h2 style='color:#007BFF;'>Xin chào {invoice.Customer.CustomerName ?? "Quý khách"},</h2>

            <p style='font-size: 16px;'>{message}</p>

            <div style='background: #f5f5f5; padding: 15px; border-radius: 5px; margin: 20px 0;'>
                <table style='width:100%; border-collapse:collapse;'>
                    <tr>
                        <td style='padding:5px 0; font-weight:bold;'>Mã hóa đơn:</td>
                        <td style='padding:5px 0;'>{invoice.InvoiceNumber}</td>
                    </tr>
                    <tr>
                        <td style='padding:5px 0; font-weight:bold;'>Ngày tạo:</td>
                        <td style='padding:5px 0;'>{invoice.CreatedAt:dd/MM/yyyy}</td>
                    </tr>
                    <tr>
                    <tr>
                        <td style='padding:5px 0; font-weight:bold;'>Ngày lập:</td>
                        <td style='padding:5px 0;'>{invoice.IssuedDate:dd/MM/yyyy}</td>
                    </tr>
                    <tr>
                        <td style='padding:5px 0; font-weight:bold;'>Tổng tiền:</td>
                        <td style='padding:5px 0; color:#D63384; font-weight:bold;'>{formattedAmount} VND</td>
                    </tr>
                </table>
            </div>

            <p>
                🧾 <strong>File đính kèm:</strong><br/>
                Bạn có thể tải xuống qua các liên kết bên dưới:
            </p>

            <ul>
                {string.Join("", attachmentUrls.Select(u => $"<li><a href='{u}' target='_blank'>Tải xuống {Path.GetFileName(u)}</a></li>"))}
            </ul>

            <p style='margin-top:20px; font-size: 13px; color: #777;'>
                Trân trọng,<br/><strong>Đội ngũ E-Invoice System</strong>
            </p>
        </div>";

            var mailRequest = new MailRequest
            {
                Email = invoice.Customer.ContactEmail,
                Subject = $"{subjectPrefix} Hóa đơn #{invoice.InvoiceNumber}", // VD: ✅ [Thành công] Hóa đơn #00123
                EmailBody = emailBody,
                CloudinaryUrls = attachmentUrls
            };

            return await SendMailAsync(mailRequest);
        }
        public async Task<Result> SendStatusUpdateNotificationAsync(int invoiceId, int newStatusId)
        {
            // 1. Lấy hóa đơn kèm thông tin khách hàng
            var invoice = await _uow.InvoicesRepository.GetByIdAsync(invoiceId, includeProperties: "Customer");

            if (invoice == null) return Result.Fail("Invoice not found");
            if (invoice.Customer == null || string.IsNullOrEmpty(invoice.Customer.ContactEmail))
                return Result.Ok(); 
            string subjectPrefix = "";
            string messageContent = "";
            bool shouldSend = true;

            switch (newStatusId)
            {
                case 1:
                    subjectPrefix = $"{invoice.InvoiceNumber} đã được khởi tạo";
                    messageContent = "Hóa đơn điện tử của quý khách đã được tạo và đang ở trạng thái nháp.";
                    break;
                case 6: 
                    subjectPrefix = "✅ [Thành công]";
                    messageContent = "Hóa đơn điện tử của quý khách đã được Cơ quan Thuế cấp mã và có giá trị pháp lý.";
                    break;

                case 9: // Cancelled (Đã hủy)
                    subjectPrefix = "❌ [Đã hủy]";
                    messageContent = "Thông báo: Hóa đơn điện tử này đã bị HỦY bỏ giá trị sử dụng.";
                    break;

                case 10: // Replaced (Bị thay thế)
                    subjectPrefix = "⚠️ [Bị thay thế]";
                    messageContent = "Thông báo: Hóa đơn này đã bị thay thế bởi một hóa đơn mới. Vui lòng không sử dụng hóa đơn này để kê khai thuế.";
                    break;

                case 11: // Adjusted (Đã điều chỉnh)
                    subjectPrefix = "📝 [Đã điều chỉnh]";
                    messageContent = "Thông báo: Hóa đơn này đã có thông tin điều chỉnh.";
                    break;

                case 7: // Rejected (CQT Từ chối - Nếu muốn báo khách)
                    subjectPrefix = "🚫 [Bị từ chối]";
                    messageContent = "Hóa đơn có sai sót và bị cơ quan thuế từ chối. Chúng tôi sẽ sớm liên hệ để xử lý.";
                    break;

                default:
                    shouldSend = false;
                    break;
            }

            if (!shouldSend) return Result.Ok();

            // 3. Tái sử dụng hàm gửi email core
            return await SendEmailCoreAsync(invoice, subjectPrefix, messageContent);
        }
    }
}
