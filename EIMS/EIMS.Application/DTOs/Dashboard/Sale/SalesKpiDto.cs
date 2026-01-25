using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard.Sale
{
    public class SalesKpiDto
    {
        public decimal CurrentRevenue { get; set; }
        public decimal LastMonthRevenue { get; set; }
        public double RevenueGrowthPercent { get; set; }
        public decimal EstimatedCommission { get; set; }
        public double CommissionRate { get; set; }
        public int NewCustomersThisMonth { get; set; }
        public int OpenInvoicesCount { get; set; }

        public int TotalCustomers { get; set; }        
        public int TotalInvoiceRequests { get; set; }  
        public decimal TotalLifetimeRevenue { get; set; }
        public int TotalIssuedInvoices { get; set; }    
        public decimal TotalOutstandingDebt { get; set; }
    }
}
