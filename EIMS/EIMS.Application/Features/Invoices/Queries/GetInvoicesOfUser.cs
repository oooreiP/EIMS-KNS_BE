using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.Invoices;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Invoices.Queries
{
    public class GetInvoicesOfUser : IRequest<Result<PaginatedList<InvoiceDTO>>>, IAuthenticatedCommand
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
        public string? SortColumn { get; set; }      // e.g. "Amount", "Date", "Customer"
        public string? SortDirection { get; set; }   // "asc" or "desc"
        public string? RoleFilter { get; set; }
        public int AuthenticatedUserId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? FromDate { get; set; }    // Filter by CreatedAt or IssuedDate
        public DateTime? ToDate { get; set; }
        
        public int? InvoiceStatusId { get; set; }  // e.g. 1=Draft, 2=Published, etc.
        public int? PaymentStatusId { get; set; }  // e.g. 1=Unpaid, 2=Paid
        
        public decimal? MinAmount { get; set; }    // Filter by TotalAmount
        public decimal? MaxAmount { get; set; }
    }
}

