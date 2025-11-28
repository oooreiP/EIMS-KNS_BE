using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.InvoiceStatement
{
    public class StatementInvoiceDto
    {
        public int InvoiceID { get; set; }
        public long InvoiceNumber { get; set; }
        public DateTime? SignDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; } // "Unpaid", "Overdue"
    }
}