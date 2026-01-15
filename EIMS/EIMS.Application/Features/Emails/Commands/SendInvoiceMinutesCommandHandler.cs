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
                        string nameOld = original.Customer?.CustomerName?.Trim() ?? "";
                        string nameNew = adjustment.Customer?.CustomerName?.Trim() ?? "";
                        if (!string.Equals(nameOld, nameNew, StringComparison.OrdinalIgnoreCase))
                        {
                            contentBefore += $"- Tên đơn vị: {original.Customer?.CustomerName}\n";
                            contentAfter += $"- Tên đơn vị: {adjustment.Customer?.CustomerName}\n";
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
                        minutesFileName = $"BienBan_ThayThe_{original.InvoiceNumber}_to_{adjustment.InvoiceNumber}.docx";
                        defaultTemplateCode = "MINUTES_REPLACE";
                    }
                    else
                    {
                        minutesFileBytes = await minutesGenerator.GenerateAdjustmentMinutesAsync(original, request.Reason, contentBefore, contentAfter, adjustment.InvoiceNumber.ToString(), request.AgreementDate);
                        minutesFileName = $"BienBan_DieuChinh_{original.InvoiceNumber}_to_{adjustment.InvoiceNumber}.docx";
                        defaultTemplateCode = "MINUTES_ADJUST";
                    }
                    var certResult = xmlService.GetCertificate(request.CertificateSerial);

                    if (certResult.IsFailed)
                    {
                        logger.LogError($"Lỗi lấy chứng thư số: {certResult.Errors[0].Message}");
                        return;
                    }

                    var signingCert = certResult.Value;
                    byte[] pdfBytes = await documentParser.ConvertDocxToPdfAsync(minutesFileBytes);
                    byte[] signedPdfBytes = pdfService.SignPdfUsingSpire(pdfBytes, signingCert);
                    string signedFileName = $"{minutesFileName}_Signed.pdf";
                    var uploadResult = await fileStorageService.UploadFileAsync(new MemoryStream(signedPdfBytes), signedFileName, "minutes");
                    string signedFileUrl = uploadResult.Value.Url;
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
                    attachmentList.Add(new FileAttachment
                    {
                        FileName = signedFileName,
                        FileContent = signedPdfBytes, 
                        FileUrl = signedFileUrl       
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
    }
}
