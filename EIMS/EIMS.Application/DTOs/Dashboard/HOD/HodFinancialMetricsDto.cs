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
        public decimal Outstanding { get; set; }      // Tiền còn phải thu
        public double OutstandingRate { get; set; }   // % còn phải thu
        public double VatRate { get; set; } = 10;     // Mặc định 10%
        public decimal TotalDebt { get; set; }
        public int TotalDebtCount { get; set; }
    }
}