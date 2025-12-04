using EIMS.Application.DTOs.Invoices;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Commands.ReplaceInvoice
{
    public class CreateReplacementInvoiceCommand : IRequest<Result<int>>
    {
        public int OriginalInvoiceId { get; set; }
        public string Reason { get; set; } 
        public int? CustomerId { get; set; }
        public string? Note { get; set; }
        public List<InvoiceItemInputDto> Items { get; set; } = new();
    }
}
