using EIMS.Application.DTOs.Invoices;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Queries
{
    public class GetOriginalInvoiceQuery : IRequest<Result<InvoiceDTO>>
    {
        public int ChildInvoiceId { get; set; }
    }
}
