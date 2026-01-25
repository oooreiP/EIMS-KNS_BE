using EIMS.Application.Commons.Helpers;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Mails;
using EIMS.Application.Features.Emails.Commands;
using EIMS.Domain.Entities;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;

namespace EIMS.Infrastructure.Service
{
    public class SendGridService : IEmailSenderService
    {
        private readonly EmailSettings _settings; // Your SendGrid Settings
        private readonly ILogger<SendGridService> _logger;
        private readonly SendGridClient _client;
        private readonly IUnitOfWork _uow;
        private readonly HttpClient _httpClient;

        public SendGridService(
            IOptions<EmailSettings> settings,
            ILogger<SendGridService> logger,
            IUnitOfWork uow,
            HttpClient httpClient)
        {
            _settings = settings.Value;
            _logger = logger;
            _uow = uow;
            _client = new SendGridClient(_settings.ApiToken);
            _httpClient = httpClient;
        }

        // =================================================================
        // 1. CORE SENDING METHOD (The "Engine")
        // =================================================================
        public async Task<Result> SendMailAsync(FEMailRequest mailRequest)
        {
            try
            {
                var from = new EmailAddress(_settings.FromEmail, _settings.FromName);
                var to = new EmailAddress(mailRequest.ToEmail);

                // Create the message
                var msg = MailHelper.CreateSingleEmail(
                    from,
                    to,
                    mailRequest.Subject,
                    "Notification from EIMS", // Plain text fallback
                    mailRequest.EmailBody // HTML Content
                );

                // Handle CC
                if (mailRequest.CcEmails != null && mailRequest.CcEmails.Any())
                {
                    foreach (var cc in mailRequest.CcEmails.Where(c => !string.IsNullOrWhiteSpace(c)))
                    {
                        msg.AddCc(new EmailAddress(cc));
                    }
                }

                // Handle BCC
                if (mailRequest.BccEmails != null && mailRequest.BccEmails.Any())
                {
                    foreach (var bcc in mailRequest.BccEmails.Where(c => !string.IsNullOrWhiteSpace(c)))
                    {
                        msg.AddBcc(new EmailAddress(bcc));
                    }
                }

                // === ATTACHMENT HANDLING (Optimized for SendGrid) ===
                if (mailRequest.AttachmentUrls != null && mailRequest.AttachmentUrls.Any())
                {
                    // 1. Download files from URL (Cloudinary/S3)
                    var downloadTasks = mailRequest.AttachmentUrls
                        .Where(f => !string.IsNullOrEmpty(f.FileUrl))
                        .Select(async file =>
                        {
                            try
                            {
                                var bytes = await _httpClient.GetByteArrayAsync(file.FileUrl);
                                return new { Success = true, FileName = file.FileName, Content = bytes };
                            }
                            catch (Exception ex)
                            {
                                _logger.LogWarning($"Error downloading file {file.FileUrl}: {ex.Message}");
                                return new { Success = false, FileName = "", Content = Array.Empty<byte>() };
                            }
                        });

                    var downloadedFiles = await Task.WhenAll(downloadTasks);

                    // 2. Add Downloaded Files to SendGrid Message
                    foreach (var file in downloadedFiles.Where(x => x.Success))
                    {
                        var base64Content = Convert.ToBase64String(file.Content);
                        msg.AddAttachment(file.FileName, base64Content);
                    }

                    // 3. Add Direct Content Files (if any exist in request)
                    foreach (var file in mailRequest.AttachmentUrls.Where(f => string.IsNullOrEmpty(f.FileUrl) && f.FileContent?.Length > 0))
                    {
                        var base64Content = Convert.ToBase64String(file.FileContent);
                        msg.AddAttachment(file.FileName, base64Content);
                    }
                }

                // === SEND ===
                var response = await _client.SendEmailAsync(msg);

                // SendGrid returns 202 (Accepted) or 200 (OK) on success
                if (response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.OK)
                {
                    _logger.LogInformation($"SendGrid: Email sent successfully to {mailRequest.ToEmail}");
                    return Result.Ok();
                }

                // Error Handling
                var body = await response.Body.ReadAsStringAsync();
                _logger.LogError($"SendGrid Failed: {response.StatusCode} - {body}");
                return Result.Fail($"SendGrid Error: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in SendGridService");
                return Result.Fail(new Error("Failed to send email via SendGrid").CausedBy(ex));
            }
        }

