using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.InvoiceStatements.Commands
{
    public class CreateStatementCommandHandler : IRequestHandler<CreateStatementCommand, Result<int>>
    {
        private readonly IUnitOfWork _uow;

        public CreateStatementCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<int>> Handle(CreateStatementCommand request, CancellationToken cancellationToken)
        {
            //find the date boundary
            var statementDate = new DateTime(request.Year, request.Month, 1).AddMonths(1).AddDays(-1);
            // 2. Fetch Invoices WITH Payments to calculate real balance
            var rawInvoices = await _uow.InvoicesRepository
                .GetAllQueryable()
                .Include(i => i.Payments) // Required for calculation
                .Where(i => i.CustomerID == request.CustomerID)
                .Where(i => i.SignDate <= statementDate)
                .Where(i => i.PaymentStatusID == 1 || i.PaymentStatusID == 2) // Unpaid or Partial
                .ToListAsync(cancellationToken);
            // 3. Calculate Remaining Amount in Memory & Filter
            var debtItems = rawInvoices
                .Select(inv => new
                {
                    Invoice = inv,
                    Paid = inv.Payments.Sum(p => p.AmountPaid),
                    Remaining = inv.TotalAmount - inv.Payments.Sum(p => p.AmountPaid)
                })
                .Where(x => x.Remaining > 0) // IMPORTANT: Filter out if fully paid (just in case status was wrong)
                .ToList();

            if (!debtItems.Any())
                return Result.Fail(new Error($"Customer {request.CustomerID} has no outstanding debt for this period."));
            //start BeginTransactionAsync
            await using var transaction = await _uow.BeginTransactionAsync();
            try
            {
                var statement = new InvoiceStatement
                {
                    CustomerID = request.CustomerID,
                    StatementCode = $"ST-{request.CustomerID}-{request.Month:D2}{request.Year}",
                    StatementDate = DateTime.UtcNow,
                    CreatedBy = request.AuthenticatedUserId,
                    StatusID = 1, // Draft
                    TotalInvoices = debtItems.Count,
                    TotalAmount = debtItems.Sum(x => x.Remaining), // Sum of DEBT, not invoice totals                    Notes = $"Statement for {request.Month}/{request.Year}"
                };
                // 5. Create Details with SNAPSHOT
                foreach (var item in debtItems)
                {
                    statement.StatementDetails.Add(new InvoiceStatementDetail
                    {
                        InvoiceID = item.Invoice.InvoiceID,
                        OutstandingAmount = item.Remaining 
                    });
                }

                await _uow.InvoiceStatementRepository.CreateAsync(statement);
                await _uow.CommitAsync();

                return Result.Ok(statement.StatementID);
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                return Result.Fail($"Failed to generate statement: {ex.Message}");
            }
        }
    }
}