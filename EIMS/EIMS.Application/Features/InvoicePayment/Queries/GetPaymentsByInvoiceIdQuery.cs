using EIMS.Application.DTOs.InvoicePayment;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoicePayment.Queries
{
    public class GetPaymentsByInvoiceIdQuery : IRequest<Result<List<InvoicePaymentDTO>>>
    {
        public int InvoiceId { get; set; }

        public GetPaymentsByInvoiceIdQuery(int invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }
}
