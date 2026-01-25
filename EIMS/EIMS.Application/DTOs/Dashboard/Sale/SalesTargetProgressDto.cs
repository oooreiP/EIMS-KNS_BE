using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard.Sale
{
    public class SalesTargetProgressDto
    {
        public decimal CurrentRevenue { get; set; }
        public decimal TargetRevenue { get; set; }
        public double CompletionRate { get; set; }
        public decimal RemainingAmount { get; set; }
        public int DaysLeft { get; set; }
    }
}
