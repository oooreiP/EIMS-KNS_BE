using System;

namespace EIMS.Application.Commons.Models
{
    public class DashboardRealtimeEvent
    {
        public string Scope { get; set; } = "Invoices"; // Invoices | Users | Dashboard
        public string ChangeType { get; set; } = "Updated";
        public int? EntityId { get; set; }
        public string[]? Roles { get; set; }
        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
    }
}
