using System;

namespace EIMS.Application.Commons.Models
{
    public class InvoiceRealtimeEvent
    {
        public int InvoiceId { get; set; }
        public string ChangeType { get; set; } = "Updated";
        public int? CompanyId { get; set; }
        public int? CustomerId { get; set; }
        public int? StatusId { get; set; }
        public string[]? Roles { get; set; }
        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
    }
}
