using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard.Accountant
{
    public class AccountantDashboardDto
    {
        public CurrentUserDto CurrentUser { get; set; } = new();
        public AccountantKpiDto Kpis { get; set; } = new();
        public List<TaskQueueItemDto> TaskQueue { get; set; } = new();
        public List<RecentWorkDto> RecentInvoices { get; set; } = new();
        public DateTime GeneratedAt { get; set; }
    }
}