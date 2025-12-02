using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Invoices.Commands.UpdateInvoice
{
    public class UpdateInvoiceCommand : IRequest<Result<int>>
    {
        public int InvoiceId { get; set; }
        public int? CustomerID { get; set; }
        public string TaxCode { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public List<InvoiceItemDto>? Items { get; set; }
        public decimal Amount { get; set; } // Subtotal
        public decimal TaxAmount { get; set; } // VAT Amount
        public decimal TotalAmount { get; set; }
        public int? MinRows { get; set; }
        public int? SignedBy { get; set; } // User modifying the invoice

    }
}