using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard.Accountant
{
    public class AccountantDashboardDto
    {
        public CurrentUserDto CurrentUser { get; set; } = new();
        public AccountantOverviewStatsDto OverviewStats { get; set; } = new();
        public AccountantKpiDto Kpis { get; set; } = new();
        public AccountantInvoiceRequestStatsDto InvoiceRequestStats { get; set; } = new();
        public AccountantTotalMonthlyDebtDto TotalMonthlyDebt { get; set; } = new();
        public List<TaskQueueItemDto> TaskQueue { get; set; } = new();
        public List<RecentWorkDto> RecentInvoices { get; set; } = new();
        public int TaskQueueTotal { get; set; }
        public int RecentInvoicesTotal { get; set; }
        public DateTime GeneratedAt { get; set; }
    }
}