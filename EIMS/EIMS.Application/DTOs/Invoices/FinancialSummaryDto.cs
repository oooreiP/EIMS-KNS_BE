using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Invoices
{
    public class FinancialSummaryDto
    {
        // Cột 1: Gốc
        public decimal OriginalTotalAmount { get; set; }
        public decimal OriginalVATAmount { get; set; }

        // Cột 2: Điều chỉnh (Có thể ÂM)
        public decimal AdjustmentTotalAmount { get; set; }
        public decimal AdjustmentVATAmount { get; set; }

        // Cột 3: Cuối
        public decimal FinalTotalAmount { get; set; }
        public decimal FinalVATAmount { get; set; }
    }
}
