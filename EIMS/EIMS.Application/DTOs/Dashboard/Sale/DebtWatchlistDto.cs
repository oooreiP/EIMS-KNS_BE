using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard.Sale
{
    public class DebtWatchlistDto
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal AmountPending { get; set; }
        public int OverdueDays { get; set; }
        public string UrgencyLevel { get; set; } // Critical, High, Medium
    }
}