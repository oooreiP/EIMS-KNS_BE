using EIMS.Application.DTOs;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceItems.Queries
{
    public class GetInvoiceItemsByInvoiceIdQuery : IRequest<Result<List<InvoiceItemDto>>>
    {
        public int InvoiceId { get; set; }
    }
}
