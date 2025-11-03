using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs
{
    public class CreateInvoiceRequest
    {
        public int? TemplateID { get; set; }
        public int? CustomerID { get; set; }
        public int? ProductID { get; set; }
        public int? StatementID { get; set; }
        public string TaxCode { get; set; }
        public string? Name { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? AccountNumber { get; set; }
        public List<InvoiceItemDto>? Items { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int? SignedBy { get; set; }
    }
}
