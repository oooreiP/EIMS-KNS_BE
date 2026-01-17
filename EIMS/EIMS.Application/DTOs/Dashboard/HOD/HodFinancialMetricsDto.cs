using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard.HOD
{
    public class HodFinancialMetricsDto
    {
        public decimal NetRevenue { get; set; }
        public decimal CashCollected { get; set; }
        public double CollectionRate { get; set; }
        public decimal EstimatedVAT { get; set; }
        public decimal CriticalDebt { get; set; }
        public int CriticalDebtCount { get; set; }
    }
}