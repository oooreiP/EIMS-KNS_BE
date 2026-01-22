using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.InvoicePayment
{
    public class InvoicePaymentDetailDTO
    {
        // --- 1. Thông tin giao dịch thanh toán (Giữ nguyên) ---
        public int PaymentID { get; set; }
        public decimal AmountPaid { get; set; }      // Số tiền vừa trả lần này
        public DateTime? PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string? TransactionCode { get; set; }
        public string? Note { get; set; }
        public int? CreatedBy { get; set; }

        public int InvoiceID { get; set; }
        public string? InvoiceCode { get; set; }     // VD: INV-2024-001 (Quan trọng để hiển thị)
        public string? CustomerName { get; set; }    // Tên khách hàng (Quan trọng để đối chiếu)

        public decimal TotalInvoiceAmount { get; set; } // Tổng tiền phải thu của hóa đơn gốc
        public decimal TotalPaidAmount { get; set; }    // Tổng tiền ĐÃ trả (bao gồm cả lần này)
        public decimal RemainingAmount { get; set; }    // Số tiền CÒN LẠI (Dư nợ hiện tại)
        public string PaymentStatus { get; set; }       // Trạng thái mới nhất: "Đã thanh toán", "Thanh toán 1 phần", "Quá hạn"...
    }
}
