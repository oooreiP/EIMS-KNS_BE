using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.InvoiceStatement;
using EIMS.Domain.Enums;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.InvoiceStatements.Commands
{
    public class GenerateAllStatementsCommandHandler : IRequestHandler<GenerateAllStatementsCommand, Result<GenerateBatchStatementResponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ISender _sender;

        public GenerateAllStatementsCommandHandler(IUnitOfWork uow, ISender sender)
        {
            _uow = uow;
            _sender = sender;
        }

        public async Task<Result<GenerateBatchStatementResponse>> Handle(GenerateAllStatementsCommand request, CancellationToken cancellationToken)
        {
            var response = new GenerateBatchStatementResponse();

            // 1. Determine the Date Boundary
            var baseDate = new DateTime(request.Year, request.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var statementDate = baseDate.AddMonths(1);
            var allowedStatuses = new List<int> {
                (int)EInvoiceStatus.Issued,
                (int)EInvoiceStatus.Adjusted,
            };
            // 2. Find "Candidate" Customers
            // We want customers who have at least one invoice that is:
            // - Created/Signed before the statement date
            // - Not cancelled (Status != 1 Draft, or whatever your void logic is)
            // - Not Fully Paid (PaymentStatus != 3)
            var candidateCustomerIds = await _uow.InvoicesRepository.GetAllQueryable()
                .AsNoTracking()
                .Where(i => (i.SignDate ?? i.CreatedAt) <= statementDate)
                .Where(i => allowedStatuses.Contains(i.InvoiceStatusID))
                .Select(i => i.CustomerID)
                .Distinct()
                .ToListAsync(cancellationToken);

            response.TotalCandidates = candidateCustomerIds.Count;

            // 3. Loop and Generate
            foreach (var customerId in candidateCustomerIds)
            {
                // Create the command for a SINGLE customer
                var singleCommand = new CreateStatementCommand
                {
                    CustomerID = customerId,
                    Month = request.Month,
                    Year = request.Year,
                    AuthenticatedUserId = request.AuthenticatedUserId
                };

                // Send it to the existing handler
                var result = await _sender.Send(singleCommand, cancellationToken);

                if (result.IsSuccess)
                {
                    response.SuccessCount++;
                }
                else
                {
                    // If it failed because "No debt found" (which might happen if the db filter above wasn't perfect), 
                    // we usually just ignore it. But real errors should be logged.
                    var errorMsg = result.Errors.FirstOrDefault()?.Message ?? "Unknown Error";

                    // Optional: Don't count "No debt" as a failure if you want cleaner logs
                    if (!errorMsg.Contains("no outstanding debt"))
                    {
                        response.FailureCount++;
                        response.Errors.Add($"Customer {customerId}: {errorMsg}");
                    }
                }
            }

            return Result.Ok(response);
        }
    }
}