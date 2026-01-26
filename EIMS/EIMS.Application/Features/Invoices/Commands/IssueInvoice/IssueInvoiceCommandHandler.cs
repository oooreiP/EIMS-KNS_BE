using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Features.InvoicePayment.Commands;
using EIMS.Domain.Constants;
using EIMS.Domain.Entities;
using EIMS.Domain.Enums;
using FluentResults;
using MediatR;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Commands.IssueInvoice
{
    public class IssueInvoiceCommandHandler : IRequestHandler<IssueInvoiceCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        private readonly IInvoiceXMLService _xmlService;
        private readonly IEmailService _emailService;
        private readonly IMediator _mediator;
        private readonly ILookupCodeGenerator _codeGenerator;
        private readonly IEmailSenderService _emailSender;
        private readonly IInvoiceRealtimeService _invoiceRealtimeService;
        private readonly IDashboardRealtimeService _dashboardRealtimeService;

        public IssueInvoiceCommandHandler(IUnitOfWork uow, IInvoiceXMLService xmlService, IEmailService emailService, IMediator mediator, ILookupCodeGenerator codeGenerator, IEmailSenderService emailSender, IInvoiceRealtimeService invoiceRealtimeService, IDashboardRealtimeService dashboardRealtimeService)
        {
            _uow = uow;
            _xmlService = xmlService;
            _emailService = emailService;
            _mediator = mediator;
            _codeGenerator = codeGenerator;
            _emailSender = emailSender;
            _invoiceRealtimeService = invoiceRealtimeService;
            _dashboardRealtimeService = dashboardRealtimeService;
        }

        public async Task<Result> Handle(IssueInvoiceCommand request, CancellationToken cancellationToken)
        {
            // 1. Lấy thông tin hóa đơn từ DB
            var invoice = await _uow.InvoicesRepository.GetByIdAsync(request.InvoiceId);
            if (invoice.OriginalInvoiceID != null)
            {
                var original = await _uow.InvoicesRepository.GetAllQueryable()
               .Include(x => x.InvoiceItems)
               .Include(x => x.Customer)
               .Include(x => x.Company)
               .OrderByDescending(x => x.InvoiceID)
               .FirstOrDefaultAsync(x => x.InvoiceID == invoice.OriginalInvoiceID);
                if (invoice.InvoiceType == 3) original.InvoiceStatusID = 5;
                else if (invoice.InvoiceType == 2) original.InvoiceStatusID = 4;
            }
            if (invoice == null) return Result.Fail("Không tìm thấy hóa đơn.");
            bool hasSignature = !string.IsNullOrEmpty(invoice.DigitalSignature);
            bool hasMccqt = !string.IsNullOrEmpty(invoice.TaxAuthorityCode);
            if (!hasSignature)
                return Result.Fail("Hóa đơn chưa có chữ ký số.");

            if (!hasMccqt)
                return Result.Fail("Hóa đơn chưa được cấp Mã CQT.");
            if (invoice.InvoiceStatusID != 2)
            {
                invoice.InvoiceStatusID = 2;
                invoice.IssuedDate = DateTime.UtcNow;
                invoice.IssuerID = request.IssuerId;
                decimal initialPaidAmount = 0;

                // 1. Cộng dồn tiền từ hóa đơn gốc (nếu là thay thế)
                if (invoice.InvoiceType == 3 && invoice.OriginalInvoiceID.HasValue)
                {
                    var originalInv = await _uow.InvoicesRepository.GetByIdAsync(invoice.OriginalInvoiceID.Value, "Payments");
                    if (originalInv != null)
                    {
                        initialPaidAmount += originalInv.Payments.Sum(x => x.AmountPaid);
                    }
                }

                // 2. Cộng dồn tiền đã cọc ở hóa đơn hiện tại (nếu có)
                if (invoice.Payments != null)
                {
                    initialPaidAmount += invoice.Payments.Sum(x => x.AmountPaid);
                }

                    invoice.PaidAmount = initialPaidAmount;
                    invoice.RemainingAmount = invoice.TotalAmount - initialPaidAmount;
                    if (invoice.RemainingAmount <= 0)
                    {
                        invoice.PaymentStatusID = 1;
                    }
                    else
                    {
                        // Nếu > 0: Đã trả 1 ít -> Partially (2), Chưa trả gì -> Unpaid (1)
                        invoice.PaymentStatusID = (invoice.PaidAmount > 0) ? 2 : 1;
                    }
                
                if (invoice.PaymentStatusID == 0) invoice.PaymentStatusID = 1;
                if (string.IsNullOrEmpty(invoice.LookupCode))
                {
                    // Sinh mã tra cứu. Lặp lại nếu trùng (dù tỉ lệ trùng rất thấp)
                    bool isUnique = false;
                    string code = "";
                    while (!isUnique)
                    {
                        code = _codeGenerator.Generate(10); // Ví dụ: K7M9X2P3H4
                        bool exists = await _uow.InvoicesRepository.GetAllQueryable()
                                            .AnyAsync(x => x.LookupCode == code, cancellationToken);
                        if (!exists) isUnique = true;
                    }
                    invoice.LookupCode = code;
                }
                await _uow.InvoicesRepository.UpdateAsync(invoice);
            }
            var invoiceRequest = await _uow.InvoiceRequestRepository
            .GetAllQueryable()
            .FirstOrDefaultAsync(r => r.CreatedInvoiceID == request.InvoiceId);
            if (invoiceRequest != null)
            {
                invoiceRequest.RequestStatusID = 5;
                await _uow.InvoiceRequestRepository.UpdateAsync(invoiceRequest);
            }
            await _uow.SaveChanges();
            if (invoice.InvoiceType == 2 || invoice.InvoiceType == 3)
            {
                await RecalculateStatementsForInvoiceAsync(invoice, cancellationToken);
            }
            var history = new InvoiceHistory
            {
                InvoiceID = request.InvoiceId,
                ActionType = InvoiceActionTypes.Issued,
                PerformedBy = request.IssuerId,
                Date = DateTime.UtcNow
            };
            await _uow.InvoiceHistoryRepository.CreateAsync(history);
            await _uow.SaveChanges();
            await _emailSender.SendStatusUpdateNotificationAsync(invoice.InvoiceID, 2);
            // await _emailService.SendStatusUpdateNotificationAsync(invoice.InvoiceID, 2);
            if (request.AutoCreatePayment && request.PaymentAmount > 0 && invoice.TotalAmount > 0)
            {
                decimal currentRemaining = invoice.RemainingAmount;
                if (invoice.RemainingAmount > 0)
                {
                    decimal realPayAmount = Math.Min(currentRemaining, request.PaymentAmount.Value);
                    decimal changeAmount = request.PaymentAmount.Value - realPayAmount;
                    var paymentCommand = new CreatePaymentCommand
                    {
                        InvoiceId = invoice.InvoiceID,
                        UserId = request.IssuerId,
                        Amount = realPayAmount,
                        PaymentDate = DateTime.UtcNow,
                        PaymentMethod = request.PaymentMethod ?? "Cash",
                        TransactionCode = $"AUTO-{invoice.InvoiceNumber}",
                        Note = request.Note ?? "Thanh toán ngay khi phát hành"
                    };

                    // Gọi Handler tạo Payment thông qua Mediator
                    var paymentResult = await _mediator.Send(paymentCommand, cancellationToken);

                    if (paymentResult.IsFailed)
                    {
                        return Result.Ok().WithSuccess("Phát hành thành công nhưng lỗi tạo thanh toán: " + paymentResult.Errors[0].Message);
                    }
                    else
                    {
                        string msg = "Phát hành và thanh toán thành công.";
                        if (changeAmount > 0)
                        {
                            msg += $" Khách đưa {request.PaymentAmount.Value:N0}, thu {realPayAmount:N0}, trả lại {changeAmount:N0}.";
                        }
                        await _invoiceRealtimeService.NotifyInvoiceChangedAsync(new EIMS.Application.Commons.Models.InvoiceRealtimeEvent
                        {
                            InvoiceId = invoice.InvoiceID,
                            ChangeType = "Issued",
                            CompanyId = invoice.CompanyId,
                            CustomerId = invoice.CustomerID,
                            StatusId = invoice.InvoiceStatusID,
                            Roles = new[] { "Admin", "Accountant", "Sale", "HOD" }
                        }, cancellationToken);
                        await _dashboardRealtimeService.NotifyDashboardChangedAsync(new EIMS.Application.Commons.Models.DashboardRealtimeEvent
                        {
                            Scope = "Invoices",
                            ChangeType = "Issued",
                            EntityId = invoice.InvoiceID,
                            Roles = new[] { "Admin", "Accountant", "Sale", "HOD" }
                        }, cancellationToken);
                        return Result.Ok().WithSuccess(msg);
                    }
                }
            }
            await _invoiceRealtimeService.NotifyInvoiceChangedAsync(new EIMS.Application.Commons.Models.InvoiceRealtimeEvent
            {
                InvoiceId = invoice.InvoiceID,
                ChangeType = "Issued",
                CompanyId = invoice.CompanyId,
                CustomerId = invoice.CustomerID,
                StatusId = invoice.InvoiceStatusID,
                Roles = new[] { "Admin", "Accountant", "Sale", "HOD" }
            }, cancellationToken);
            await _dashboardRealtimeService.NotifyDashboardChangedAsync(new EIMS.Application.Commons.Models.DashboardRealtimeEvent
            {
                Scope = "Invoices",
                ChangeType = "Issued",
                EntityId = invoice.InvoiceID,
                Roles = new[] { "Admin", "Accountant", "Sale", "HOD" }
            }, cancellationToken);
            return Result.Ok();
        }

        private async Task RecalculateStatementsForInvoiceAsync(Invoice invoice, CancellationToken cancellationToken)
        {
            var issuedDate = invoice.IssuedDate ?? DateTime.UtcNow;
            var invoicePeriodMonth = issuedDate.Month;
            var invoicePeriodYear = issuedDate.Year;

            var relatedStatements = await _uow.InvoiceStatementRepository
                .GetAllQueryable()
                .Include(s => s.StatementDetails)
                .Where(s => s.StatementDetails.Any(d => d.InvoiceID == invoice.InvoiceID))
                .ToListAsync(cancellationToken);

            var periodStatements = await _uow.InvoiceStatementRepository
                .GetAllQueryable()
                .Include(s => s.StatementDetails)
                .Where(s => s.CustomerID == invoice.CustomerID
                            && s.PeriodMonth == invoicePeriodMonth
                            && s.PeriodYear == invoicePeriodYear)
                .ToListAsync(cancellationToken);

            relatedStatements = relatedStatements
                .Concat(periodStatements)
                .GroupBy(s => s.StatementID)
                .Select(g => g.First())
                .ToList();

            if (!relatedStatements.Any())
            {
                return;
            }

            var allowedStatuses = new List<int>
            {
                (int)EInvoiceStatus.Issued,
                (int)EInvoiceStatus.Adjusted
            };

            foreach (var statement in relatedStatements)
            {
                var startOfMonth = new DateTime(statement.PeriodYear, statement.PeriodMonth, 1, 0, 0, 0, DateTimeKind.Utc);
                var endOfMonth = startOfMonth.AddMonths(1);

                var rawInvoices = await _uow.InvoicesRepository
                    .GetAllQueryable()
                    .Include(i => i.Payments)
                    .Where(i => i.CustomerID == statement.CustomerID)
                    .Where(i => i.IssuedDate != null)
                    .Where(i => i.IssuedDate < endOfMonth)
                    .Where(i => allowedStatuses.Contains(i.InvoiceStatusID))
                    .ToListAsync(cancellationToken);

                decimal openingBalance = rawInvoices
                    .Where(inv => inv.IssuedDate < startOfMonth)
                    .Sum(inv =>
                        inv.TotalAmount - inv.Payments
                            .Where(p => p.PaymentDate < startOfMonth)
                            .Sum(p => p.AmountPaid)
                    );

                decimal newCharges = rawInvoices
                    .Where(inv => inv.IssuedDate >= startOfMonth && inv.IssuedDate < endOfMonth)
                    .Sum(inv => inv.TotalAmount);

                decimal paymentsInPeriod = rawInvoices
                    .SelectMany(i => i.Payments)
                    .Where(p => p.PaymentDate >= startOfMonth && p.PaymentDate < endOfMonth)
                    .Sum(p => p.AmountPaid);

                decimal closingBalance = openingBalance + newCharges - paymentsInPeriod;

                var debtItems = rawInvoices
                    .Select(inv =>
                    {
                        var paidTotal = inv.Payments.Where(p => p.PaymentDate < endOfMonth).Sum(p => p.AmountPaid);
                        var remaining = inv.TotalAmount - paidTotal;
                        return new { Invoice = inv, Remaining = remaining };
                    })
                    .Where(x => x.Remaining != 0 || (x.Invoice.IssuedDate >= startOfMonth))
                    .ToList();

                statement.OpeningBalance = openingBalance;
                statement.NewCharges = newCharges;
                statement.PaidAmount = paymentsInPeriod;
                statement.TotalAmount = closingBalance;
                statement.TotalInvoices = debtItems.Count;

                statement.StatementDetails.Clear();
                foreach (var item in debtItems)
                {
                    statement.StatementDetails.Add(new InvoiceStatementDetail
                    {
                        InvoiceID = item.Invoice.InvoiceID,
                        OutstandingAmount = item.Remaining
                    });
                }
            }
        }
    }
}
