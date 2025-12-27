using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs.Customer;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Customers.Queries
{
    public class GetCustomerDebtDetailQuery : IRequest<Result<CustomerDebtDetailDto>>
    {
        public int CustomerId { get; set; }
        public int InvoicePageIndex { get; set; } = 1;
        public int InvoicePageSize { get; set; } = 10;

        // Pagination for Payments
        public int PaymentPageIndex { get; set; } = 1;
        public int PaymentPageSize { get; set; } = 10;

        // Filters
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? SearchInvoiceNumber { get; set; }
        public string? SortBy { get; set; } // "date", "amount", "dueDate"
        public string? SortOrder { get; set; } // "asc", "desc"
    }
}