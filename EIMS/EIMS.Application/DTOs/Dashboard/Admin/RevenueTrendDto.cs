using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard.Admin
{
    public class RevenueTrendDto
    {
        public string Month { get; set; } // e.g., "Jan 2024"
        public int MonthNumber { get; set; }
        public int Year { get; set; }
        public decimal Revenue { get; set; }
    }
}