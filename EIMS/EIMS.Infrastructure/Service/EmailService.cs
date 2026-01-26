using DocumentFormat.OpenXml.Wordprocessing;
using EIMS.Application.Commons.Helpers;
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
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using ContentType = MimeKit.ContentType;

namespace EIMS.Infrastructure.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<EmailService> _logger;
        private readonly HttpClient _httpClient;
        private readonly SendGridClient _client; 

        public EmailService(
            IOptions<EmailSettings> options, // Lưu ý: Đổi sang class chứa API Key
            ILogger<EmailService> logger,
            HttpClient httpClient,
            IUnitOfWork uow)
        {
            _settings = options.Value;
            _logger = logger;
            _httpClient = httpClient;
            _uow = uow;
            // Khởi tạo SendGrid Client
            _client = new SendGridClient(_settings.ApiToken);
        }

        public async Task<Result> SendMailAsync(FEMailRequest mailRequest)
        {
            try
            {
                // 1. Cấu hình người gửi/nhận
                var from = new EmailAddress(_settings.FromEmail, _settings.FromName);
                var to = new EmailAddress(mailRequest.ToEmail);
                var subject = mailRequest.Subject;
                var htmlContent = mailRequest.EmailBody;
                var plainTextContent = "Notification from EIMS"; 

                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                // Xử lý CC
                if (mailRequest.CcEmails != null && mailRequest.CcEmails.Any())
                {
                    foreach (var cc in mailRequest.CcEmails.Where(c => !string.IsNullOrWhiteSpace(c)))
                        msg.AddCc(new EmailAddress(cc));
                }

                // Xử lý BCC
                if (mailRequest.BccEmails != null && mailRequest.BccEmails.Any())
                {
                    foreach (var bcc in mailRequest.BccEmails.Where(c => !string.IsNullOrWhiteSpace(c)))
                        msg.AddBcc(new EmailAddress(bcc));
                }

                // 2. XỬ LÝ FILE ĐÍNH KÈM (Giữ logic Tối ưu song song của bạn)
                if (mailRequest.AttachmentUrls != null && mailRequest.AttachmentUrls.Any())
                {
                    // Lọc ra danh sách cần download từ URL
                    var downloadTasks = mailRequest.AttachmentUrls
                        .Where(f => !string.IsNullOrEmpty(f.FileUrl))
                        .Select(async file =>
                        {
                            try
                            {
                                var bytes = await _httpClient.GetByteArrayAsync(file.FileUrl);
                                return new { IsSuccess = true, FileName = file.FileName, Content = bytes };
                            }
                            catch (Exception ex)
                            {
                                _logger.LogWarning($"Lỗi tải file {file.FileUrl}: {ex.Message}");
                                return new { IsSuccess = false, FileName = "", Content = Array.Empty<byte>() };
                            }
                        });

                    // Chờ tải xong
                    var downloadedFiles = await Task.WhenAll(downloadTasks);

                    // Add file đã download vào SendGrid Message
                    foreach (var file in downloadedFiles.Where(x => x.IsSuccess))
                    {
                        var base64Content = Convert.ToBase64String(file.Content);
                        msg.AddAttachment(file.FileName, base64Content);
                    }

                    // Add file có sẵn byte content (nếu có)
                    foreach (var file in mailRequest.AttachmentUrls.Where(f => string.IsNullOrEmpty(f.FileUrl) && f.FileContent?.Length > 0))
                    {
                        var base64Content = Convert.ToBase64String(file.FileContent);
                        msg.AddAttachment(file.FileName, base64Content);
                    }
                }

                // 3. Gửi qua API (Không lo chặn port)
                var response = await _client.SendEmailAsync(msg);

                // Kiểm tra kết quả
                if (response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.OK)
                {
                    return Result.Ok();
                }

                var body = await response.Body.ReadAsStringAsync();
                _logger.LogError($"SendGrid Error: {response.StatusCode} - {body}");
                return Result.Fail($"Email failed: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi gửi email SendGrid");
                return Result.Fail(new Error("Lỗi gửi email SendGrid").CausedBy(ex));
            }
        }

        // TỐI ƯU 2: Xử lý cho MailRequest (Cloudinary) - Logic tương tự
       public async Task<Result> SendMailAsync(MailRequest mailRequest)
       {
            // Adapter chuyển đổi sang FEMailRequest để tái sử dụng hàm trên
            return await SendMailAsync(new FEMailRequest
            {
                ToEmail = mailRequest.Email,
                Subject = mailRequest.Subject,
                EmailBody = mailRequest.EmailBody,
                // Chuyển đổi CloudinaryUrls sang AttachmentUrls nếu cần
                AttachmentUrls = mailRequest.CloudinaryUrls?.Select(url => new FileAttachment 
                { 
                    FileUrl = url, 
                    FileName = Path.GetFileName(new Uri(url).LocalPath) 
                }).ToList()
            });
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
            string emailSubject = EmailHelper.ReplacePlaceholders(template.Subject, replacements);
            string emailBody = EmailHelper.ReplacePlaceholders(template.BodyContent, replacements);

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
                    subjectPrefix = $"{invoice.InvoiceSymbol}_{invoice.InvoiceNumber} đã được khởi tạo";
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
    }
}
