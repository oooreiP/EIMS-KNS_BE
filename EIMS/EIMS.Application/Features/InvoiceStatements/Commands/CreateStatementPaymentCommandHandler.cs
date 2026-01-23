using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.InvoicePayment;
using EIMS.Application.DTOs.InvoiceStatement;
using EIMS.Domain.Constants;
using EIMS.Domain.Entities;
using InvoicePaymentEntity = EIMS.Domain.Entities.InvoicePayment;
using FluentResults;
using MediatR;



















using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.InvoiceStatements.Commands
{
    public class CreateStatementPaymentCommandHandler : IRequestHandler<CreateStatementPaymentCommand, Result<StatementPaymentResultDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateStatementPaymentCommandHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<StatementPaymentResultDto>> Handle(CreateStatementPaymentCommand request, CancellationToken cancellationToken)
        {
            // money pay for this payment > 0
            if (request.Amount <= 0)
                return Result.Fail("Payment amount must be greater than 0.");
            // find statement
            var statement = await _uow.InvoiceStatementRepository.GetByIdWithInvoicesAsync(request.StatementId);
            if (statement == null)
                return Result.Fail($"Statement with id {request.StatementId} not found.");
            // invalid statement
            if (statement.StatusID == 6 || statement.StatusID == 7)
                return Result.Fail("Cannot add payment to a cancelled or refunded statement.");
            // the money pay is bigger than the whole statement
            if (request.Amount > statement.TotalAmount)
                return Result.Fail($"Payment amount ({request.Amount:N0}) exceeds statement total ({statement.TotalAmount:N0}).");
            // calculate the money pay for this statement before
            var existingApplied = await _uow.StatementPaymentRepository
                .GetAllQueryable()
                .Where(x => x.StatementID == statement.StatementID)
                .Select(x => (decimal?)x.AppliedAmount)
                .SumAsync(cancellationToken) ?? 0m;
            // money must pay = total of this statement - the money pay before ( take from payment statements above )
            var statementRemaining = statement.TotalAmount - existingApplied;
            // check if it's full paid or not
            if (statementRemaining <= 0)
                return Result.Fail("This statement has no unpaid balance.");
            // check if it bigger than the money must paid
            if (request.Amount > statementRemaining)
                return Result.Fail($"Payment amount ({request.Amount:N0}) exceeds remaining balance ({statementRemaining:N0}).");
            // take all the invoice in that statements and calcute the money to pay left ( remaining )
            var statementInvoices = statement.StatementDetails
                .Where(d => d.Invoice != null)
                .Select(d => new
                {
                    Detail = d,
                    Invoice = d.Invoice,
                    Remaining = Math.Max(0, d.Invoice.TotalAmount - d.Invoice.Payments.Sum(p => p.AmountPaid))
                })
                .GroupBy(x => x.Invoice.InvoiceID)
                .Select(g => g.First())
                .ToList();
            // check if invoice exist
            if (!statementInvoices.Any())
                return Result.Fail("This statement contains no invoices.");
            // take all the ! paid invoices sort with closet payment due date
            var unpaidInvoices = statementInvoices
                .Where(x => x.Remaining > 0)
                .OrderBy(x => x.Invoice.PaymentDueDate ?? DateTime.MaxValue)
                .ThenBy(x => x.Invoice.InvoiceID)
                .ToList();
            // check if all invoices are paid
            if (!unpaidInvoices.Any())
                return Result.Fail("This statement has no unpaid invoices.");
            // calculate the total money to paid for all the unpaid invoices
            var totalRemaining = unpaidInvoices.Sum(i => i.Remaining);
            if (totalRemaining <= 0)
                return Result.Fail("This statement has no unpaid balance.");
            if (request.Amount > totalRemaining)
                return Result.Fail($"Payment amount ({request.Amount:N0}) exceeds total unpaid invoices ({totalRemaining:N0}).");

            var userId = request.CreatedBy ?? request.AuthenticatedUserId;
            if (userId <= 0)
                return Result.Fail("Invalid user.");

            var paymentDate = request.PaymentDate ?? DateTime.UtcNow;
            var remainingToApply = request.Amount;
            var createdPayments = new List<InvoicePaymentEntity>();
            var createdStatementPayments = new List<StatementPayment>();

            await using var transaction = await _uow.BeginTransactionAsync();
            try
            {
                // check each unpaid invoice
                foreach (var item in unpaidInvoices)
                {
                    if (remainingToApply <= 0)
                        break;

                    var invoice = item.Invoice;
                    // if paid full for that invoice move on
                    var remaining = item.Remaining;
                    if (remaining <= 0)
                        continue;
                    // which smaller the money must paid to that invoices or the money we add for statement? 
                    // the money we take from statement give us to pay for that statement to pay for specific invoice in statement
                    var applyAmount = Math.Min(remaining, remainingToApply);
                    if (applyAmount <= 0)
                        continue;
                    // create invoice payment level
                    var payment = new InvoicePaymentEntity
                    {
                        InvoiceID = invoice.InvoiceID,
                        AmountPaid = applyAmount,
                        PaymentDate = paymentDate,
                        PaymentMethod = request.PaymentMethod,
                        TransactionCode = request.TransactionCode,
                        Note = request.Note,
                        Invoice = invoice,
                        CreatedBy = userId
                    };

                    await _uow.InvoicePaymentRepository.CreateAsync(payment);
                    createdPayments.Add(payment);
                    // create payment level statement
                    var statementPayment = new StatementPayment
                    {
                        StatementID = statement.StatementID,
                        Payment = payment,
                        AppliedAmount = applyAmount,
                        AppliedAt = paymentDate,
                        CreatedBy = userId
                    };

                    await _uow.StatementPaymentRepository.CreateAsync(statementPayment);
                    createdStatementPayments.Add(statementPayment);

                    var currentPaid = invoice.Payments.Sum(p => p.AmountPaid);
                    invoice.PaidAmount = currentPaid + applyAmount;
                    invoice.RemainingAmount = Math.Max(0, invoice.TotalAmount - invoice.PaidAmount);

                    if (invoice.RemainingAmount <= 0)
                        invoice.PaymentStatusID = 3; // Paid
                    else
                        invoice.PaymentStatusID = 2; // Partially Paid

                    await _uow.InvoicesRepository.UpdateAsync(invoice);

                    var history = new InvoiceHistory
                    {
                        InvoiceID = invoice.InvoiceID,
                        ActionType = InvoiceActionTypes.PaymentAdded,
                        PerformedBy = userId,
                        Date = DateTime.UtcNow,
                        ReferenceInvoiceID = null
                    };

                    await _uow.InvoiceHistoryRepository.CreateAsync(history);
                    // recalculate the money left after pay for the 1 invoice in statement
                    remainingToApply -= applyAmount;
                }

                if (createdPayments.Count == 0)
                    return Result.Fail("No payment was applied.");
                // recalculate the total money we paid for this statement
                var totalApplied = existingApplied + createdStatementPayments.Sum(x => x.AppliedAmount);
                // check if we fully paid the statement
                if (totalApplied >= statement.TotalAmount)
                {
                    statement.StatusID = 5; // Paid
                }
                // still not enough money
                else if (totalApplied > 0)
                {
                    statement.StatusID = 4; // Partially Paid
                }

                await _uow.InvoiceStatementRepository.UpdateAsync(statement);

                await _uow.SaveChanges();
                await _uow.CommitAsync();

                var response = new StatementPaymentResultDto
                {
                    StatementId = statement.StatementID,
                    // the total money we've use to pay this statement 
                    AppliedAmount = request.Amount - remainingToApply,
                    // calcute sum of the existing money we paid before + paid this time
                    StatementPaidAmount = totalApplied,
                    // check if we fully paid or not
                    StatementBalanceDue = Math.Max(0, statement.TotalAmount - totalApplied),
                    StatementStatusId = statement.StatusID,
                    Payments = _mapper.Map<List<InvoicePaymentDTO>>(createdPayments)
                };

                return Result.Ok(response);
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                return Result.Fail(new Error(ex.Message));
            }
        }
    }
}
