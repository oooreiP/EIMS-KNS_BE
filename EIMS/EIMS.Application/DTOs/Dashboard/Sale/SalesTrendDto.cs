using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard.Sale
{
    public class SalesTrendDto
    {
        public string Month { get; set; } // Display Name (e.g. "Jan 2026")
        public int MonthNumber { get; set; }
        public int Year { get; set; }
        public decimal Revenue { get; set; }
        public int InvoiceCount { get; set; }
        public decimal CommissionEarned { get; set; }
    }
}