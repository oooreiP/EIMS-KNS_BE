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
            if (request.Amount <= 0)
                return Result.Fail("Payment amount must be greater than 0.");

            var statement = await _uow.InvoiceStatementRepository.GetByIdWithInvoicesAsync(request.StatementId);
            if (statement == null)
                return Result.Fail($"Statement with id {request.StatementId} not found.");

            if (statement.StatusID == 6 || statement.StatusID == 7)
                return Result.Fail("Cannot add payment to a cancelled or refunded statement.");

            if (statement.TotalAmount <= 0)
                return Result.Fail("This statement has no unpaid balance.");

            if (request.Amount > statement.TotalAmount)
                return Result.Fail($"Payment amount ({request.Amount:N0}) exceeds statement total ({statement.TotalAmount:N0}).");

            var invoices = statement.StatementDetails
                .Select(d => d.Invoice)
                .Where(i => i != null)
                .Distinct()
                .ToList();

            if (!invoices.Any())
                return Result.Fail("This statement contains no invoices.");

            var unpaidInvoices = invoices
                .Where(i => i.RemainingAmount > 0)
                .OrderBy(i => i.PaymentDueDate ?? DateTime.MaxValue)
                .ThenBy(i => i.InvoiceID)
                .ToList();

            if (!unpaidInvoices.Any())
                return Result.Fail("This statement has no unpaid invoices.");

            var totalRemaining = unpaidInvoices.Sum(i => i.RemainingAmount);
            if (request.Amount > totalRemaining)
                return Result.Fail($"Payment amount ({request.Amount:N0}) exceeds total unpaid invoices ({totalRemaining:N0}).");

            var userId = request.CreatedBy ?? request.AuthenticatedUserId;
            if (userId <= 0)
                return Result.Fail("Invalid user.");

            var paymentDate = request.PaymentDate ?? DateTime.UtcNow;
            var remainingToApply = request.Amount;
            var createdPayments = new List<InvoicePaymentEntity>();

            await using var transaction = await _uow.BeginTransactionAsync();
            try
            {
                foreach (var invoice in unpaidInvoices)
                {
                    if (remainingToApply <= 0)
                        break;

                    var remaining = invoice.RemainingAmount;
                    if (remaining <= 0)
                        continue;

                    var applyAmount = Math.Min(remaining, remainingToApply);
                    if (applyAmount <= 0)
                        continue;

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

                    invoice.PaidAmount += applyAmount;
                    invoice.RemainingAmount -= applyAmount;
                    if (invoice.RemainingAmount < 0) invoice.RemainingAmount = 0;

                    if (invoice.RemainingAmount <= 0)
                        invoice.PaymentStatusID = 3; // Paid
                    else
                        invoice.PaymentStatusID = 2; // Partially Paid

                    await _uow.InvoicesRepository.UpdateAsync(invoice);

                    var relatedStatements = await _uow.InvoiceStatementRepository
                        .GetStatementsContainingInvoiceAsync(invoice.InvoiceID);

                    foreach (var stmt in relatedStatements)
                    {
                        stmt.PaidAmount += applyAmount;
                        if (stmt.PaidAmount >= stmt.TotalAmount)
                        {
                            stmt.StatusID = 5; // Paid
                        }
                        else if (stmt.PaidAmount > 0)
                        {
                            stmt.StatusID = 4; // Partially Paid
                        }

                        await _uow.InvoiceStatementRepository.UpdateAsync(stmt);
                    }

                    var history = new InvoiceHistory
                    {
                        InvoiceID = invoice.InvoiceID,
                        ActionType = InvoiceActionTypes.PaymentAdded,
                        PerformedBy = userId,
                        Date = DateTime.UtcNow,
                        ReferenceInvoiceID = null
                    };

                    await _uow.InvoiceHistoryRepository.CreateAsync(history);

                    remainingToApply -= applyAmount;
                }

                if (createdPayments.Count == 0)
                    return Result.Fail("No payment was applied.");

                await _uow.SaveChanges();
                await _uow.CommitAsync();

                var response = new StatementPaymentResultDto
                {
                    StatementId = statement.StatementID,
                    AppliedAmount = request.Amount - remainingToApply,
                    StatementPaidAmount = statement.PaidAmount,
                    StatementBalanceDue = statement.TotalAmount - statement.PaidAmount,
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
