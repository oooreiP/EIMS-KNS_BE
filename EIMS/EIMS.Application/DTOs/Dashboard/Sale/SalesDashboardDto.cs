using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard.Sale
{
    public class SalesDashboardDto
    {
        public int TotalInvoicesGenerated { get; set; }
        public decimal TotalRevenue { get; set; }       // Total value of all contracts/invoices
        public decimal TotalCollected { get; set; }     // Actual cash received (PaidAmount)
        public decimal TotalDebt { get; set; }          // Money still owed by clients

        // --- Current Month Performance (KPIs) ---
        public decimal ThisMonthRevenue { get; set; }
        public int ThisMonthInvoiceCount { get; set; }

        // --- Invoice Status Breakdown ---
        public int PaidCount { get; set; }
        public int UnpaidCount { get; set; }
        public int OverdueCount { get; set; }

        // --- Recent Activity ---
        public List<SalesInvoiceSimpleDto> RecentSales { get; set; } = new();
    }
}