using System;
using System.Collections.Generic;

namespace EIMS.Application.DTOs.Dashboard.Accountant
{
    public class AccountantInvoiceRequestStatsDto
    {
        public int PendingCount { get; set; }
        public int ProcessedCount { get; set; }
        public int RejectedCount { get; set; }
        public int TotalThisMonth { get; set; }
        public List<AccountantInvoiceRequestRecentDto> RecentRequests { get; set; } = new();
    }

    public class AccountantInvoiceRequestRecentDto
    {
        public int RequestId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public int DaysWaiting { get; set; }
    }
}
