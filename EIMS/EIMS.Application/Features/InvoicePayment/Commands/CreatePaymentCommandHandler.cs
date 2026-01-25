using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.InvoicePayment;
using EIMS.Domain.Constants;
using EIMS.Domain.Entities;
using EIMS.Domain.Enums;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.InvoicePayment.Commands
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Result<InvoicePaymentDTO>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        public CreatePaymentCommandHandler(IUnitOfWork uow, IMapper mapper, ICurrentUserService currentUser)
        {
            _uow = uow;
            _mapper = mapper;
            _currentUser = currentUser;
        }
        public async Task<Result<InvoicePaymentDTO>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            await using var transaction = await _uow.BeginTransactionAsync();
            try
            {
                //fetch invoice 
                var userId = int.Parse(_currentUser.UserId);
                var invoice = await _uow.InvoicesRepository.GetByIdAsync(request.InvoiceId, "Payments");
                if (invoice == null)
                    return Result.Fail($"Invoice with id {request.InvoiceId} not found");
                if (invoice.InvoiceStatusID == 1)
                {
                    return Result.Fail("Cannot add payment to a Draft invoice. Please Sign/Issue the invoice first.");
                }
                //calcute balance
                decimal remaining = invoice.RemainingAmount;
                if (remaining <= 0)
                    return Result.Fail("This invoice has been fully paid. Cannot paid for more");
                if (request.Amount > remaining)
                    return Result.Fail($"Payment amount ({request.Amount:N0}) exceeds remaining balance ({remaining:N0}).");
                //create payment
                var payment = new Domain.Entities.InvoicePayment
                {
                    InvoiceID = request.InvoiceId,
                    AmountPaid = request.Amount,
                    PaymentDate = request.PaymentDate ?? DateTime.UtcNow,
                    PaymentMethod = request.PaymentMethod,
                    TransactionCode = request.TransactionCode,
                    Note = request.Note,
                    Invoice = invoice,
                    CreatedBy = request.UserId
                };
                await _uow.InvoicePaymentRepository.CreateAsync(payment);
                invoice.PaidAmount += request.Amount;
                invoice.RemainingAmount -= request.Amount;
                if (invoice.RemainingAmount < 0) invoice.RemainingAmount = 0;
                if (invoice.RemainingAmount <= 0) // Fully Paid
                {
                    invoice.PaymentStatusID = 3;
                }
                else // Partially Paid
                {
                    invoice.PaymentStatusID = 2;
                }
                await _uow.InvoicesRepository.UpdateAsync(invoice);
                                await _uow.SaveChanges();

                var relatedStatements = await _uow.InvoiceStatementRepository
            .GetStatementsContainingInvoiceAsync(request.InvoiceId);
            
                foreach (var stmt in relatedStatements)
                {
                    // // A. Update the Paid Amount on the statement
                    // stmt.PaidAmount += request.Amount;

                    // // B. Recalculate Statement Status
                    // // 5 = Paid, 4 = Partially Paid, 3 = Sent
                    // if (stmt.PaidAmount >= stmt.TotalAmount)
                    // {
                    //     stmt.StatusID = 5; // Fully Paid
                    // }
                    // else if (stmt.PaidAmount > 0)
                    // {
                    //     // Only switch to "Partially Paid" if it's currently "Sent" or "Draft"
                    //     // If it was already "Partially Paid", this keeps it there.
                    //     stmt.StatusID = 4;
                    // }
                    var statement = await _uow.InvoiceStatementRepository.GetByIdWithInvoicesAsync(stmt.StatementID) ?? stmt;
                    await RecalculateStatementAsync(statement, cancellationToken);
                    // C. Update the Statement in DB
                    // await _uow.InvoiceStatementRepository.UpdateAsync(stmt);
                }
                var history = new InvoiceHistory
                {
                    InvoiceID = request.InvoiceId,
                    ActionType = InvoiceActionTypes.PaymentAdded,
                    PerformedBy = userId,
                    Date = DateTime.UtcNow,
                    ReferenceInvoiceID = null
                };

                await _uow.InvoiceHistoryRepository.CreateAsync(history);
                // 5. Commit
                await _uow.SaveChanges();
                await _uow.CommitAsync();
                return Result.Ok(_mapper.Map<InvoicePaymentDTO>(payment));
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                return Result.Fail(new Error(ex.Message));
            }
        }
         private async Task RecalculateStatementAsync(InvoiceStatement statement, CancellationToken cancellationToken)
        {
            var startOfMonth = new DateTime(statement.PeriodYear, statement.PeriodMonth, 1, 0, 0, 0, DateTimeKind.Utc);
            var endOfMonth = startOfMonth.AddMonths(1);
            var allowedStatuses = new List<int>
            {
                (int)EInvoiceStatus.Issued,
                (int)EInvoiceStatus.Adjusted
            };

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
                .Where(x => x.Remaining > 0 || (x.Invoice.IssuedDate >= startOfMonth))
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

            if (statement.PaidAmount >= statement.TotalAmount)
            {
                statement.StatusID = 5;
            }
            else if (statement.PaidAmount > 0)
            {
                statement.StatusID = 4;
            }

            await _uow.InvoiceStatementRepository.UpdateAsync(statement);
        }
    }
}