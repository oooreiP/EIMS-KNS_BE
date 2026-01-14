using EIMS.Application.DTOs.InvoiceItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Commands
{
    public abstract class BaseInvoiceCommand
    {
        public int? TemplateID { get; set; }
        public int? CustomerID { get; set; }
        public string TaxCode { get; set; } = string.Empty;
        public int InvoiceStatusID { get; set; }
        public int CompanyID { get; set; } = 1;
        public int? SalesID { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }
        public string PaymentMethod { get; set; }
        public List<CreateInvoiceItemRequest>? Items { get; set; }
        public decimal? Amount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? SignedBy { get; set; }
        public int? MinRows { get; set; }
        // public int SalesID { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactPhone { get; set; }
    }
}
