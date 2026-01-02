using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs
{
    public class NotificationDto
    {
        public int NotificationID { get; set; }
        public string Content { get; set; }
        public string StatusName { get; set; } // "Chưa đọc", "Đã đọc"
        public bool IsRead { get; set; }       // Helper field cho FE
        public string TypeName { get; set; }   // "System", "Invoice", "Payment"
        public DateTime Time { get; set; }
    }
}
