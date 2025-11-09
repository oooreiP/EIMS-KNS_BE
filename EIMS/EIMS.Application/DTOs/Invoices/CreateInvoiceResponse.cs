using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Invoices
{
    public class CreateInvoiceResponse
    {
        public int InvoiceID { get; set; }
        public long InvoiceNumber { get; set; }
        public int CustomerID { get; set; }
        public decimal TotalAmount { get; set; }
        public string TotalAmountInWords { get; set; }
        public string Status { get; set; }
    }
}