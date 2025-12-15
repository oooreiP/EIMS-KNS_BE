using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs.InvoicePayment;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoicePayment.Queries
{
    public class GetInvoicePaymentByIdQuery : IRequest<Result<InvoicePaymentDTO>>
    {
        public int Id { get; set; }
    }
}