        // =================================================================
        // 2. INVOICE LOGIC (Ported from Old Service)
        // =================================================================
        public async Task<Result> SendInvoiceEmailAsync(SendInvoiceEmailCommand request)
        {
            // 1. Get Invoice Data
            var invoice = await _uow.InvoicesRepository.GetAllQueryable()
                    .Include(x => x.Customer)
                    .Include(x => x.Template).ThenInclude(t => t.Serial).ThenInclude(s => s.SerialStatus)
                    .Include(x => x.Template).ThenInclude(t => t.Serial).ThenInclude(s => s.InvoiceType)
                    .FirstOrDefaultAsync(x => x.InvoiceID == request.InvoiceId);
            if (invoice == null) return Result.Fail("Invoice not found");
            string serialString = "N/A";
            if (invoice.Template?.Serial != null)
            {
                var s = invoice.Template.Serial;
                // Logic: Prefix + StatusSymbol + Year + TypeSymbol + Tail
                // Check for nulls on sub-properties to be safe
                serialString = $"{s.PrefixID}" +
                               $"{s.SerialStatus?.Symbol}" +
                               $"{s.Year}" +
                               $"{s.InvoiceType?.Symbol}" +
                               $"{s.Tail}";
            }
            EmailTemplate? template = null;

            // 2. Get Template (Specific or Default)
            if (request.EmailTemplateId.HasValue && request.EmailTemplateId.Value > 0)
            {
                template = await _uow.EmailTemplateRepository.GetByIdAsync(request.EmailTemplateId.Value);
                if (template == null) return Result.Fail($"Template ID {request.EmailTemplateId} not found");
            }
            else
            {
                string lang = request.Language ?? "vi";
                template = await _uow.EmailTemplateRepository.GetAllQueryable()
                    .FirstOrDefaultAsync(x => x.TemplateCode == "INVOICE_SEND" && x.LanguageCode == lang && x.IsActive);

                if (template == null) return Result.Fail($"Default template 'INVOICE_SEND' for '{lang}' not found");
            }

            if (!template.IsActive) return Result.Fail("Email template is inactive.");

            // 3. Determine Recipient
            string toEmail = !string.IsNullOrEmpty(request.RecipientEmail)
                            ? request.RecipientEmail
                            : invoice.Customer?.ContactEmail;

            if (string.IsNullOrEmpty(toEmail)) return Result.Fail("No recipient email found.");

            // 4. Prepare Attachments
            var finalAttachments = new List<FileAttachment>();
            string GetFileNameFromUrl(string url)
            {
                try { return Path.GetFileName(new Uri(url).LocalPath); }
                catch { return "document.pdf"; }
            }

            if (request.IncludePdf && !string.IsNullOrEmpty(invoice.FilePath))
            {
                finalAttachments.Add(new FileAttachment { FileUrl = invoice.FilePath, FileName = GetFileNameFromUrl(invoice.FilePath) });
            }
            if (request.IncludeXml && !string.IsNullOrEmpty(invoice.XMLPath))
            {
                finalAttachments.Add(new FileAttachment { FileUrl = invoice.XMLPath, FileName = GetFileNameFromUrl(invoice.XMLPath) });
            }
            if (request.ExternalAttachmentUrls != null)
            {
                foreach (var url in request.ExternalAttachmentUrls)
                {
                    finalAttachments.Add(new FileAttachment { FileUrl = url, FileName = GetFileNameFromUrl(url) });
                }
            }

            // 5. Prepare Replacements
            string displayLang = template.LanguageCode;
            string fileLinksHtml = string.Join("", finalAttachments.Select(f =>
                $"<li><a href='{f.FileUrl}' target='_blank'>{(displayLang == "en" ? "Download" : "Tải xuống")} {f.FileName}</a></li>"));

            var replacements = new Dictionary<string, string>
            {
                { "{{CustomerName}}", invoice.Customer?.CustomerName ?? (displayLang == "en" ? "Customer" : "Quý khách") },
                { "{{Message}}", request.CustomMessage ?? template.Name ?? "" },
                { "{{InvoiceNumber}}", (invoice.InvoiceNumber ?? 0).ToString() }, // Handle nullable int safely
                { "{{IssuedDate}}", invoice.IssuedDate?.ToString("dd/MM/yyyy") ?? "N/A" },
                { "{{CreatedAt}}", invoice.CreatedAt.ToString("dd/MM/yyyy") },
                { "{{TotalAmount}}", invoice.TotalAmount.ToString("N0") },
                { "{{AttachmentList}}", fileLinksHtml },
                { "{{LookupCode}}", invoice.LookupCode ?? "" },
                { "{{LookupUrl}}", $"https://tracuu-knsinvoice.id.vn/?code={invoice.LookupCode ?? string.Empty}" },
                { "{{Serial}}", serialString }
            };

            // 6. Replace Placeholders
            string emailSubject = EmailHelper.ReplacePlaceholders(template.Subject, replacements);
            string emailBody = EmailHelper.ReplacePlaceholders(template.BodyContent, replacements);

            // 7. Construct Request & Send
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

        // =================================================================
        // 3. STATUS UPDATE LOGIC (Ported from Old Service)
        // =================================================================
        public async Task<Result> SendStatusUpdateNotificationAsync(int invoiceId, int newStatusId)
        {
            var invoice = await _uow.InvoicesRepository.GetByIdAsync(invoiceId, includeProperties: "Customer");
            string templateCode = "INVOICE_SEND_2"; // Or whatever logic you use for notification templates

            if (invoice == null) return Result.Fail("Invoice not found");
            if (invoice.Customer == null || string.IsNullOrEmpty(invoice.Customer.ContactEmail))
                return Result.Ok(); // No email to send to

            string messageContent = "";
            bool shouldSend = true;

            switch (newStatusId)
            {
                case 1: // Draft
                    messageContent = "Hóa đơn điện tử của quý khách đã được khởi tạo và đang ở trạng thái nháp.";
                    break;
                case 12: // CQT Approved
                    messageContent = "Hóa đơn điện tử của quý khách đã được Cơ quan Thuế cấp mã.";
                    break;
                case 3: // Cancelled
                    messageContent = "Thông báo: Hóa đơn điện tử này đã bị HỦY bỏ giá trị sử dụng.";
                    break;
                case 5: // Replaced
                    messageContent = "Thông báo: Hóa đơn này đã bị thay thế bởi một hóa đơn mới.";
                    break;
                case 4: // Adjusted
                    messageContent = "Thông báo: Hóa đơn này đã có thông tin điều chỉnh.";
                    break;
                case 2: // Published/Issued
                    messageContent = $@"<h3>Hóa đơn điện tử đã được phát hành.</h3>";
                    break;
                case 10: // Adjusting
                    messageContent = "Thông báo: Hóa đơn này đã có thông tin điều chỉnh.";
                    break;
                default:
                    shouldSend = false;
                    break;
            }

            if (!shouldSend) return Result.Ok();

            // Find Template for Notification
            var template = await _uow.EmailTemplateRepository.GetAllQueryable()
                .FirstOrDefaultAsync(x => x.TemplateCode == templateCode && x.LanguageCode == "vi" && x.IsActive);

            int templateId = template?.EmailTemplateID ?? 0;

            // Reuse the main logic
            var command = new SendInvoiceEmailCommand
            {
                InvoiceId = invoiceId,
                EmailTemplateId = templateId > 0 ? templateId : (int?)null,
                CustomMessage = messageContent,
                RecipientEmail = null, // Will fetch from customer
                IncludePdf = true,
                IncludeXml = true,
                Language = "vi"
            };

            return await SendInvoiceEmailAsync(command);
        }

        // =================================================================
        // 4. HELPERS
        // =================================================================

        public async Task<Result> SendMailAsync(MailRequest request)
        {
            var feRequest = new FEMailRequest
            {
                ToEmail = request.Email,
                Subject = request.Subject,
                EmailBody = request.EmailBody,

                // Map Cloudinary URLs (List<string>) to Attachment objects (if any)
                AttachmentUrls = request.CloudinaryUrls?.Select(url => new FileAttachment
                {
                    FileUrl = url,
                    FileName = "attachment" // Default name since we don't have one in MailRequest
                }).ToList()
            };

            // Call the main engine
            return await SendMailAsync(feRequest);
        }

        public Task<Result> SendEmailCoreAsync(Invoice invoice, string subjectPrefix, string message)
        {
            throw new NotImplementedException();
        }

        public Task<Result> SendInvoiceEmailAsync(string recipientEmail, int invoiceId, string message)
        {
            throw new NotImplementedException();
        }
        public async Task<Result> SendEmailAsync(FEMailRequest mailRequest)
        {
            return await SendMailAsync(new MailRequest
            {
                Email = mailRequest.ToEmail,
                Subject = mailRequest.Subject,
                EmailBody = mailRequest.EmailBody
            });
        }
    }
}