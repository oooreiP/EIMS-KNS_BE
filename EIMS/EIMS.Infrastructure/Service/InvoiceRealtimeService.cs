using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EIMS.Infrastructure.Service
{
    public class InvoiceRealtimeService : IInvoiceRealtimeService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ILogger<InvoiceRealtimeService> _logger;

        public InvoiceRealtimeService(IHubContext<NotificationHub> hubContext, ILogger<InvoiceRealtimeService> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        public Task NotifyInvoiceChangedAsync(InvoiceRealtimeEvent invoiceEvent, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Invoice realtime event: {ChangeType} | InvoiceId: {InvoiceId} | StatusId: {StatusId} | Roles: {Roles}",
                invoiceEvent.ChangeType,
                invoiceEvent.InvoiceId,
                invoiceEvent.StatusId,
                invoiceEvent.Roles == null ? "<none>" : string.Join(",", invoiceEvent.Roles));

            if (invoiceEvent.Roles != null && invoiceEvent.Roles.Length > 0)
            {
                var groups = invoiceEvent.Roles.Select(role => $"role:{role}").ToArray();
                return _hubContext.Clients.Groups(groups).SendAsync("InvoiceChanged", invoiceEvent, cancellationToken);
            }

            return _hubContext.Clients.All.SendAsync("InvoiceChanged", invoiceEvent, cancellationToken);
        }
    }
}
