using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard.Sale
{
    public class SalesInvoiceSimpleDto
    {
        public int InvoiceId { get; set; }
        public long? InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }
}