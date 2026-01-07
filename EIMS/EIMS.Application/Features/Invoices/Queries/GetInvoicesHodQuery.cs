using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.Invoices;
using MediatR;

namespace EIMS.Application.Features.Invoices.Queries
{
    public class GetInvoicesHodQuery : IRequest<PaginatedList<InvoiceDTO>>
    {
        // --- Pagination ---
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // --- Search ---
        public string? SearchTerm { get; set; }

        // --- Advanced Filters ---
        public DateTime? FromDate { get; set; }      // Filter by Issued Date
        public DateTime? ToDate { get; set; }
        
        public decimal? MinAmount { get; set; }      // Filter by Total Amount
        public decimal? MaxAmount { get; set; }
        
        public int? PaymentStatusId { get; set; }    // Filter by Paid/Unpaid
        public int? InvoiceStatusId { get; set; }    // Filter by Specific Status (e.g. CQT Approved)

        // --- Sorting ---
        public string? SortColumn { get; set; }      // e.g. "date", "amount", "number"
        public string? SortDirection { get; set; }   // "asc" or "desc"
    }
}