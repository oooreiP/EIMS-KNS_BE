using System.Threading;
using System.Threading.Tasks;
using EIMS.Application.Commons.Models;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IUserRealtimeService
    {
        Task NotifyUserChangedAsync(UserRealtimeEvent userEvent, CancellationToken cancellationToken = default);
    }
}
