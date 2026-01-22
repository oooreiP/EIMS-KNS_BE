using System.Threading;
using System.Threading.Tasks;
using EIMS.Application.Commons.Models;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IDashboardRealtimeService
    {
        Task NotifyDashboardChangedAsync(DashboardRealtimeEvent dashboardEvent, CancellationToken cancellationToken = default);
    }
}
