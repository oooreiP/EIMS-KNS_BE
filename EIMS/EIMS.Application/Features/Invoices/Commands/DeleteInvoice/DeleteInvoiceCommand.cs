using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Commands.DeleteInvoice
{
    public class DeleteInvoiceCommand : IRequest<Result<bool>>
    {
        public int InvoiceID { get; set; }

        public DeleteInvoiceCommand(int invoiceId)
        {
            InvoiceID = invoiceId;
        }
    }
}
