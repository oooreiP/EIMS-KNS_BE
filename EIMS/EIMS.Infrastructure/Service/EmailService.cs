using DocumentFormat.OpenXml.Wordprocessing;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Mails;
using EIMS.Application.Features.Emails.Commands;
using EIMS.Domain.Entities;
using FluentResults;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using ContentType = MimeKit.ContentType;

namespace EIMS.Infrastructure.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailSMTPSettings _settings;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<EmailService> _logger;
        private readonly HttpClient _httpClient;

        public EmailService(IOptions<EmailSMTPSettings> options, ILogger<EmailService> logger, HttpClient httpClient, IUnitOfWork uow)
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

                 using var smtp = new SmtpClient();
                 await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
                 await smtp.AuthenticateAsync(_settings.Email, _settings.Password);
                 await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                _logger.LogInformation(" Invoice email sent successfully to {Recipient}", mailRequest.Email);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Failed to send email to {Recipient}", mailRequest.Email);
                return Result.Fail(new Error("Failed to send invoice email").CausedBy(ex));
            }
        }

        public async Task<Result> SendMailAsync(FEMailRequest mailRequest)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_settings.Email);
                email.From.Add(new MailboxAddress(_settings.DisplayName, _settings.Email)); // Thêm tên hiển thị
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                email.Subject = mailRequest.Subject;

                // Xử lý CC
                if (mailRequest.CcEmails != null)
                {
                    foreach (var cc in mailRequest.CcEmails)
                        if (!string.IsNullOrWhiteSpace(cc)) email.Cc.Add(MailboxAddress.Parse(cc));
                }

                // Xử lý BCC
                if (mailRequest.BccEmails != null)
                {
                    foreach (var bcc in mailRequest.BccEmails)
                        if (!string.IsNullOrWhiteSpace(bcc)) email.Bcc.Add(MailboxAddress.Parse(bcc));
                }

                var builder = new BodyBuilder { HtmlBody = mailRequest.EmailBody };

                // Xử lý File đính kèm (Download từ URL và Attach vào Email)
                if (mailRequest.AttachmentUrls != null && mailRequest.AttachmentUrls.Any())
                {
                    foreach (var file in mailRequest.AttachmentUrls)
                    {
                        // CASE 1: Nếu là URL (File trên Cloud) -> Tải về rồi đính kèm
                        if (!string.IsNullOrEmpty(file.FileUrl))
                        {
                            try
                            {
                                var fileBytes = await _httpClient.GetByteArrayAsync(file.FileUrl);
                                builder.Attachments.Add(file.FileName, fileBytes);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogWarning($"Lỗi tải file {file.FileUrl}: {ex.Message}");
                            }
                        }
                        // CASE 2: Nếu là Byte Array (File sinh ra từ RAM - ví dụ Biên bản) -> Đính kèm luôn
                        else if (file.FileContent != null && file.FileContent.Length > 0)
                        {
                            builder.Attachments.Add(file.FileName, file.FileContent);
                        }
                    }
                }

                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_settings.Email, _settings.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi gửi email");
                return Result.Fail(new Error("Lỗi gửi email SMTP").CausedBy(ex));
            }
        }
        public async Task<Result> SendInvoiceEmailAsync(SendInvoiceEmailCommand request)
        {
            // 1. Lấy thông tin hóa đơn
            var invoice = await _uow.InvoicesRepository.GetByIdAsync(request.InvoiceId, includeProperties: "Customer");
            if (invoice == null) return Result.Fail("Invoice not found");
            EmailTemplate? template = null;

            if (request.EmailTemplateId.HasValue && request.EmailTemplateId.Value > 0)
            {
                template = await _uow.EmailTemplateRepository.GetByIdAsync(request.EmailTemplateId.Value);

                if (template == null)
                    return Result.Fail($"Không tìm thấy mẫu email với ID {request.EmailTemplateId}");
            }
            else
            {
                string lang = request.Language ?? "vi";
                // Giả sử bạn có hàm GetSingleAsync hoặc dùng GetQueryable
                template = await _uow.EmailTemplateRepository.GetAllQueryable()
                    .FirstOrDefaultAsync(x => x.TemplateCode == "INVOICE_SEND" && x.LanguageCode == lang && x.IsActive);

                if (template == null)
                    return Result.Fail($"Không tìm thấy mẫu mặc định 'INVOICE_SEND' cho ngôn ngữ '{lang}'");
            }
            // Validate active
            if (!template.IsActive)
                return Result.Fail("Mẫu email này đang bị khóa (Inactive).");
            string toEmail = !string.IsNullOrEmpty(request.RecipientEmail)
                        ? request.RecipientEmail
                        : invoice.Customer?.ContactEmail;
            if (string.IsNullOrEmpty(toEmail))
                return Result.Fail("Không tìm thấy email người nhận.");
            // 4. Chuẩn bị File đính kèm (Logic cũ)
            var finalAttachments = new List<FileAttachment>();
            string GetFileNameFromUrl(string url)
            {
                try { return Path.GetFileName(new Uri(url).LocalPath); }
                catch { return "document.pdf"; }
            }

            // 4.1. PDF Hóa đơn
            if (request.IncludePdf && !string.IsNullOrEmpty(invoice.FilePath))
            {
                finalAttachments.Add(new FileAttachment
                {
                    FileUrl = invoice.FilePath,
                    FileName = GetFileNameFromUrl(invoice.FilePath)
                });
            }

            // 4.2. XML Hóa đơn
            if (request.IncludeXml && !string.IsNullOrEmpty(invoice.XMLPath))
            {
                finalAttachments.Add(new FileAttachment
                {
                    FileUrl = invoice.XMLPath,
                    FileName = GetFileNameFromUrl(invoice.XMLPath)
                });
            }

            // 4.3. File bên ngoài (Từ request)
            if (request.ExternalAttachmentUrls != null)
            {
                foreach (var url in request.ExternalAttachmentUrls)
                {
                    finalAttachments.Add(new FileAttachment
                    {
                        FileUrl = url,
                        FileName = GetFileNameFromUrl(url)
                    });
                }
            }

            // 5. Chuẩn bị Dữ liệu để thay thế vào Template (Replacements)
            // Tạo chuỗi HTML danh sách file đính kèm để nhúng vào Body
            string displayLang = template.LanguageCode;
            string fileLinksHtml = string.Join("", finalAttachments.Select(f =>
                $"<li><a href='{f.FileUrl}' target='_blank'>{(displayLang == "en" ? "Download" : "Tải xuống")} {f.FileName}</a></li>"));

            // Dictionary chứa các biến sẽ thay thế
            var replacements = new Dictionary<string, string>
        {
            { "{{CustomerName}}", invoice.Customer?.CustomerName ?? (displayLang == "en" ? "Customer" : "Quý khách") },
            { "{{Message}}", request.CustomMessage ?? template.Name ?? "" }, 
            { "{{InvoiceNumber}}", invoice.InvoiceNumber.ToString() },
            { "{{IssuedDate}}", invoice.IssuedDate?.ToString("dd/MM/yyyy") ?? "N/A" },
            { "{{CreatedAt}}", invoice.CreatedAt.ToString("dd/MM/yyyy") },
            { "{{TotalAmount}}", invoice.TotalAmount.ToString("N0") },
            { "{{AttachmentList}}", fileLinksHtml }
        };

            // 6. Xử lý nội dung (Replace placeholders)
            string emailSubject = ReplacePlaceholders(template.Subject, replacements);
            string emailBody = ReplacePlaceholders(template.BodyContent, replacements);

            // 7. Tạo Mail Request & Gửi
            var mailRequest = new FEMailRequest
            {
                ToEmail = toEmail,
                CcEmails = request.CcEmails ?? new List<string>(),
                BccEmails = request.BccEmails ?? new List<string>(),
                Subject = emailSubject,
                EmailBody = emailBody,
                AttachmentUrls = finalAttachments
            };

            return await SendMailAsync(mailRequest);
        }
        public async Task<Result> SendStatusUpdateNotificationAsync(int invoiceId, int newStatusId)
        {
            // 1. Lấy hóa đơn kèm thông tin khách hàng
            var invoice = await _uow.InvoicesRepository.GetByIdAsync(invoiceId, includeProperties: "Customer");
            string templateCode = "INVOICE_SEND_2";
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
                case 12: 
                    subjectPrefix = "✅ [Thành công]";
                    messageContent = "Hóa đơn điện tử của quý khách đã được Cơ quan Thuế cấp mã.";
                    break;

                case 3: // Cancelled (Đã hủy)
                    subjectPrefix = "❌ [Đã hủy]";
                    messageContent = "Thông báo: Hóa đơn điện tử này đã bị HỦY bỏ giá trị sử dụng.";
                    break;

                case 5: // Replaced (Bị thay thế)
                    subjectPrefix = "⚠️ [Bị thay thế]";
                    messageContent = "Thông báo: Hóa đơn này đã bị thay thế bởi một hóa đơn mới. Vui lòng không sử dụng hóa đơn này để kê khai thuế.";
                    break;

                case 4: // Adjusted (Đã điều chỉnh)
                    subjectPrefix = "📝 [Đã điều chỉnh]";
                    messageContent = "Thông báo: Hóa đơn này đã có thông tin điều chỉnh.";
                    break;

                case 2: 
                    subjectPrefix = "✅ [Đã phát hành]";
                    messageContent = "Hóa đơn điện tử của quý khách đã được phát hành và có giá trị pháp lý.";
                    break;
                case 10: // Adjusted (Đã điều chỉnh)
                    subjectPrefix = "📝 [Đang điều chỉnh]";
                    messageContent = "Thông báo: Hóa đơn này đã có thông tin điều chỉnh, vui lòng kiểm tra hóa đơn nháp ở dưới.";
                    break;

                default:
                    shouldSend = false;
                    break;
            }

            if (!shouldSend) return Result.Ok();
            var template = await _uow.EmailTemplateRepository.GetAllQueryable()
                .FirstOrDefaultAsync(x => x.TemplateCode == templateCode && x.LanguageCode == "vi" && x.IsActive);
            int templateId = template?.EmailTemplateID ?? 1;
            var command = new SendInvoiceEmailCommand
            {
                InvoiceId = invoiceId,
                EmailTemplateId = templateId, 
                CustomMessage = messageContent, 
                RecipientEmail = null, 
                IncludePdf = true,
                IncludeXml = true,
                Language = "vi"
            };

            // 4. GỌI HÀM CHÍNH
            return await SendInvoiceEmailAsync(command);
        }
        private string ReplacePlaceholders(string templateText, Dictionary<string, string> replacements)
        {
            if (string.IsNullOrEmpty(templateText)) return "";

            foreach (var item in replacements)
            {
                // Thay thế {{Key}} bằng Value
                templateText = templateText.Replace(item.Key, item.Value);
            }
            return templateText;
        }
    }
}
