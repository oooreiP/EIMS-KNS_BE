using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs
{
    public class CreateInvoiceRequestwithoutId
    {
        public int? TemplateID { get; set; }
        public string TaxCode { get; set; }
        public string? CompanyName { get; set; }
        public string? CustomerName { get; set; }
        public string Address { get; set; }
        public string? Phone { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int? SignedBy { get; set; }
    }
}
