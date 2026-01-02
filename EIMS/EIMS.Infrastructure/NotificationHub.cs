using DocumentFormat.OpenXml.Drawing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;

namespace EIMS.Infrastructure
{
    [Authorize]
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            // Có thể log user kết nối tại đây nếu cần
            await base.OnConnectedAsync();
        }
    }
}
