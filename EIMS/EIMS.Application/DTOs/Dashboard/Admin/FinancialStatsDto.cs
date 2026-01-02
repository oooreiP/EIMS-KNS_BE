using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard
{
    public class FinancialStatsDto
    {
        public decimal TotalRevenue { get; set; }   // Total Amount (Inc. Tax)
        public decimal NetProfit { get; set; }      // Subtotal (Excl. Tax)
        public decimal TaxLiability { get; set; }   // VAT Amount (Must be paid to Gov)
        public decimal CollectedAmount { get; set; } // Actual Cash received
        public decimal OutstandingAmount { get; set; } // Money waiting to be paid
        public decimal OverdueAmount { get; set; }     // Bad debt risk
    }
}