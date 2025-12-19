using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Invoices
{
    public class IssueInvoiceRequest
    {
        // ID người thực hiện (Nếu bạn chưa lấy từ Token)
        public int IssuerId { get; set; }

        // --- Các trường tùy chọn cho Thanh toán ---
        public bool AutoCreatePayment { get; set; } = false;
        public decimal? PaymentAmount { get; set; }
        public string? PaymentMethod { get; set; } // "Cash", "Transfer"...
        public string? Note { get; set; }
    }
}
