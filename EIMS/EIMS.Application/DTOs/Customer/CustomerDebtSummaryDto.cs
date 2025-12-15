using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Customer
{
    public class CustomerDebtSummaryDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string TaxCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        // Financials
        public decimal TotalDebt { get; set; }      // Remaining amount they need to pay
        public decimal OverdueDebt { get; set; }    // Debt that has passed the DueDate
        public decimal TotalPaid { get; set; }      // Total cash collected from them

        // Counts
        public int InvoiceCount { get; set; }       // Total valid invoices
        public int UnpaidInvoiceCount { get; set; } // Invoices with balance > 0
        public DateTime? LastPaymentDate { get; set; }
    }
}