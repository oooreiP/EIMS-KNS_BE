using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard
{
    public class CustomerDashboardDto
    {
        public int TotalInvoices { get; set; }
        public int PaidInvoicesCount { get; set; }      // Status 3
        public int UnpaidInvoicesCount { get; set; }    // Status 1
        public int PartiallyPaidInvoicesCount { get; set; } // Status 2
        public int OverdueInvoicesCount { get; set; }   // Status 4 (or Date passed)

        // --- Money ---
        public decimal TotalInvoicedAmount { get; set; }  // Sum of TotalAmount (Total value of orders)
        public decimal TotalAmountPaid { get; set; }      // Sum of PaidAmount (Money you have collected)
        public decimal TotalAmountPending { get; set; }   // Sum of RemainingAmount (Money you are owed)

        // --- Recent Activity ---
        public List<SimpleInvoiceDto> RecentInvoices { get; set; } = new();
    }
}