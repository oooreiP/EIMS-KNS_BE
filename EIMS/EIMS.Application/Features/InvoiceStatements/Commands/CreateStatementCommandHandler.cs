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

            int newStatusId = (totalPaidSoFar >= totalOriginalAmount) ? 5 : // Paid
                                      (totalPaidSoFar > 0) ? 4 : // Partially Paid
                                      1; // Draft
            string statementCode = $"ST-{request.CustomerID}-{request.Month:D2}{request.Year}";
            // 2. CHECK FOR EXISTING STATEMENT
            var existingStatement = await _uow.InvoiceStatementRepository
                .GetAllQueryable()
                .Include(s => s.Customer)
                .Include(s => s.StatementDetails)
                .Include(s => s.StatementStatus)
                .FirstOrDefaultAsync(s => s.StatementCode == statementCode, cancellationToken);

            InvoiceStatement statementToReturn;

            if (existingStatement != null)
            {
                existingStatement.TotalInvoices = debtItems.Count;
                existingStatement.TotalAmount = totalOriginalAmount;
                existingStatement.PaidAmount = totalPaidSoFar;
                existingStatement.StatusID = newStatusId;
                existingStatement.StatementDate = DateTime.UtcNow; // Update timestamp
                existingStatement.StatementDetails.Clear();
                foreach (var item in debtItems)
                {
                    existingStatement.StatementDetails.Add(new InvoiceStatementDetail
                    {
                        InvoiceID = item.Invoice.InvoiceID,
                        OutstandingAmount = item.Remaining,
                        Invoice = item.Invoice // Important for Mapper
                    });
                }

                // Attach customer for response mapping
                existingStatement.Customer = rawInvoices.FirstOrDefault()?.Customer;

                statementToReturn = existingStatement;
            }
            else
            {
                // === CREATE LOGIC ===
                var statement = new InvoiceStatement
                {
                    CustomerID = request.CustomerID,
                    StatementCode = statementCode,
                    StatementDate = DateTime.UtcNow,
                    DueDate = DateTime.UtcNow.AddDays(14),
                    CreatedBy = request.AuthenticatedUserId,
                    TotalInvoices = debtItems.Count,
                    Notes = $"Statement for {request.Month}/{request.Year}",
                    TotalAmount = totalOriginalAmount,
                    PaidAmount = totalPaidSoFar,
                    StatusID = newStatusId,
                    Customer = rawInvoices.FirstOrDefault()?.Customer // For Mapper
                };

                foreach (var item in debtItems)
                {
                    statement.StatementDetails.Add(new InvoiceStatementDetail
                    {
                        InvoiceID = item.Invoice.InvoiceID,
                        OutstandingAmount = item.Remaining,
                        Invoice = item.Invoice // Important for Mapper
                    });
                }

                await _uow.InvoiceStatementRepository.CreateAsync(statement);
                statementToReturn = statement;
            }

            // 3. SAVE AND RETURN
            await using var transaction = await _uow.BeginTransactionAsync();
            try
            {
                // SaveChanges will execute the INSERT or UPDATE
                await _uow.SaveChanges();
                await _uow.CommitAsync();

                var response = _mapper.Map<StatementDetailResponse>(statementToReturn);
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