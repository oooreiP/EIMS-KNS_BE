using System.Threading;
using System.Threading.Tasks;
using EIMS.Application.Commons.Models;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IInvoiceRealtimeService
    {
        Task NotifyInvoiceChangedAsync(InvoiceRealtimeEvent invoiceEvent, CancellationToken cancellationToken = default);
    }
}
