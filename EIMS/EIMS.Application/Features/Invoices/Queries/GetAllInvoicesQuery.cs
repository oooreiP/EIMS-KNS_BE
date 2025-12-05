using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.Invoices;
using EIMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Queries
{
    public class GetAllInvoicesQuery : IRequest<PaginatedList<InvoiceDTO>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
        public int? StatusId { get; set; }
    }
}
