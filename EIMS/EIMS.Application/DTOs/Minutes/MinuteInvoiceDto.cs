using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Minutes
{
    public class MinuteInvoiceDto
    {
        public int MinuteInvoiceId { get; set; }
        public int InvoiceId { get; set; }
        public string MinuteCode { get; set; }       // Số biên bản
        public string InvoiceNo { get; set; }        // Số hóa đơn gốc
        public string CustomerName { get; set; }     // Tên khách hàng
        public string MinuteType { get; set; }       // Điều chỉnh / Thay thế
        public string Status { get; set; }           // Nháp / Chờ ký...
        public string Description { get; set; }
        public string FilePath { get; set; }         // Link file PDF
        public DateTime CreatedAt { get; set; }
        public string CreatedByName { get; set; }    // Tên người tạo

        // Trạng thái ký
        public bool IsSellerSigned { get; set; }
        public bool IsBuyerSigned { get; set; }
    }
}
