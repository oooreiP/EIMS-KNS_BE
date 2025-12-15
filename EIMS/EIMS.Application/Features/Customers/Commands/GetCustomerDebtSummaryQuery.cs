using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.Customer;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Customers.Commands
{
    public class GetCustomerDebtSummaryQuery : IRequest<Result<PaginatedList<CustomerDebtSummaryDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; } // Optional: To search by Name or TaxCode
        public string? SortBy { get; set; } // "totalDebt", "overdueDebt", "lastPaymentDate"
        public string? SortOrder { get; set; } = "desc"; // "asc" or "desc"
        public bool HasOverdue { get; set; } = false; // Filter flag
    }
}