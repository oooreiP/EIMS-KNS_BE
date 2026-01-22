using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;

namespace EIMS.Infrastructure
{
    [Authorize]
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var roleClaims = Context.User?.Claims
                .Where(c => c.Type == ClaimTypes.Role || c.Type == "role")
                .Select(c => c.Value)
                .Distinct()
                .ToList();

            if (roleClaims != null)
            {
                foreach (var role in roleClaims)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, $"role:{role}");
                }
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var roleClaims = Context.User?.Claims
                .Where(c => c.Type == ClaimTypes.Role || c.Type == "role")
                .Select(c => c.Value)
                .Distinct()
                .ToList();

            if (roleClaims != null)
            {
                foreach (var role in roleClaims)
                {
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"role:{role}");
                }
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
