using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Domain.Enums
{
    public enum EAdjustmentType
    {
        // 0: Không xác định (Dùng để catch lỗi nếu quên set)
        None = 0,

        // 1: Điều chỉnh TĂNG (VD: Viết thiếu tiền, giờ viết thêm)
        Increase = 1,

        // 2: Điều chỉnh GIẢM (VD: Viết dư tiền, trả lại hàng)
        Decrease = 2
    }
}
