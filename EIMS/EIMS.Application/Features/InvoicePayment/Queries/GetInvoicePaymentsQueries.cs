using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.InvoicePayment;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoicePayment.Queries
{
    public class GetInvoicePayments : IRequest<Result<PaginatedList<InvoicePaymentDTO>>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? InvoiceId { get; set; }
        public int? CustomerId { get; set; }
        public int? SalesId { get; set; }
        public string? SearchTerm { get; set; }
    }
}