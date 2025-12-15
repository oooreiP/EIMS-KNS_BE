using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Customer
{
    public class DebtSummaryDto
    {
        public decimal TotalDebt { get; set; }
        public decimal OverdueDebt { get; set; }
        public decimal TotalPaid { get; set; }
        public int InvoiceCount { get; set; }
        public int UnpaidInvoiceCount { get; set; }
        public DateTime? LastPaymentDate { get; set; }
    }
}