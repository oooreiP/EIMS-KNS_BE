using EIMS.Application.DTOs;
using EIMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceCommand : IRequest<Invoice>
    {
        public int? TemplateID { get; set; }
        public int? CustomerID { get; set; }
        public string TaxCode { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public List<InvoiceItemDto>? Items { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int? SignedBy { get; set; }
    }
}
