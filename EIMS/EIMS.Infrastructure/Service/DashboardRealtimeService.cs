using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using Microsoft.AspNetCore.SignalR;

namespace EIMS.Infrastructure.Service
{
    public class DashboardRealtimeService : IDashboardRealtimeService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public DashboardRealtimeService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task NotifyDashboardChangedAsync(DashboardRealtimeEvent dashboardEvent, CancellationToken cancellationToken = default)
        {
            if (dashboardEvent.Roles != null && dashboardEvent.Roles.Length > 0)
            {
                var groups = dashboardEvent.Roles.Select(role => $"role:{role}").ToArray();
                return _hubContext.Clients.Groups(groups).SendAsync("DashboardChanged", dashboardEvent, cancellationToken);
            }

            return _hubContext.Clients.All.SendAsync("DashboardChanged", dashboardEvent, cancellationToken);
        }
    }
}
