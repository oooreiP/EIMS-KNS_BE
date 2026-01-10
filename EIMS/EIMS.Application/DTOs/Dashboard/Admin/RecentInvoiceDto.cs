using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard.Admin
{
    public class RecentInvoiceDto
    {
        public int InvoiceId { get; set; }
        public long? InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Amount { get; set; }
        public string StatusName { get; set; }
        public string PaymentStatus { get; set; }
    }
}