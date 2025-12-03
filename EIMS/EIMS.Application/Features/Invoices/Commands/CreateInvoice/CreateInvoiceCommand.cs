using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs;
using EIMS.Application.DTOs.Invoices;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceCommand : IRequest<Result<CreateInvoiceResponse>>
    {
        public int? TemplateID { get; set; }
        public int? CustomerID { get; set; }
        public string TaxCode { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public List<InvoiceItemDto>? Items { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int? SignedBy { get; set; }
        public int? MinRows { get; set; }
        public int  SalesID { get; set; }
    }
}
