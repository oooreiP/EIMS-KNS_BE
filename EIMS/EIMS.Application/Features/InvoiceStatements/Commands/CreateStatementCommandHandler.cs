using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.InvoiceStatement;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.InvoiceStatements.Commands
{
    public class CreateStatementCommandHandler : IRequestHandler<CreateStatementCommand, Result<StatementDetailResponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CreateStatementCommandHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<StatementDetailResponse>> Handle(CreateStatementCommand request, CancellationToken cancellationToken)
        {
            string statementCode = $"ST-{request.CustomerID}-{request.Month:D2}{request.Year}";

            // 1. CHECK FOR DUPLICATES
            // 1. CHECK FOR DUPLICATES
            var existingStatement = await _uow.InvoiceStatementRepository
                .GetAllQueryable()
                .Include(s => s.Customer)
                .Include(s => s.StatementDetails)
                    .ThenInclude(sd => sd.Invoice)
                .Include(s => s.StatementStatus)
                .FirstOrDefaultAsync(s => s.StatementCode == statementCode, cancellationToken);
            if (existingStatement != null)
            {
                // If it already exists, just return its ID. 
                var responseDto = _mapper.Map<StatementDetailResponse>(existingStatement);
                return Result.Ok(responseDto);
            }
            //find the date boundary
            var startOfMonth = new DateTime(request.Year, request.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var statementDate = startOfMonth.AddMonths(1).AddDays(-1);            // 2. Fetch Invoices WITH Payments to calculate real balance
            var rawInvoices = await _uow.InvoicesRepository
                .GetAllQueryable()
                .Include(i => i.Payments)
                .Include(i => i.Customer)
                .Where(i => i.CustomerID == request.CustomerID)
                .Where(i => (i.SignDate ?? i.CreatedAt) <= statementDate)
                .Where(i => i.InvoiceStatusID != 1)
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
            decimal totalOriginalAmount = debtItems.Sum(x => x.Invoice.TotalAmount);
            decimal totalPaidSoFar = debtItems.Sum(x => x.Paid);

            await using var transaction = await _uow.BeginTransactionAsync();
            try
            {
                var statement = new InvoiceStatement
                {
                    CustomerID = request.CustomerID,
                    StatementCode = $"ST-{request.CustomerID}-{request.Month:D2}{request.Year}",
                    StatementDate = DateTime.UtcNow,

                    // ADD THIS: Set a Due Date (e.g., 14 days from now) 
                    // Essential for the "IsOverdue" logic we added earlier
                    DueDate = DateTime.UtcNow.AddDays(14),

                    CreatedBy = request.AuthenticatedUserId,
                    TotalInvoices = debtItems.Count,
                    Notes = $"Statement for {request.Month}/{request.Year}",

                    // --- UPDATED FINANCIALS ---
                    TotalAmount = totalOriginalAmount,
                    PaidAmount = totalPaidSoFar,

                    // --- UPDATED STATUS LOGIC ---
                    // 5 = Paid (Shouldn't happen here due to filter, but good safety)
                    // 4 = Partially Paid (If they have paid ANYTHING so far)
                    // 1 = Draft (Default)
                    StatusID = (totalPaidSoFar >= totalOriginalAmount) ? 5 :
                               (totalPaidSoFar > 0) ? 4 :
                               1
                };

                // 5. Create Details
                foreach (var item in debtItems)
                {
                    statement.StatementDetails.Add(new InvoiceStatementDetail
                    {
                        InvoiceID = item.Invoice.InvoiceID,
                        //save the snapshot of what was owed specifically at this moment
                        OutstandingAmount = item.Remaining,
                        Invoice = item.Invoice
                    });
                }

                await _uow.InvoiceStatementRepository.CreateAsync(statement);
                await _uow.SaveChanges();
                await _uow.CommitAsync();

                // EF Core won't automatically load the 'Customer' navigation property on the new entity.
                // We manually assign it from the loaded invoices so the Mapper can map 'CustomerName'.
                statement.Customer = rawInvoices.FirstOrDefault()?.Customer;

                // Your MappingProfile handles StatusID fallback logic, so we don't need to load StatementStatus entity.
                var response = _mapper.Map<StatementDetailResponse>(statement);

                return Result.Ok(response);
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                return Result.Fail($"Failed to generate statement: {ex.Message}");
            }
        }
    }
}