using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard
{
    public class InvoiceCountDto
    {
        public int Total { get; set; }
        public int Paid { get; set; }
        public int Unpaid { get; set; }
        public int Overdue { get; set; }
        public int Cancelled { get; set; } 
    }
}