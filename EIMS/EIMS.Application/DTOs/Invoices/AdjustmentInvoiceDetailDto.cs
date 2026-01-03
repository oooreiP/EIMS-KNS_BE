using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Invoices
{
    public class AdjustmentInvoiceDetailDto
    {
        public int InvoiceId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ReferenceText { get; set; } // Lý do điều chỉnh
        public int AdjustmentType { get; set; }   // 1: Tăng, 2: Giảm (Enum)

        // Cục 1: Danh sách hàng hóa (3 cột số liệu)
        public List<AdjustmentItemDto> Items { get; set; } = new();

        // Cục 2: Tổng kết tài chính (3 cột số liệu)
        public FinancialSummaryDto FinancialSummary { get; set; }
    }
}
