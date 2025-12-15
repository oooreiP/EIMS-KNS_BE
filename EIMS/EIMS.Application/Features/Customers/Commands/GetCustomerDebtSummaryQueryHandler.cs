using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.Customer;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Customers.Commands
{
    public class GetCustomerDebtSummaryQueryHandler : IRequestHandler<GetCustomerDebtSummaryQuery, Result<PaginatedList<CustomerDebtSummaryDto>>>
    {
        private readonly IUnitOfWork _uow;

        public GetCustomerDebtSummaryQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<PaginatedList<CustomerDebtSummaryDto>>> Handle(GetCustomerDebtSummaryQuery request, CancellationToken cancellationToken)
        {
            // 1. Start with the Customer Query
            var query = _uow.CustomerRepository.GetAllQueryable();

            // 2. Apply optional search filter
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                var term = request.SearchTerm.ToLower();
                query = query.Where(c => c.CustomerName.ToLower().Contains(term) ||
                                         c.TaxCode.Contains(term));
            }

            // 3. Project directly to DTO (Efficient SQL generation)
            // We ignore Draft Invoices (StatusID = 1) for debt calculations
            var projection = query.Select(c => new CustomerDebtSummaryDto
            {
                CustomerId = c.CustomerID,
                CustomerName = c.CustomerName,
                TaxCode = c.TaxCode,
                Email = c.ContactEmail,
                Phone = c.ContactPhone,
                Address = c.Address,

                // --- CALCULATIONS ---

                // Total Debt: (TotalAmount - PaidAmount) for all non-draft invoices
                TotalDebt = c.Invoices
                    .Where(i => i.InvoiceStatusID != 1)
                    .Sum(i => i.TotalAmount - i.Payments.Sum(p => p.AmountPaid)),

                // Overdue Debt: Same as above, but only where DueDate is in the past
                OverdueDebt = c.Invoices
                    .Where(i => i.InvoiceStatusID != 1 &&
                                i.PaymentDueDate < DateTime.UtcNow)
                    .Sum(i => i.TotalAmount - i.Payments.Sum(p => p.AmountPaid)),

                // Total Paid: Sum of all payments received
                TotalPaid = c.Invoices
                    .Where(i => i.InvoiceStatusID != 1)
                    .Sum(i => i.Payments.Sum(p => p.AmountPaid)),
                LastPaymentDate = c.Invoices
            .SelectMany(i => i.Payments)
            .Max(p => (DateTime?)p.PaymentDate),
                // Counts
                InvoiceCount = c.Invoices.Count(i => i.InvoiceStatusID != 1),

                UnpaidInvoiceCount = c.Invoices
                    .Count(i => i.InvoiceStatusID != 1 &&
                                (i.TotalAmount - i.Payments.Sum(p => p.AmountPaid)) > 0)
            });

            // 3. Apply "HasOverdue" Filter
            if (request.HasOverdue)
            {
                projection = projection.Where(x => x.OverdueDebt > 0);
            }

            // 4. Apply Sorting
            bool isAsc = request.SortOrder?.ToLower() == "asc";

            projection = request.SortBy switch
            {
                "totalDebt" => isAsc ? projection.OrderBy(x => x.TotalDebt) : projection.OrderByDescending(x => x.TotalDebt),
                "overdueDebt" => isAsc ? projection.OrderBy(x => x.OverdueDebt) : projection.OrderByDescending(x => x.OverdueDebt),
                "lastPaymentDate" => isAsc ? projection.OrderBy(x => x.LastPaymentDate) : projection.OrderByDescending(x => x.LastPaymentDate),
                _ => projection.OrderByDescending(x => x.TotalDebt) // Default Sort
            };

            var result = await PaginatedList<CustomerDebtSummaryDto>.CreateAsync(
                projection,
                request.PageNumber,
                request.PageSize
            );

            return Result.Ok(result);
        }
    }
}