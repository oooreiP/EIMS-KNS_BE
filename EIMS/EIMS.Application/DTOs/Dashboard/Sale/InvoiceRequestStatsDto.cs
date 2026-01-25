using System;
using System.Collections.Generic;

namespace EIMS.Application.DTOs.Dashboard.Sale
{
    public class InvoiceRequestStatsDto
    {
        public int PendingCount { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }
        public int IssuedCount { get; set; }
        public List<InvoiceRequestRecentDto> RecentRequests { get; set; } = new();
    }

    public class InvoiceRequestRecentDto
    {
        public int RequestId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
    }
}
