using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Domain.Constants
{
    public static class InvoiceActionTypes
    {
        public const string Created = "Created";           // Tạo mới (Nháp)
        public const string Updated = "Updated";           // Cập nhật thông tin
        public const string Signed = "Signed";             // Ký số thành công
        public const string Issued = "Issued";             // Phát hành
        public const string PaymentAdded = "PaymentAdded"; // Thanh toán
        public const string SentToCQT = "SentToCQT";       // Đã gửi CQT
        public const string CqtAccepted = "CqtAccepted";   // CQT Chấp nhận (Cấp mã)
        public const string CqtRejected = "CqtRejected";   // CQT Từ chối
        public const string Cancelled = "Cancelled";       // Hủy bỏ
        public const string Replaced = "Replaced";         // Bị thay thế
        public const string Replacement = "Replacement";   // Là hóa đơn thay thế
        public const string Adjusted = "Adjusted";         // Bị điều chỉnh
        public const string Adjustment = "Adjustment";     // Là hóa đơn điều chỉnh
    }
}
