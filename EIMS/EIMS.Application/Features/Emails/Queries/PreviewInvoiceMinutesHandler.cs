using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs;
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

namespace EIMS.Application.Features.Emails.Queries
{
    public class PreviewInvoiceMinutesHandler : IRequestHandler<PreviewInvoiceMinutesQuery, Result<FileAttachment>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMinutesGenerator _minutesGenerator;

        public PreviewInvoiceMinutesHandler(IUnitOfWork unitOfWork, IMinutesGenerator minutesGenerator)
        {
            _unitOfWork = unitOfWork;
            _minutesGenerator = minutesGenerator;
        }

        public async Task<Result<FileAttachment>> Handle(PreviewInvoiceMinutesQuery request, CancellationToken cancellationToken)
        {  
            var adjustment = await _unitOfWork.InvoicesRepository.GetAllQueryable()
                .AsNoTracking() 
                .Include(x => x.InvoiceItems).ThenInclude(it => it.Product)
                .Include(x => x.Customer)
                .Include(x => x.Company)
                .FirstOrDefaultAsync(x => x.InvoiceID == request.InvoiceId, cancellationToken);

            if (adjustment == null)
                return Result.Fail($"Không tìm thấy hóa đơn điều chỉnh với ID {request.InvoiceId}");

            if (adjustment.OriginalInvoiceID == null)
                return Result.Fail($"Hóa đơn này không có hóa đơn gốc (OriginalInvoiceID is null).");
            var original = await _unitOfWork.InvoicesRepository.GetByIdAsync(adjustment.OriginalInvoiceID.Value, "Customer,Company,InvoiceItems.Product,Template.Serial.Prefix,Template.Serial.SerialStatus, Template.Serial.InvoiceType,InvoiceStatus");

            if (original == null)
                return Result.Fail($"Không tìm thấy hóa đơn gốc ID {adjustment.OriginalInvoiceID}");
            string contentBefore = "";
            string contentAfter = "";

            if (!string.IsNullOrEmpty(request.ContentBefore) && !string.IsNullOrEmpty(request.ContentAfter))
            {
                contentBefore = request.ContentBefore;
                contentAfter = request.ContentAfter;
            }
            else
            {
                var sbBefore = new StringBuilder();
                var sbAfter = new StringBuilder();
                bool IsDifferent(string? s1, string? s2)
                {
                    string v1 = s1?.Trim() ?? "";
                    string v2 = s2?.Trim() ?? "";
                    return !string.Equals(v1, v2, StringComparison.OrdinalIgnoreCase);
                }
                if (IsDifferent(original.InvoiceCustomerTaxCode, adjustment.InvoiceCustomerTaxCode))
                {
                    sbBefore.AppendLine($"- Mã số thuế: {original.InvoiceCustomerTaxCode}");
                    sbAfter.AppendLine($"- Mã số thuế: {adjustment.InvoiceCustomerTaxCode}");
                }
                if (IsDifferent(original.PaymentMethod, adjustment.PaymentMethod))
                {
                    sbBefore.AppendLine($"- HTTT: {original.PaymentMethod}");
                    sbAfter.AppendLine($"- HTTT: {adjustment.PaymentMethod}");
                }
                if (IsDifferent(original.InvoiceCustomerAddress, adjustment.InvoiceCustomerAddress))
                {
                    sbBefore.AppendLine($"- Địa chỉ: {original.InvoiceCustomerAddress}");
                    sbAfter.AppendLine($"- Địa chỉ: {adjustment.InvoiceCustomerAddress}");
                }
                string nameOld = original.InvoiceCustomerName.Trim() ?? "";
                string nameNew = adjustment.InvoiceCustomerName.Trim() ?? "";
                if (!string.Equals(nameOld, nameNew, StringComparison.OrdinalIgnoreCase))
                {
                    sbBefore.AppendLine($"- Tên đơn vị: {original.InvoiceCustomerName}");
                    sbAfter.AppendLine($"- Tên đơn vị: {adjustment.InvoiceCustomerName}");
                }
                var originalItems = original.InvoiceItems?.ToList() ?? new List<InvoiceItem>();
                var newItems = adjustment.InvoiceItems?.ToList() ?? new List<InvoiceItem>();
                var matchedOriginalItemIds = new HashSet<int>();

                foreach (var newItem in newItems)
                {
                    InvoiceItem? matchedOldItem = null;
                    if (newItem.OriginalInvoiceItemID.HasValue)
                        matchedOldItem = originalItems.FirstOrDefault(x => x.InvoiceItemID == newItem.OriginalInvoiceItemID.Value);

                    if (matchedOldItem == null)
                        matchedOldItem = originalItems.FirstOrDefault(x => x.ProductID == newItem.ProductID && !matchedOriginalItemIds.Contains(x.InvoiceItemID));

                    if (matchedOldItem != null)
                    {
                        matchedOriginalItemIds.Add(matchedOldItem.InvoiceItemID);
                        string diffDetail = "";
                        bool isDiff = false;

                        if (matchedOldItem.Quantity != newItem.Quantity) { isDiff = true; diffDetail += $"SL: {matchedOldItem.Quantity} -> {newItem.Quantity}; "; }
                        if (matchedOldItem.UnitPrice != newItem.UnitPrice) { isDiff = true; diffDetail += $"Giá: {matchedOldItem.UnitPrice:N0} -> {newItem.UnitPrice:N0}; "; }
                        if (matchedOldItem.Amount != newItem.Amount) { isDiff = true; diffDetail += $"Thành tiền: {matchedOldItem.Amount:N0} -> {newItem.Amount:N0}; "; }

                        if (isDiff)
                        {
                            string pName = newItem.Product?.Name ?? "Sản phẩm";
                            sbBefore.AppendLine($"- {pName}: {GetItemString(matchedOldItem)}");
                            sbAfter.AppendLine($"- {pName}: {GetItemString(newItem)} ({diffDetail.TrimEnd(' ', ';')})");
                        }
                    }
                    else
                    {
                        string pName = newItem.Product?.Name ?? "Sản phẩm mới";
                        sbBefore.AppendLine($"- {pName}: (Không có)");
                        sbAfter.AppendLine($"- {pName}: {GetItemString(newItem)} (Thêm mới)");
                    }
                }

                foreach (var oldItem in originalItems)
                {
                    if (!matchedOriginalItemIds.Contains(oldItem.InvoiceItemID))
                    {
                        string pName = oldItem.Product?.Name ?? "Sản phẩm cũ";
                        sbBefore.AppendLine($"- {pName}: {GetItemString(oldItem)}");
                        sbAfter.AppendLine($"- {pName}: (Đã xóa)");
                    }
                }

                if (sbBefore.Length > 0 || sbAfter.Length > 0)
                {
                    contentBefore += sbBefore.ToString();
                    contentAfter += sbAfter.ToString();
                }

                if (string.IsNullOrEmpty(contentBefore))
                {
                    contentBefore = "................................................";
                    contentAfter = "................................................";
                }
            }
            byte[] fileBytes;
            string fileName;

            if (request.Type == MinutesType.Replacement)
            {
                fileBytes = await _minutesGenerator.GenerateReplacementMinutesAsync(
                    original, request.Reason, contentBefore, contentAfter, adjustment.InvoiceNumber.ToString(), request.AgreementDate);
                fileName = $"BienBan_ThayThe_{original.InvoiceNumber}_to_{adjustment.InvoiceNumber}.docx";
            }
            else
            {
                fileBytes = await _minutesGenerator.GenerateAdjustmentMinutesAsync(
                    original, request.Reason, contentBefore, contentAfter, adjustment.InvoiceNumber.ToString(), request.AgreementDate);
                fileName = $"BienBan_DieuChinh_{original.InvoiceNumber}_to_{adjustment.InvoiceNumber}.docx";
            }

            return Result.Ok(new FileAttachment { FileContent = fileBytes, FileName = fileName });
        }

        // Helper function (local)
        private string GetItemString(InvoiceItem item) => $"SL: {item.Quantity} x Giá: {item.UnitPrice:N0} = {item.Amount:N0}";
    }
}
