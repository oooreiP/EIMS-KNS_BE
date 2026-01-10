using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
    public class InvoiceErrorNotification
    {
        [Key]
        [Column("InvoiceErrorNotificationID")]
        public int InvoiceErrorNotificationID { get; set; }

        // --- Thông tin chung của tờ khai ---
        public string? TaxAuthorityCode { get; set; } = "10925";
        public string Place { get; set; } = "TP. Hồ Chí Minh"; // Địa danh làm báo cáo
        public DateTime ReportDate { get; set; } = DateTime.UtcNow; // Ngày lập báo cáo

        // --- Trạng thái vòng đời ---
        // 1: Nháp (Draft), 2: Đã ký (Signed), 3: Đã gửi T-VAN (Sent), 4: CQT Tiếp nhận (Accepted), 5: CQT Từ chối (Rejected)
        public int Status { get; set; }

        // --- Lưu trữ file ---
        public string? XMLPath { get; set; } // Link file XML chưa ký/đã ký lưu trên Cloud
        public string? SignedData { get; set; } // Chuỗi chữ ký số (SignatureValue) nếu cần tách riêng

        // --- Thông tin phản hồi từ Thuế (Sau khi gửi) ---
        public string? MTDiep { get; set; } // Mã thông điệp (quan trọng để tra cứu trạng thái)

        // Audit info
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int CreatedBy { get; set; }

        // --- Navigation ---
        [InverseProperty("Notification")]
        public virtual ICollection<InvoiceErrorDetail> Details { get; set; }
    }
}
