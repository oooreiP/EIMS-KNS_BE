using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Payments
{
    public class DebtSummaryDTO
    {
        public decimal TotalReceivable { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalRemaining { get; set; }
        public decimal TotalOverdue { get; set; }
    }
}
