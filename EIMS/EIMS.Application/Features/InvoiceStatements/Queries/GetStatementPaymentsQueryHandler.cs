using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.InvoiceStatement;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.InvoiceStatements.Queries
{
    public class GetStatementPaymentsQueryHandler : IRequestHandler<GetStatementPaymentsQuery, Result<StatementPaymentsResponseDto>>
    {
        private readonly IUnitOfWork _uow;

        public GetStatementPaymentsQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<StatementPaymentsResponseDto>> Handle(GetStatementPaymentsQuery request, CancellationToken cancellationToken)
        {
            var statement = await _uow.InvoiceStatementRepository.GetByIdWithInvoicesAsync(request.StatementId);
            if (statement == null)
            {
                return Result.Fail($"Statement with id {request.StatementId} not found.");
            }

            var paymentsQuery = _uow.StatementPaymentRepository
                .GetAllQueryable(includeProperties: "Payment,Payment.Invoice")
                .Where(x => x.StatementID == request.StatementId);

            var paidAmount = await paymentsQuery
                .Select(x => (decimal?)x.AppliedAmount)
                .SumAsync(cancellationToken) ?? 0m;

            var paymentItems = await paymentsQuery
                .OrderBy(x => x.AppliedAt)
                .ThenBy(x => x.StatementPaymentID)
                .Select(x => new StatementPaymentItemDto
                {
                    StatementPaymentId = x.StatementPaymentID,
                    PaymentId = x.PaymentID,
                    InvoiceId = x.Payment.InvoiceID,
                    InvoiceNumber = x.Payment.Invoice.InvoiceNumber ?? 0,
                    AppliedAmount = x.AppliedAmount,
                    InvoiceRemainingAfter = Math.Max(0, x.Payment.Invoice.TotalAmount - x.Payment.Invoice.PaidAmount),
                    PaymentDate = x.Payment.PaymentDate,
                    PaymentMethod = x.Payment.PaymentMethod,
                    TransactionCode = x.Payment.TransactionCode,
                    Note = x.Payment.Note,
                    CreatedBy = x.Payment.CreatedBy
                })
                .ToListAsync(cancellationToken);

            var runningPaid = 0m;
            foreach (var item in paymentItems)
            {
                runningPaid += item.AppliedAmount;
                item.StatementPaidAfter = runningPaid;
                item.StatementBalanceAfter = Math.Max(0, statement.TotalAmount - runningPaid);
            }

            paymentItems = paymentItems
                .OrderByDescending(x => x.PaymentDate)
                .ThenByDescending(x => x.StatementPaymentId)
                .ToList();

            var response = new StatementPaymentsResponseDto
            {
                StatementId = statement.StatementID,
                TotalAmount = statement.TotalAmount,
                PaidAmount = paidAmount,
                BalanceDue = Math.Max(0, statement.TotalAmount - paidAmount),
                StatusId = statement.StatusID,
                Status = statement.StatementStatus?.StatusName ?? "Unknown",
                Payments = paymentItems
            };

            return Result.Ok(response);
        }
    }
}
