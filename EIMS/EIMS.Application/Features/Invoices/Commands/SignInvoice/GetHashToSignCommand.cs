using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Commands.SignInvoice
{
    public class GetHashToSignCommand : IRequest<Result<string>>
    {
        public int InvoiceId { get; set; }
    }
}
