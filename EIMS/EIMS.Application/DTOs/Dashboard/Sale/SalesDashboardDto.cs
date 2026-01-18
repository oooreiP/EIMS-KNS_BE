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
        public decimal LastMonthRevenue { get; set; }
        public double RevenueGrowthPercentage { get; set; } // Round to 2 decimals
        public int NewCustomersThisMonth { get; set; } // Customers with first invoice this month
        public decimal TargetRevenue { get; set; } = 500000000; // Default 500M
        public double CommissionRate { get; set; } = 2.0; // Default 2%

        // --- 3. Sales Trend (New B) ---
        public List<SalesTrendDto> SalesTrend { get; set; } = new();

        // --- 4. Debt Watchlist (New C) ---
        public List<DebtWatchlistDto> DebtWatchlist { get; set; } = new();
        // --- Recent Activity ---
        public List<SalesInvoiceSimpleDto> RecentSales { get; set; } = new();
    }
}