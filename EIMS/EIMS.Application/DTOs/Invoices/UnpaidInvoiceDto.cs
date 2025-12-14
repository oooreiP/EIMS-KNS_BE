using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Invoices
{
    public class UnpaidInvoiceDto
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; } // IssuedDate or SignDate
        public DateTime DueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public string PaymentStatus { get; set; } // "Unpaid", "PartiallyPaid", "Overdue"
        public string Description { get; set; }   // Usually "Notes" or created from date
        public bool IsOverdue { get; set; }
    }
}