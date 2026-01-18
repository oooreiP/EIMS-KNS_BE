using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard.HOD
{
    public class PendingInvoiceDto
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Priority { get; set; } // High, Medium, Low
        public double HoursWaiting { get; set; }
        public int DaysWaiting { get; set; }
        public int InvoiceType { get; set; } // 1: Gốc, 2: Điều chỉnh, 3: Thay thế, ...
        public string TypeName { get; set; }
        
        public string TypeColor { get; set; } // Mã màu Hex
        public string TypeIcon { get; set; }  // Tên Icon
        public string Reason { get; set; }     // Nội dung lý do
        public string ReasonType { get; set; } // Loại lý do (adjustment, cancellation...)
        public string OriginalInvoiceNumber { get; set; }
        public string TypeBackgroundColor { get; set; }
    }
}