using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.InvoiceItem
{
    public class CreateInvoiceItemRequest
    {
         public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? Unit { get; set; }
        public double Quantity { get; set; }
        public decimal? Amount { get; set; } = 0 ;
        public decimal? VATAmount { get; set; } = 0;
    }
}