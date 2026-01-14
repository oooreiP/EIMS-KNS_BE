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
        public string NotificationNumber { get; set; } = string.Empty;
        public string NotificationType { get; set; } = string.Empty;
        public string TaxAuthorityName { get; set; } = string.Empty;
        public string? TaxAuthorityCode { get; set; } = "10925";
        public string Place { get; set; } = "TP. Hồ Chí Minh"; // Địa danh làm báo cáo
        public DateTime ReportDate { get; set; } = DateTime.UtcNow; // Ngày lập báo cáo
        // 1: Nháp (Draft), 2: Đã ký (Signed), 3: Đã gửi T-VAN (Sent), 4: CQT Tiếp nhận (Accepted), 5: CQT Từ chối (Rejected)
        public int Status { get; set; }
        public string? XMLPath { get; set; } 
        public string? SignedData { get; set; } 
        public string? MTDiep { get; set; } // Mã thông điệp (quan trọng để tra cứu trạng thái)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int CreatedBy { get; set; }

        // --- Navigation ---
        [InverseProperty("Notification")]
        public virtual ICollection<InvoiceErrorDetail> Details { get; set; }
    }
}
