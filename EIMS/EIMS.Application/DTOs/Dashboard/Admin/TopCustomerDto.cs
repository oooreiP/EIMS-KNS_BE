using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard.Admin
{
    public class TopCustomerDto
    {
        public string CustomerName { get; set; }
        public int InvoiceCount { get; set; }
        public decimal TotalSpent { get; set; }
    }
}