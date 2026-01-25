using EIMS.Application.DTOs;
using EIMS.Application.DTOs.Invoices;
using EIMS.Domain.Enums;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Commands.AdjustInvoice
{
    public class CreateAdjustmentInvoiceCommand : IRequest<Result<AdjustmentInvoiceDetailDto>>
    {
        public int OriginalInvoiceId { get; set; }
        public int? TemplateId { get; set; }
        public int? InvoiceStatusId { get; set; }
        public string? MinuteCode { get; set; }
        public string? AdjustmentReason { get; set; }
        public List<InvoiceItemInputDto> AdjustmentItems { get; set; } = new();
        public string? RootPath { get; set; }
    }
}
