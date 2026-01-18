using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Requests
{
    public class InvoiceRequestItemDto
    {
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
        public decimal VATAmount { get; set; }
    }
}
