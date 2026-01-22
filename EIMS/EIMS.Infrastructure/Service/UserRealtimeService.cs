using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace EIMS.Infrastructure.Service
{
    public class UserRealtimeService : IUserRealtimeService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ILogger<UserRealtimeService> _logger;

        public UserRealtimeService(IHubContext<NotificationHub> hubContext, ILogger<UserRealtimeService> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public Task NotifyUserChangedAsync(UserRealtimeEvent userEvent, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("User realtime event: {ChangeType} | UserId: {UserId} | RoleName: {RoleName} | IsActive: {IsActive} | Roles: {Roles}",
                userEvent.ChangeType,
                userEvent.UserId,
                userEvent.RoleName,
                userEvent.IsActive,
                userEvent.Roles == null ? "<none>" : string.Join(",", userEvent.Roles));
            if (userEvent.Roles != null && userEvent.Roles.Length > 0)
            {
                var groups = userEvent.Roles.Select(role => $"role:{role}").ToArray();
                return _hubContext.Clients.Groups(groups).SendAsync("UserChanged", userEvent, cancellationToken);
            }
            return _hubContext.Clients.All.SendAsync("UserChanged", userEvent, cancellationToken);
        }
    }
}
