using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Mails;
using EIMS.Domain.Entities;
using EIMS.Domain.Enums;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Emails.Commands
{
    public class SendInvoiceMinutesCommandHandler : IRequestHandler<SendInvoiceMinutesCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        private readonly IEmailService _emailService;
        private readonly IMinutesGenerator _minutesGenerator; // Service sinh file Word

        public SendInvoiceMinutesCommandHandler(
            IUnitOfWork uow,
            IEmailService emailService,
            IMinutesGenerator minutesGenerator)
        {
            _uow = uow;
            _emailService = emailService;
            _minutesGenerator = minutesGenerator;
        }

        public async Task<Result> Handle(SendInvoiceMinutesCommand request, CancellationToken cancellationToken)
        {
            var invoice = await _uow.InvoicesRepository.GetByIdAsync(request.InvoiceId, "Customer,Company,InvoiceItems.Product,Template.Serial.Prefix,Template.Serial.SerialStatus, Template.Serial.InvoiceType,InvoiceStatus");            
            if (invoice == null) return Result.Fail("Invoice not found");
            if (invoice == null) return Result.Fail("Original invoice not found");
            if (invoice.InvoiceStatusID != 2 && invoice.InvoiceStatusID != 10 && invoice.InvoiceStatusID != 11)
            {
                return Result.Fail($"Hóa đơn đang ở trạng thái {invoice.InvoiceStatus.StatusName}, chỉ được gửi biên bản khi hóa đơn Đã phát hành (Status 2).");
            }
            int targetInvoiceType = (request.Type == MinutesType.Replacement) ? 3 : 2;
            var adjustment = await _uow.InvoicesRepository.GetAllQueryable()
            .Include(x => x.InvoiceItems)
            .Include(x => x.Customer)
            .Include(x => x.Company)
            .OrderByDescending(x => x.InvoiceID)
            .FirstOrDefaultAsync(x => x.OriginalInvoiceID == request.InvoiceId && x.InvoiceType == targetInvoiceType);
            string contentBefore = "";
            string contentAfter = "";
            byte[] fileBytes;
            string fileName;
            string templateCode;
            string adjustmentNumber = adjustment.InvoiceNumber.ToString();
            if (request.ContentBefore != null && request.ContentAfter != null)
            {
                contentBefore = request.ContentBefore;
                contentAfter = request.ContentAfter;
            }
            else if (adjustment != null && request.ContentBefore == null && request.ContentAfter == null)
            {
                // 1. So sánh Tên đơn vị (Quan trọng nhất)
                // Lưu ý: Nên Trim() và ToLower() để tránh trường hợp thừa khoảng trắng
                string nameOld = invoice.Customer?.CustomerName?.Trim() ?? "";
                string nameNew = adjustment.Customer?.CustomerName?.Trim() ?? "";
                if (!string.Equals(nameOld, nameNew, StringComparison.OrdinalIgnoreCase))
                {
                    contentBefore += $"- Tên đơn vị: {invoice.Customer?.CustomerName}\n";
                    contentAfter += $"- Tên đơn vị: {adjustment.Customer?.CustomerName}\n";
                }

                // 2. So sánh Mã số thuế (Sai cái này là phạt nặng)
                string taxOld = invoice.Customer?.TaxCode?.Trim() ?? "";
                string taxNew = adjustment.Customer?.TaxCode?.Trim() ?? "";

                if (taxOld != taxNew)
                {
                    contentBefore += $"- Mã số thuế: {invoice.Customer?.TaxCode}\n";
                    contentAfter += $"- Mã số thuế: {adjustment.Customer?.TaxCode}\n";
                }

                // 3. So sánh Địa chỉ
                string addrOld = invoice.Customer?.Address?.Trim() ?? "";
                string addrNew = adjustment.Customer?.Address?.Trim() ?? "";

                if (!string.Equals(addrOld, addrNew, StringComparison.OrdinalIgnoreCase))
                {
                    contentBefore += $"- Địa chỉ: {invoice.Customer?.Address}\n";
                    contentAfter += $"- Địa chỉ: {adjustment.Customer?.Address}\n";
                }

                // 4. So sánh Người mua hàng (Nếu có)
                string buyerOld = invoice.Customer?.ContactPerson?.Trim() ?? "";
                string buyerNew = adjustment.Customer?.ContactPerson?.Trim() ?? "";

                if (!string.Equals(buyerOld, buyerNew, StringComparison.OrdinalIgnoreCase))
                {
                    contentBefore += $"- Người mua hàng: {invoice.Customer?.ContactPerson}\n";
                    contentAfter += $"- Người mua hàng: {adjustment.Customer?.ContactPerson}\n";
                }
                var itemOld = invoice.InvoiceItems.FirstOrDefault();
                var itemNew = adjustment.InvoiceItems.FirstOrDefault();

                if (itemOld != null && itemNew != null)
                {
                    if (itemOld.Product.Name != itemNew.Product.Name)
                    {
                        contentBefore += $"- Tên hàng: {itemOld.Product.Name}\n";
                        contentAfter += $"- Tên hàng: {itemNew.Product.Name}\n";
                    }
                    if (itemOld.Product.Unit != itemNew.Product.Unit)
                    {
                        contentBefore += $"- ĐVT: {itemOld.Product.Unit}\n";
                        contentAfter += $"- ĐVT: {itemNew.Product.Unit}\n";
                    }
                    if (itemOld.Amount != itemNew.Amount)
                    {
                        contentBefore += $"- Thành tiền: {itemOld.Amount:N0}\n";
                        contentAfter += $"- Thành tiền: {itemNew.Amount:N0}\n";
                    }
                }
            }
            else
            {
                contentBefore = "................................................";
                contentAfter = "................................................";
            }
            if (request.Type == MinutesType.Replacement)
            {
                fileBytes = await _minutesGenerator.GenerateReplacementMinutesAsync(invoice, request.Reason, contentBefore, contentAfter, adjustmentNumber, request.AgreementDate);
                fileName = $"BienBan_ThayThe_{invoice.InvoiceNumber}.docx"; // Hoặc .pdf
                templateCode = "MINUTES_REPLACE";
            }
            else
            {
                fileBytes = await _minutesGenerator.GenerateAdjustmentMinutesAsync(invoice, request.Reason, contentBefore, contentAfter, adjustmentNumber, request.AgreementDate);
                fileName = $"BienBan_DieuChinh_{invoice.InvoiceNumber}.docx";
                templateCode = "MINUTES_ADJUST";
            }
            var emailTemplate = await _uow.EmailTemplateRepository.GetAllQueryable()
                .FirstOrDefaultAsync(x => x.TemplateCode == templateCode && x.LanguageCode == "vi");
            string attachmentHtml = $@"
    <li style='margin-bottom: 5px;'>
        📎 <strong>{fileName}</strong> <br/>
        <em style='color: #666; font-size: 12px;'>(File này được đính kèm theo email, vui lòng kiểm tra mục Attachments)</em>
    </li>";
            if (emailTemplate == null) return Result.Fail($"Chưa cấu hình mẫu email {templateCode}");

            // 5. GỬI EMAIL (Tận dụng hàm EmailService nhưng cần chỉnh sửa chút để hỗ trợ Attachment dạng Byte[])
            // Do hàm SendInvoiceEmailAsync hiện tại chỉ nhận URL file, ta cần gọi hàm core SMTP trực tiếp hoặc nâng cấp Service.
            // Ở đây tôi giả lập việc gọi hàm SMTP trực tiếp để đính kèm file Byte[] vừa sinh ra.

            var replacements = new Dictionary<string, string>
        {
            { "{{CustomerName}}", invoice.Customer.CustomerName },
            { "{{InvoiceNumber}}", invoice.InvoiceNumber.ToString() },
            { "{{CreatedDate}}", invoice.CreatedAt.ToString("dd/MM/yyyy") },
            { "{{IssuedDate}}", invoice.CreatedAt.ToString("dd/MM/yyyy") },
            { "{{Reason}}", request.Reason },
            { "{{AttachmentList}}", attachmentHtml }
        };

            string subject = ReplacePlaceholders(emailTemplate.Subject, replacements);
            string body = ReplacePlaceholders(emailTemplate.BodyContent, replacements);

            var mailRequest = new FEMailRequest
            {
                ToEmail = invoice.Customer.ContactEmail,
                Subject = subject,
                EmailBody = body,
                AttachmentUrls = new List<FileAttachment>
                {
                    new FileAttachment
                    {
                        FileName = fileName,
                        FileContent = fileBytes, 
                        FileUrl = null 
                    }
                }
            };

            var sendResult = await _emailService.SendMailAsync(mailRequest);

            // 6. GHI LỊCH SỬ
            if (sendResult.IsSuccess)
            {
                await _uow.InvoiceHistoryRepository.CreateAsync(new InvoiceHistory
                {
                    InvoiceID = invoice.InvoiceID,
                    ActionType = "Minutes Sent",
                    Date = DateTime.UtcNow
                });
                await _uow.SaveChanges();
            }
            return sendResult;
        }

        private string ReplacePlaceholders(string text, Dictionary<string, string> replacements)
        {
            foreach (var item in replacements) text = text.Replace(item.Key, item.Value);
            return text;
        }
    }
}
