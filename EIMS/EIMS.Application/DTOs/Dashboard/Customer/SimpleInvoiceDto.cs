using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard
{
    public class SimpleInvoiceDto
    {
       public int InvoiceId { get; set; }
        public long? InvoiceNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public string StatusName { get; set; } // "Paid", "Unpaid" etc.
        public int StatusId { get; set; }
    }
}