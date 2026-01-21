using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.InvoiceStatement
{
    public class StatementProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Unit { get; set; }
        public double Quantity { get; set; }
        public decimal UnitPrice { get; set; } 
        public decimal TotalAmount { get; set; } 
        public decimal VATAmount { get; set; }
    }
}
