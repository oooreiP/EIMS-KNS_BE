using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceStatuses.Commands
{
    public class DeleteInvoiceStatusCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
