using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Mails;
using EIMS.Domain.Entities;
using EIMS.Domain.Enums;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Emails.Commands
{
    public class SendInvoiceMinutesCommandHandler : IRequestHandler<SendInvoiceMinutesCommand, Result>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public SendInvoiceMinutesCommandHandler(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        // 1. Handle chính: Trả về kết quả ngay lập tức
        public Task<Result> Handle(SendInvoiceMinutesCommand request, CancellationToken cancellationToken)
        {
            // Chạy ngầm để không block UI
            Task.Run(async () =>
            {
                await ProcessMinutesInBackground(request);
            });

            return Task.FromResult(Result.Ok());
        }

        // 2. Logic xử lý ngầm
        private async Task ProcessMinutesInBackground(SendInvoiceMinutesCommand request)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                // Resolve các service trong scope mới
                var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var minutesGenerator = scope.ServiceProvider.GetRequiredService<IMinutesGenerator>();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<SendInvoiceMinutesCommandHandler>>();

                try
                {
                    logger.LogInformation($"Bắt đầu xử lý gửi biên bản cho Invoice Adjustment ID: {request.InvoiceId}");

                    // --- BƯỚC A: LẤY DỮ LIỆU HÓA ĐƠN ---
                    // request.InvoiceId là của hóa đơn ĐIỀU CHỈNH/THAY THẾ
                    var adjustment = await uow.InvoicesRepository.GetAllQueryable()
                        .Include(x => x.InvoiceItems).ThenInclude(it => it.Product)
                        .Include(x => x.Customer)
                        .Include(x => x.Company)
                        .FirstOrDefaultAsync(x => x.InvoiceID == request.InvoiceId);

                    if (adjustment == null)
                    {
                        logger.LogError($"Không tìm thấy hóa đơn điều chỉnh với ID {request.InvoiceId}");
                        return;
                    }

                    // Lấy hóa đơn GỐC dựa trên OriginalInvoiceID của hóa đơn điều chỉnh
                    if (adjustment.OriginalInvoiceID == null)
                    {
                        logger.LogError($"Hóa đơn {request.InvoiceId} không có OriginalInvoiceID. Không thể tạo biên bản.");
                        return;
                    }

                    var original = await uow.InvoicesRepository.GetByIdAsync(adjustment.OriginalInvoiceID.Value, "Customer,Company,InvoiceItems.Product,Template.Serial.Prefix,Template.Serial.SerialStatus, Template.Serial.InvoiceType,InvoiceStatus");

                    if (original == null)
                    {
                        logger.LogError($"Không tìm thấy hóa đơn gốc ID {adjustment.OriginalInvoiceID}");
                        return;
                    }

                    // --- BƯỚC B: SO SÁNH NỘI DUNG (Content Before/After) ---
                    // Logic giữ nguyên, nhưng lưu ý biến: original (cũ) vs adjustment (mới)
                    string contentBefore = "";
                    string contentAfter = "";

                    if (request.ContentBefore != null && request.ContentAfter != null)
                    {
                        contentBefore = request.ContentBefore;
                        contentAfter = request.ContentAfter;
                    }
                    else
                    {
                        // Logic so sánh tự động (Tôi giữ gọn lại để tập trung vào logic mới)
                        // So sánh Tên
                        string nameOld = original.Customer?.CustomerName?.Trim() ?? "";
                        string nameNew = adjustment.Customer?.CustomerName?.Trim() ?? "";
                        if (!string.Equals(nameOld, nameNew, StringComparison.OrdinalIgnoreCase))
                        {
                            contentBefore += $"- Tên đơn vị: {original.Customer?.CustomerName}\n";
                            contentAfter += $"- Tên đơn vị: {adjustment.Customer?.CustomerName}\n";
                        }
                        // ... (Giữ nguyên các logic so sánh MST, Địa chỉ, Item của bạn ở đây) ...
                        // Nếu không có thay đổi gì thì để dòng kẻ
                        if (string.IsNullOrEmpty(contentBefore))
                        {
                            contentBefore = "................................................";
                            contentAfter = "................................................";
                        }
                    }

                    // --- BƯỚC C: SINH FILE BIÊN BẢN ---
                    byte[] minutesFileBytes;
                    string minutesFileName;
                    string defaultTemplateCode;

                    if (request.Type == MinutesType.Replacement)
                    {
                        minutesFileBytes = await minutesGenerator.GenerateReplacementMinutesAsync(original, request.Reason, contentBefore, contentAfter, adjustment.InvoiceNumber.ToString(), request.AgreementDate);
                        minutesFileName = $"BienBan_ThayThe_{original.InvoiceNumber}_to_{adjustment.InvoiceNumber}.docx";
                        defaultTemplateCode = "MINUTES_REPLACE";
                    }
                    else
                    {
                        minutesFileBytes = await minutesGenerator.GenerateAdjustmentMinutesAsync(original, request.Reason, contentBefore, contentAfter, adjustment.InvoiceNumber.ToString(), request.AgreementDate);
                        minutesFileName = $"BienBan_DieuChinh_{original.InvoiceNumber}_to_{adjustment.InvoiceNumber}.docx";
                        defaultTemplateCode = "MINUTES_ADJUST";
                    }

                    // --- BƯỚC D: LẤY EMAIL TEMPLATE ---
                    EmailTemplate emailTemplate = null;

                    if (request.EmailTemplateId.HasValue)
                    {
                        emailTemplate = await uow.EmailTemplateRepository.GetByIdAsync(request.EmailTemplateId.Value);
                    }

                    if (emailTemplate == null)
                    {
                        // Nếu không chọn hoặc không tìm thấy -> Lấy mặc định
                        emailTemplate = await uow.EmailTemplateRepository.GetAllQueryable()
                            .FirstOrDefaultAsync(x => x.TemplateCode == defaultTemplateCode && x.LanguageCode == "vi");
                    }

                    if (emailTemplate == null)
                    {
                        logger.LogError($"Không tìm thấy Email Template (Code: {defaultTemplateCode})");
                        return;
                    }
                    var attachmentList = new List<FileAttachment>();

                    // 1. File Biên bản (Luôn có)
                    attachmentList.Add(new FileAttachment
                    {
                        FileName = minutesFileName,
                        FileContent = minutesFileBytes
                    });
                    string GetFileNameFromUrl(string url)
                    {
                        try { return Path.GetFileName(new Uri(url).LocalPath); }
                        catch { return "document.pdf"; }
                    }
                    string attachmentHtmlList = $"<li style='margin-bottom: 5px;'>📎 <strong>{minutesFileName}</strong></li>";
                    if (request.IncludePdf && !string.IsNullOrEmpty(adjustment.FilePath))
                    {
                        attachmentList.Add(new FileAttachment
                        {
                            FileUrl = adjustment.FilePath,
                            FileName = GetFileNameFromUrl(adjustment.FilePath)
                        });
                    }

                    // 4.2. XML Hóa đơn
                    if (request.IncludeXml && !string.IsNullOrEmpty(adjustment.XMLPath))
                    {
                        attachmentList.Add(new FileAttachment
                        {
                            FileUrl = adjustment.XMLPath,
                            FileName = GetFileNameFromUrl(adjustment.XMLPath)
                        });
                    }

                    attachmentHtmlList += "<br/><em style='color: #666; font-size: 12px;'>(File được đính kèm theo email)</em>";


                    // --- BƯỚC F: REPLACE NỘI DUNG EMAIL ---
                    var replacements = new Dictionary<string, string>
                {
                    { "{{CustomerName}}", adjustment.Customer.CustomerName }, 
                    { "{{InvoiceNumber}}", original.InvoiceNumber.ToString() },
                    { "{{OriginalInvoiceNumber}}", original.InvoiceNumber.ToString() }, 
                    { "{{CreatedDate}}", DateTime.Now.ToString("dd/MM/yyyy") },
                    { "{{Reason}}", request.Reason },
                    { "{{AttachmentList}}", attachmentHtmlList },
                    { "{{IssuedDate}}", original.CreatedAt.ToString("dd/MM/yyyy") },
                    { "{{CustomMessage}}", request.CustomMessage ?? "" }
                };

                    string subject = ReplacePlaceholders(emailTemplate.Subject, replacements);
                    string body = ReplacePlaceholders(emailTemplate.BodyContent, replacements);

                    // --- BƯỚC G: CẤU HÌNH NGƯỜI NHẬN ---
                    // Ưu tiên email FE gửi lên, nếu trống thì lấy email khách hàng
                    string toEmail = !string.IsNullOrEmpty(request.RecipientEmail)
                                     ? request.RecipientEmail
                                     : adjustment.Customer.ContactEmail;

                    var mailRequest = new FEMailRequest
                    {
                        ToEmail = toEmail,
                        Subject = subject,
                        EmailBody = body,
                        AttachmentUrls = attachmentList, // Service gửi mail cần hỗ trợ List<FileAttachment>
                        CcEmails = request.CcEmails,   // Truyền List CC
                        BccEmails = request.BccEmails  // Truyền List BCC
                    };

                    // --- BƯỚC H: GỬI EMAIL ---
                    var sendResult = await emailService.SendMailAsync(mailRequest);

                    // --- BƯỚC I: GHI LOG LỊCH SỬ ---
                    if (sendResult.IsSuccess)
                    {
                        await uow.InvoiceHistoryRepository.CreateAsync(new InvoiceHistory
                        {
                            InvoiceID = adjustment.InvoiceID, 
                            ActionType = "Minutes Sent",
                            Date = DateTime.UtcNow
                        });
                        await uow.SaveChanges();
                        logger.LogInformation($"Đã gửi biên bản thành công cho Invoice {request.InvoiceId}");
                    }
                    else
                    {
                        logger.LogError($"Lỗi gửi email SendGrid: {sendResult.Errors.FirstOrDefault()?.Message}");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"CRITICAL ERROR khi xử lý gửi biên bản cho Invoice {request.InvoiceId}");
                }
            }
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
