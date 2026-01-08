using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs.Dashboard.Admin;

namespace EIMS.Application.DTOs.Dashboard
{
    public class AdminDashboardDto
    {
        public FinancialStatsDto CurrentMonthStats { get; set; }
        public FinancialStatsDto AllTimeStats { get; set; }
        public InvoiceCountDto InvoiceCounts { get; set; }
        public UserStatsDto UserStats { get; set; }
        public List<RevenueTrendDto> RevenueTrend { get; set; } = new();
        public List<TopCustomerDto> TopCustomers { get; set; } = new();
        public List<RecentInvoiceDto> RecentInvoices { get; set; } = new();
        public double RevenueGrowthPercentage { get; set; }
    }
}