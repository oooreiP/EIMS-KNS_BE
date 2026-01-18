using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard.HOD
{
    public class CashFlowDto
    {
        public string Month { get; set; } // T{MM}/{YYYY}
        public int MonthNumber { get; set; }
        public int Year { get; set; }
        public decimal Invoiced { get; set; }
        public decimal Collected { get; set; }
        public decimal Outstanding { get; set; }
        public double CollectionRate { get; set; }
    }
}