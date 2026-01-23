using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Domain.Enums
{
    public enum  EMinuteStatus
    {
        Pending = 1,    // Chờ ký
        Signed = 2,     // Đã ký đầy đủ
        Sent = 3,     // Đã gui
        Complete = 4,     // Hai ben dong thuan
        Cancelled = 5   // Đã hủy
    }
}
