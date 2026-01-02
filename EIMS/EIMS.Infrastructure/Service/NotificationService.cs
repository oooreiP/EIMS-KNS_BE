using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Infrastructure.Service
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _uow;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IUnitOfWork uow, IHubContext<NotificationHub> hubContext)
        {
            _uow = uow;
            _hubContext = hubContext;
        }

        public async Task SendToUserAsync(int userId, string content, int typeId = 1)
        {
            // 1. Lưu vào Database
            var noti = new Notification
            {
                UserID = userId,
                Content = content,
                NotificationStatusID = 1, // 1: Unread
                NotificationTypeID = typeId,
                Time = DateTime.UtcNow
            };

            await _uow.NotificationRepository.CreateAsync(noti);
            await _uow.SaveChanges();
            // 2. Bắn Real-time qua SignalR
            // Lưu ý: SignalR định danh user bằng ClaimTypes.NameIdentifier (thường là UserID dạng string)
            await _hubContext.Clients.User(userId.ToString())
                .SendAsync("ReceiveNotification", new
                {
                    id = noti.NotificationID,
                    content = noti.Content,
                    time = noti.Time,
                    typeId = noti.NotificationTypeID,
                    isRead = false
                });
        }

        public async Task SendToRoleAsync(string roleName, string content, int typeId = 1)
        {
            // 1. Tìm tất cả user thuộc Role đó
            // (Giả sử bạn có hàm lấy User theo Role, nếu chưa có thì viết thêm trong Repo)
            var users = await _uow.UserRepository.GetUsersByRoleAsync(roleName);

            if (users == null || !users.Any()) return;

            var notiList = new List<Notification>();

            // 2. Tạo Notification cho từng người
            foreach (var user in users)
            {
                notiList.Add(new Notification
                {
                    UserID = user.UserID,
                    Content = content,
                    NotificationStatusID = 1,
                    NotificationTypeID = typeId,
                    Time = DateTime.UtcNow
                });

                // Bắn SignalR luôn trong vòng lặp (hoặc gom lại bắn 1 lần nếu tối ưu sau)
                await _hubContext.Clients.User(user.UserID.ToString())
                    .SendAsync("ReceiveNotification", new { content, time = DateTime.UtcNow });
            }

            // 3. Lưu DB hàng loạt
            await _uow.NotificationRepository.CreateRangeAsync(notiList);
            await _uow.SaveChanges();
        }
        public async Task SendRealTimeAsync(int userId, string content, int typeId)
        {
            await _hubContext.Clients.User(userId.ToString())
                .SendAsync("ReceiveNotification", new
                {
                    content = content,
                    time = DateTime.UtcNow,
                    typeId = typeId,
                    isRead = false
                });
        }
    }
}
