using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Invoices.Commands.ChangeInvoiceStatus
{
    public class UpdateInvoiceStatusCommand : IRequest<Result<int>>
    {
        public int InvoiceId { get; set; }
        public int InvoiceStatusId { get; set; }
    }
}