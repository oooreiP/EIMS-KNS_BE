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
        public int NewCustomers { get; set; }
        public int OpenInvoices { get; set; }
    }
}
