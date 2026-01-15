using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
    public class InvoiceErrorDetail
    {
        [Key]
        public int DetailID { get; set; }
        public int NotificationID { get; set; }
        public int? InvoiceID { get; set; }
        public string InvoiceSerial { get; set; } // Ký hiệu (K24TAA)
        public string InvoiceNumber { get; set; } // Số hóa đơn (0000123)
        public DateTime InvoiceDate { get; set; }  // Ngày lập hóa đơn      
        public string InvoiceTaxCode { get; set; }       

        // --- Nội dung giải trình ---
        // 1: Hủy, 2: Điều chỉnh, 3: Thay thế, 4: Giải trình sai sót khác
        public int ErrorType { get; set; }
        public string Reason { get; set; } // Lý do sai sót

        // --- Navigation ---
        [ForeignKey("NotificationID")]
        public virtual InvoiceErrorNotification Notification { get; set; }
        [ForeignKey("InvoiceID")]
        public virtual Invoice? Invoice { get; set; }
    }
}
