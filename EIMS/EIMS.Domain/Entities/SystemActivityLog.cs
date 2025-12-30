using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
    public class SystemActivityLog
    {
        [Key]
        public int LogId { get; set; }
        public string? UserId { get; set; }
        public string? ActionName { get; set; } 
        public string? Description { get; set; } // VD: "Đăng nhập thành công", "Ký hóa đơn số 001"
        public string? IpAddress { get; set; }
        public string? Status { get; set; } // Success/Failed
        public DateTime Timestamp { get; set; }
        public string? TraceId { get; set; }
    }
}
