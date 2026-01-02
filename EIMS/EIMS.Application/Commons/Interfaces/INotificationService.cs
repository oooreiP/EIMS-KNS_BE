using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Interfaces
{
    public interface INotificationService
    {
        // Gửi cho 1 người cụ thể
        Task SendToUserAsync(int userId, string content, int typeId = 1);

        // Gửi cho một nhóm Role (VD: Gửi cho tất cả Kế toán trưởng)
        Task SendToRoleAsync(string roleName, string content, int typeId = 1);
        // Dùng cho trường hợp Bulk Insert (Lưu DB 1 cục ở cuối)
        Task SendRealTimeAsync(int userId, string content, int typeId);
    }
}
