using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs.InvoicePayment;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoicePayment.Commands
{
public class CreatePaymentCommand : IRequest<Result<InvoicePaymentDTO>>
    {
        public int InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public string? TransactionCode { get; set; }
        public string? Note { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int? UserId { get; set; } 
    }
}