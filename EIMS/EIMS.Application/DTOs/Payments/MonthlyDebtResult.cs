using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Payments
{
    public class MonthlyDebtResult
    {
        public DebtSummaryDTO Summary { get; set; }
        public PaginatedList<MonthlyDebtDTO> Invoices { get; set; }
    }
}
