using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Invoices
{
    public class AdjustmentItemDto
    {
        public string ProductName { get; set; }
        public string Unit { get; set; }

        // Cột 1: Gốc (Lấy từ HĐ cũ)
        public double OriginalQuantity { get; set; }
        public decimal OriginalUnitPrice { get; set; }
        public decimal OriginalAmount { get; set; }

        // Cột 2: Điều chỉnh (Lấy từ HĐ hiện tại - Có thể ÂM)
        public double AdjustmentQuantity { get; set; }
        public decimal AdjustmentUnitPrice { get; set; }
        public decimal AdjustmentAmount { get; set; }

        // Cột 3: Cuối (Tính toán: Gốc + Điều chỉnh)
        public double FinalQuantity { get; set; }
        public decimal FinalUnitPrice { get; set; }
        public decimal FinalAmount { get; set; }
    }
}
