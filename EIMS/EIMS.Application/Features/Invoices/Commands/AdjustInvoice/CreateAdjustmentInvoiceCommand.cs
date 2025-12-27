using EIMS.Application.DTOs;
using EIMS.Application.DTOs.Invoices;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Commands.AdjustInvoice
{
    public class CreateAdjustmentInvoiceCommand : IRequest<Result<int>>
    {
        public int OriginalInvoiceId { get; set; }
        public int? PerformedBy { get; set; }
        public string AdjustmentReason { get; set; }
        public int? NewCustomerId { get; set; }
        public List<InvoiceItemInputDto> AdjustmentItems { get; set; } = new();
    }
}
