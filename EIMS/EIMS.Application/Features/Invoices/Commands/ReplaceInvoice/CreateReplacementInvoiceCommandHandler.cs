using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Commands.ReplaceInvoice
{
    public class CreateReplacementInvoiceCommandHandler : IRequestHandler<CreateReplacementInvoiceCommand, Result<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IEmailService _emailService;

        public CreateReplacementInvoiceCommandHandler(IUnitOfWork uow, IEmailService emailService)
        {
            _uow = uow;
            _emailService = emailService;
        }

        public async Task<Result<int>> Handle(CreateReplacementInvoiceCommand request, CancellationToken cancellationToken)
        {
            var originalInvoice = await _uow.InvoicesRepository.GetByIdAsync(
                request.OriginalInvoiceId,
                includeProperties: "InvoiceItems,InvoiceItems.Product"
            );
            if (originalInvoice == null) return Result.Fail("Không tìm thấy hóa đơn gốc.");
            if (originalInvoice.InvoiceStatusID != 6 && originalInvoice.InvoiceStatusID != 10)
                return Result.Fail("Chỉ được thay thế hóa đơn đã phát hành.");
            var template = await _uow.InvoiceTemplateRepository.GetByIdAsync(originalInvoice.TemplateID);
            if (template == null)
                return Result.Fail(new Error($"Template {originalInvoice.TemplateID} not found").WithMetadata("ErrorCode", "Invoice.Create.Failed"));
            var serial = await _uow.SerialRepository.GetByIdAndLockAsync(template.SerialID);
            if (serial == null)
                return Result.Fail(new Error($"Template {serial.SerialID} not found").WithMetadata("ErrorCode", "Invoice.Create.Failed"));
            serial.CurrentInvoiceNumber += 1;
            long nextInvoiceNumber = serial.CurrentInvoiceNumber;
            // var nextInvoiceNumber = await _uow.InvoicesRepository.GetNextInvoiceNumberAsync(originalInvoice.TemplateID);
            int targetCustomerId = request.CustomerId ?? originalInvoice.CustomerID;
            string targetNote = request.Note ?? originalInvoice.Notes;
            var newInvoice = new Invoice
            {
                InvoiceType = 3, // 3: Thay thế
                OriginalInvoiceID = originalInvoice.InvoiceID,
                AdjustmentReason = request.Reason,
                CustomerID = targetCustomerId,
                Notes = targetNote,
                TemplateID = originalInvoice.TemplateID,
                CompanyId = originalInvoice.CompanyId,
                IssuerID = originalInvoice.IssuerID,
                InvoiceStatusID = 1, // Draft
                CreatedAt = DateTime.UtcNow,
                InvoiceNumber = nextInvoiceNumber,
                MCCQT = null,
                SignDate = null,
                IssuedDate = null,
                DigitalSignature = null,
                TaxAuthorityCode = null,
            };
            decimal totalSubtotal = 0;
            decimal totalVAT = 0;
            var distinctVatRates = new HashSet<decimal>();
            if (request.Items != null && request.Items.Any())
            {
                foreach (var itemInput in request.Items)
                {
                    var product = await _uow.ProductRepository.GetByIdAsync(itemInput.ProductID);
                    if (product == null) return Result.Fail($"Sản phẩm {itemInput.ProductID} không tồn tại.");
                    decimal price = itemInput.UnitPrice ?? product.BasePrice;
                    decimal vatRate = itemInput.OverrideVATRate ?? product?.VATRate ?? 0;
                    distinctVatRates.Add(vatRate);
                    decimal amount = (decimal)itemInput.Quantity * price;
                    decimal vatAmount = amount * (vatRate / 100m);

                    newInvoice.InvoiceItems.Add(new InvoiceItem
                    {
                        ProductID = itemInput.ProductID,
                        Quantity = itemInput.Quantity,
                        UnitPrice = price,
                        Amount = amount,
                        VATAmount = vatAmount
                    });

                    totalSubtotal += amount;
                    totalVAT += vatAmount;
                }
            }
            else
            {
                if (originalInvoice.InvoiceItems != null)
                {
                    foreach (var oldItem in originalInvoice.InvoiceItems)
                    {
                        decimal oldItemRate = oldItem.Product?.VATRate ?? 0;
                        distinctVatRates.Add(oldItemRate);
                        var newItem = new InvoiceItem
                        {
                            ProductID = oldItem.ProductID,
                            Quantity = oldItem.Quantity,
                            UnitPrice = oldItem.UnitPrice,
                            Amount = oldItem.Amount,
                            VATAmount = oldItem.VATAmount,
                            InvoiceID = oldItem.InvoiceID,
                            InvoiceItemID = oldItem.InvoiceItemID
                        };
                        newInvoice.InvoiceItems.Add(newItem);
                        totalSubtotal += oldItem.Amount;
                        totalVAT += oldItem.VATAmount;
                    }
                }
            }
            newInvoice.SubtotalAmount = totalSubtotal;
            newInvoice.VATAmount = totalVAT;
            newInvoice.TotalAmount = totalSubtotal + totalVAT;

            if (distinctVatRates.Count == 0)
            {
                newInvoice.VATRate = 0;
            }
            else if (distinctVatRates.Count == 1)
            {
                newInvoice.VATRate = distinctVatRates.First();
            }
            else
            {
                // Trường hợp đa thuế suất (Có cả 8% và 10%)
                // Theo quy định XML CQT: 
                // Nếu hóa đơn hỗn hợp, thường để -1 (Khác) hoặc hệ thống phải chặn.
                // Ở đây ta gán -1 để biểu thị "Nhiều thuế suất"
                newInvoice.VATRate = -1;
            }
            originalInvoice.InvoiceStatusID = 10;
            await _uow.InvoicesRepository.UpdateAsync(originalInvoice);
            await _uow.InvoicesRepository.CreateAsync(newInvoice);
            await _uow.InvoiceHistoryRepository.CreateAsync(new InvoiceHistory
            {
                InvoiceID = originalInvoice.InvoiceID,
                ActionType = "Replacement Created",
                Date = DateTime.UtcNow
                // PerformedBy lấy từ User Context
            });

            await _uow.SaveChanges();
            await _emailService.SendStatusUpdateNotificationAsync(newInvoice.InvoiceID, 10);
            return Result.Ok(newInvoice.InvoiceID);
        }
    }
}
