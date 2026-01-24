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
        public Task<Result> Handle(SendInvoiceMinutesCommand request, CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                await ProcessMinutesInBackground(request);
            });

            return Task.FromResult(Result.Ok());
        }
        private async Task ProcessMinutesInBackground(SendInvoiceMinutesCommand request)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var minutesGenerator = scope.ServiceProvider.GetRequiredService<IMinutesGenerator>();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                var fileStorageService = scope.ServiceProvider.GetRequiredService<IFileStorageService>();
                var documentParser = scope.ServiceProvider.GetRequiredService<IDocumentParserService>();
                var pdfService = scope.ServiceProvider.GetRequiredService<IPdfService>();
                var xmlService = scope.ServiceProvider.GetRequiredService<IInvoiceXMLService>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<SendInvoiceMinutesCommandHandler>>();

                try
                {
                    logger.LogInformation($"Bắt đầu xử lý gửi biên bản cho Invoice Adjustment ID: {request.InvoiceId}");
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
                    string contentBefore = "";
                    string contentAfter = "";

                    if (request.ContentBefore != null && request.ContentAfter != null)
                    {
                        contentBefore = request.ContentBefore;
                        contentAfter = request.ContentAfter;
                    }
                    else
                    {
                        // Sử dụng StringBuilder để tối ưu hiệu năng nối chuỗi
                        var sbBefore = new StringBuilder();
                        var sbAfter = new StringBuilder();

                        // 1. So sánh thông tin chung (Customer)
                        string nameOld = original.Customer?.CustomerName?.Trim() ?? "";
                        string nameNew = adjustment.Customer?.CustomerName?.Trim() ?? "";
                        if (!string.Equals(nameOld, nameNew, StringComparison.OrdinalIgnoreCase))
                        {
                            sbBefore.AppendLine($"- Tên đơn vị: {original.Customer?.CustomerName}");
                            sbAfter.AppendLine($"- Tên đơn vị: {adjustment.Customer?.CustomerName}");
                        }

                        // 2. So sánh danh sách Hàng hóa (Products)
                        // Lấy danh sách item gốc để dễ tra cứu (dùng Dictionary hoặc List)
                        var originalItems = original.InvoiceItems?.ToList() ?? new List<InvoiceItem>();
                        var newItems = adjustment.InvoiceItems?.ToList() ?? new List<InvoiceItem>();

                        // Danh sách các ID của item gốc đã được so khớp (để tí nữa tìm các item bị xóa)
                        var matchedOriginalItemIds = new HashSet<int>();

                        foreach (var newItem in newItems)
                        {
                            InvoiceItem? matchedOldItem = null;

                            // ƯU TIÊN 1: Tìm theo Link (Trường hợp Điều chỉnh có lưu vết)
                            if (newItem.OriginalInvoiceItemID.HasValue)
                            {
                                matchedOldItem = originalItems.FirstOrDefault(x => x.InvoiceItemID == newItem.OriginalInvoiceItemID.Value);
                            }

                            // ƯU TIÊN 2: Tìm theo ProductID (Trường hợp Thay thế hoặc chưa link)
                            // Logic: Tìm item gốc có cùng ProductID mà chưa được khớp với item mới nào
                            if (matchedOldItem == null)
                            {
                                matchedOldItem = originalItems
                                    .FirstOrDefault(x => x.ProductID == newItem.ProductID && !matchedOriginalItemIds.Contains(x.InvoiceItemID));
                            }

                            if (matchedOldItem != null)
                            {
                                // Đánh dấu là đã khớp
                                matchedOriginalItemIds.Add(matchedOldItem.InvoiceItemID);

                                // So sánh chi tiết xem có gì khác không
                                bool isDiff = false;
                                string diffDetail = "";

                                if (matchedOldItem.Quantity != newItem.Quantity)
                                {
                                    isDiff = true;
                                    diffDetail += $"SL: {matchedOldItem.Quantity} -> {newItem.Quantity}; ";
                                }
                                if (matchedOldItem.UnitPrice != newItem.UnitPrice)
                                {
                                    isDiff = true;
                                    diffDetail += $"Giá: {matchedOldItem.UnitPrice:N0} -> {newItem.UnitPrice:N0}; ";
                                }
                                if (matchedOldItem.Amount != newItem.Amount)
                                {
                                    isDiff = true;
                                    diffDetail += $"Thành tiền: {matchedOldItem.Amount:N0} -> {newItem.Amount:N0}; ";
                                }

                                // Nếu có khác biệt thì ghi log
                                if (isDiff)
                                {
                                    string productName = newItem.Product?.Name ?? "Sản phẩm";
                                    sbBefore.AppendLine($"- {productName}: {GetItemString(matchedOldItem)}");
                                    sbAfter.AppendLine($"- {productName}: {GetItemString(newItem)} ({diffDetail.TrimEnd(' ', ';')})");
                                }
                            }
                            else
                            {
                                // Trường hợp: Item Mới này không có trong hóa đơn cũ (Hàng mới thêm vào)
                                string productName = newItem.Product?.Name ?? "Sản phẩm mới";
                                sbBefore.AppendLine($"- {productName}: (Không có)");
                                sbAfter.AppendLine($"- {productName}: {GetItemString(newItem)} (Thêm mới)");
                            }
                        }

                        // 3. Tìm các item có ở Gốc nhưng bị xóa ở Mới (Chỉ xảy ra ở Thay thế)
                        foreach (var oldItem in originalItems)
                        {
                            if (!matchedOriginalItemIds.Contains(oldItem.InvoiceItemID))
                            {
                                string productName = oldItem.Product?.Name ?? "Sản phẩm cũ";
                                sbBefore.AppendLine($"- {productName}: {GetItemString(oldItem)}");
                                sbAfter.AppendLine($"- {productName}: (Đã xóa)");
                            }
                        }
                        if (sbBefore.Length > 0 || sbAfter.Length > 0)
                        {
                            // Nối với dữ liệu cũ (nếu có)
                            contentBefore += sbBefore.ToString();
                            contentAfter += sbAfter.ToString();
                        }
                        if (string.IsNullOrEmpty(contentBefore))
                        {
                            contentBefore = "................................................";
                            contentAfter = "................................................";
                        }
                    }
                        byte[] minutesFileBytes;
                    string minutesFileName;
                    string defaultTemplateCode;

                    if (request.Type == MinutesType.Replacement)
                    {
                        minutesFileBytes = await minutesGenerator.GenerateReplacementMinutesAsync(original, request.Reason, contentBefore, contentAfter, adjustment.InvoiceNumber.ToString(), request.AgreementDate);
                        minutesFileName = $"BienBan_ThayThe_Cua_{original.InvoiceNumber}.docx";
                        defaultTemplateCode = "MINUTES_REPLACE";
                    }
                    else
                    {
                        minutesFileBytes = await minutesGenerator.GenerateAdjustmentMinutesAsync(original, request.Reason, contentBefore, contentAfter, adjustment.InvoiceNumber.ToString(), request.AgreementDate);
                        minutesFileName = $"BienBan_DieuChinh_{original.InvoiceNumber}_to_{adjustment.InvoiceNumber}.docx";
                        defaultTemplateCode = "MINUTES_ADJUST";
                    }
                    var certResult = await xmlService.GetCertificateAsync(adjustment.CompanyId ?? 1);

                    if (certResult.IsFailed)
                    {
                        logger.LogError($"Lỗi lấy chứng thư số: {certResult.Errors[0].Message}");
                        return;
                    }

                    var signingCert = certResult.Value;
                    //byte[] pdfBytes = await documentParser.ConvertDocxToPdfAsync(minutesFileBytes);
                    //byte[] signedPdfBytes = pdfService.SignPdfUsingSpire(pdfBytes, signingCert);
                    //string signedFileName = $"{minutesFileName}_Signed.pdf";
                    //var uploadResult = await fileStorageService.UploadFileAsync(new MemoryStream(signedPdfBytes), signedFileName, "minutes");
                    //string signedFileUrl = uploadResult.Value.Url;
                    EmailTemplate emailTemplate = null;

                    if (request.EmailTemplateId.HasValue)
                    {
                        emailTemplate = await uow.EmailTemplateRepository.GetByIdAsync(request.EmailTemplateId.Value);
                    }

                    if (emailTemplate == null)
                    {
                        emailTemplate = await uow.EmailTemplateRepository.GetAllQueryable()
                            .FirstOrDefaultAsync(x => x.TemplateCode == defaultTemplateCode && x.LanguageCode == "vi");
                    }

                    if (emailTemplate == null)
                    {
                        logger.LogError($"Không tìm thấy Email Template (Code: {defaultTemplateCode})");
                        return;
                    }
                    var attachmentList = new List<FileAttachment>();
                    //attachmentList.Add(new FileAttachment
                    //{
                    //    FileName = signedFileName,
                    //    FileContent = signedPdfBytes, 
                    //    FileUrl = signedFileUrl       
                    //});
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
                    if (request.IncludeXml && !string.IsNullOrEmpty(adjustment.XMLPath))
                    {
                        attachmentList.Add(new FileAttachment
                        {
                            FileUrl = adjustment.XMLPath,
                            FileName = GetFileNameFromUrl(adjustment.XMLPath)
                        });
                    }

                    attachmentHtmlList += "<br/><em style='color: #666; font-size: 12px;'>(File được đính kèm theo email)</em>";
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
                    string toEmail = !string.IsNullOrEmpty(request.RecipientEmail)
                                     ? request.RecipientEmail
                                     : adjustment.Customer.ContactEmail;

                    var mailRequest = new FEMailRequest
                    {
                        ToEmail = toEmail,
                        Subject = subject,
                        EmailBody = body,
                        AttachmentUrls = attachmentList, 
                        CcEmails = request.CcEmails,  
                        BccEmails = request.BccEmails  
                    };
                    var sendResult = await emailService.SendMailAsync(mailRequest);
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
        string GetItemString(InvoiceItem item)
        {
            return $"SL: {item.Quantity} x Giá: {item.UnitPrice:N0} = {item.Amount:N0}";
        }
    }
}
