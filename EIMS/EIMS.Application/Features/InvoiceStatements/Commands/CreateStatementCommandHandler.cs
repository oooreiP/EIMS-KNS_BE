using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;

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
            //get unpaid invoices
            var unpaidInvoices = _uow.InvoicesRepository
                .GetAllQueryable()
                .Where(i => i.CustomerID == request.CustomerID)
                .Where(i => i.SignDate <= statementDate)
                .Where(i => i.PaymentStatusID == 1 || i.PaymentStatusID == 2)
                .ToList();
            if (!unpaidInvoices.Any())
                return Result.Fail(new Error($"This customer {request.CustomerID} has no unpaid invoices."));
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
                    TotalInvoices = unpaidInvoices.Count,
                    TotalAmount = unpaidInvoices.Sum(i => i.TotalAmount),
                    Notes = $"Statement for {request.Month}/{request.Year}"
                };
                foreach (var inv in unpaidInvoices)
                {
                    statement.StatementDetails.Add(new InvoiceStatementDetail
                    {
                        InvoiceID = inv.InvoiceID
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
