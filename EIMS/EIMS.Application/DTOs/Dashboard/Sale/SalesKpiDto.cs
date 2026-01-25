using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard.Sale
{
    public class SalesKpiDto
    {
        public decimal CurrentRevenue { get; set; }
        public double RevenueGrowthPercent { get; set; }
        public int TotalCustomers { get; set; }
    }
}
