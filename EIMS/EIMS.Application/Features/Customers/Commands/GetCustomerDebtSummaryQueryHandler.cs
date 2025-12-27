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

            // --- KEY FIX: Define Valid Debt Statuses ---
            // 2=Issued, 8=Signed, 9=Sent, 12=TaxApproved, 15=SendError
            var validStatuses = new[] { 2, 8, 9, 12, 15 };

            // 3. Project directly to DTO
            var projection = query.Select(c => new CustomerDebtSummaryDto
            {
                CustomerId = c.CustomerID,
                CustomerName = c.CustomerName,
                TaxCode = c.TaxCode,
                Email = c.ContactEmail,
                Phone = c.ContactPhone,
                Address = c.Address,

                // --- CALCULATIONS (Updated to use Valid Statuses + Real Columns) ---

                // Total Debt: Sum of 'RemainingAmount' for VALID invoices
                TotalDebt = c.Invoices
                    .Where(i => validStatuses.Contains(i.InvoiceStatusID))
                    .Sum(i => i.RemainingAmount),

                // Overdue Debt: Valid invoices where DueDate < Now AND they still owe money
                OverdueDebt = c.Invoices
                    .Where(i => validStatuses.Contains(i.InvoiceStatusID) &&
                                i.PaymentDueDate < DateTime.UtcNow)
                    .Sum(i => i.RemainingAmount),

                // Total Paid: Sum of 'PaidAmount' for VALID invoices
                TotalPaid = c.Invoices
                    .Where(i => validStatuses.Contains(i.InvoiceStatusID))
                    .Sum(i => i.PaidAmount),
                
                // Last Payment: Look at payments linked to VALID invoices
                LastPaymentDate = c.Invoices
                    .Where(i => validStatuses.Contains(i.InvoiceStatusID))
                    .SelectMany(i => i.Payments)
                    .Max(p => (DateTime?)p.PaymentDate),

                // Counts
                InvoiceCount = c.Invoices
                    .Count(i => validStatuses.Contains(i.InvoiceStatusID)),

                UnpaidInvoiceCount = c.Invoices
                    .Count(i => validStatuses.Contains(i.InvoiceStatusID) && 
                                i.PaymentStatusID != 3) // Assuming 3 = Paid
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