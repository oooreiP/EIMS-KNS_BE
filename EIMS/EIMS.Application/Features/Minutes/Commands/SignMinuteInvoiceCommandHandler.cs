using EIMS.Application.Commons.Helpers;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Mails;
using EIMS.Domain.Entities;
using EIMS.Domain.Enums;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Minutes.Commands
{
    public class SignMinuteInvoiceCommandHandler : IRequestHandler<SignMinuteInvoiceCommand, Result<string>>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public SignMinuteInvoiceCommandHandler(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task<Result<string>> Handle(
            SignMinuteInvoiceCommand request,
            CancellationToken cancellationToken)
        {
            Task.Run(() => ProcessInBackground(request));
            return Task.FromResult(Result.Ok("Biên bản đang được ký nền."));
        }

        private async Task ProcessInBackground(SignMinuteInvoiceCommand request)
        {
            using var scope = _scopeFactory.CreateScope();

            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var pdfService = scope.ServiceProvider.GetRequiredService<IPdfService>();
            var xmlService = scope.ServiceProvider.GetRequiredService<IInvoiceXMLService>();
            var fileStorage = scope.ServiceProvider.GetRequiredService<IFileStorageService>();
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
            var currentUser = scope.ServiceProvider.GetRequiredService<ICurrentUserService>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SignMinuteInvoiceCommandHandler>>();

            try
            {
                var userId = int.Parse(currentUser.UserId);
                var minute = await uow.MinuteInvoiceRepository
        .GetByIdAsync(request.MinuteInvoiceId, "Invoice.Customer");
                if (minute == null)
                    return;

                if (minute.IsSellerSigned)
                    return;
                byte[] pdfBytes;
                try
                {
                    if (minute.FilePath.StartsWith("http"))
                    {
                        // Gọi Service đã tách ra
                        pdfBytes = await pdfService.DownloadFileBytesAsync(minute.FilePath);
                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    return;
                }
                var certResult = await xmlService.GetCertificateAsync(1); // ID công ty = 1
                if (certResult.IsFailed)
                    return;

                var signingCert = certResult.Value;
                var fontPath = Path.Combine(
                    request.RootPath,
                    "Fonts",
                    "arial.ttf"
                );
                // Ký số vào file (Kết quả là byte[] đã ký)
                byte[] signedPdfBytes;
                try
                {
                    signedPdfBytes = pdfService.SignPdfAtText(pdfBytes, signingCert, request.SearchText, fontPath);
                }
                catch (Exception ex)
                {
                    return;
                }

                // BƯỚC 4: Upload file ĐÃ KÝ lên lại Cloudinary
                // Tạo tên file mới để tránh cache hoặc ghi đè (thêm suffix _Signed)
                string newFileName = Path.GetFileNameWithoutExtension(minute.FilePath) + "_Signed_A.pdf";

                // Convert byte[] sang IFormFile bằng helper class ở trên
                IFormFile formFile = new MemoryFormFile(signedPdfBytes, newFileName);

                var uploadResult = await fileStorage.UploadFileAsync(formFile);
                if (uploadResult.IsFailed)
                    return;
                string defaultTemplateCode;
                if (minute.MinutesType == MinutesType.Replacement)
                {
                    defaultTemplateCode = "MINUTES_REPLACE";
                }
                else
                {
                    defaultTemplateCode = "MINUTES_ADJUST";
                }
                var emailTemplate = await uow.EmailTemplateRepository.GetAllQueryable()
                                .FirstOrDefaultAsync(x => x.TemplateCode == defaultTemplateCode && x.LanguageCode == "vi");
                var attachmentList = new List<FileAttachment>();
                string GetFileNameFromUrl(string url)
                {
                    try { return Path.GetFileName(new Uri(url).LocalPath); }
                    catch { return "document.pdf"; }
                }
                string attachmentHtmlList = $"<li style='margin-bottom: 5px;'>📎 <strong>{GetFileNameFromUrl(minute.FilePath)}</strong></li>";
                attachmentList.Add(new FileAttachment
                {
                    FileUrl = minute.FilePath,
                    FileName = GetFileNameFromUrl(minute.FilePath)
                });
                attachmentHtmlList += "<br/><em style='color: #666; font-size: 12px;'>(File được đính kèm theo email)</em>";
                var replacements = new Dictionary<string, string>
        {
            { "{{CustomerName}}", minute.Invoice.InvoiceCustomerName },
            { "{{InvoiceNumber}}", minute.Invoice.InvoiceNumber.ToString() },
            { "{{CreatedDate}}", DateTime.Now.ToString("dd/MM/yyyy") },
            { "{{Reason}}", minute.Description },
            { "{{AttachmentList}}", attachmentHtmlList },
            { "{{IssuedDate}}", minute.Invoice.IssuedDate.Value.ToString("dd/MM/yyyy") },
        };

                string subject = ReplacePlaceholders(emailTemplate.Subject, replacements);
                string body = ReplacePlaceholders(emailTemplate.BodyContent, replacements);
                string toEmail = minute.Invoice.Customer.ContactEmail;

                var mailRequest = new FEMailRequest
                {
                    ToEmail = toEmail,
                    Subject = subject,
                    EmailBody = body,
                    AttachmentUrls = attachmentList
                };
                var sendResult = await emailService.SendMailAsync(mailRequest);
                minute.FilePath = uploadResult.Value.Url;
                minute.IsSellerSigned = true;
                minute.SellerSignedAt = DateTime.UtcNow;
                if (minute.IsBuyerSigned)
                {
                    minute.Status = EMinuteStatus.Complete;
                }
                else
                {
                    minute.Status = EMinuteStatus.Sent;
                }

                await uow.MinuteInvoiceRepository.UpdateAsync(minute); // Nếu dùng EF Core tracking thì dòng này có thể ko cần
                await uow.SaveChanges();

                return;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Lỗi ký biên bản phút");
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
