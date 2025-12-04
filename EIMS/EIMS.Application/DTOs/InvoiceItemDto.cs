using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs
{
    public class InvoiceItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? Unit { get; set; }
        public double Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal VATAmount { get; set; }
    }
}
