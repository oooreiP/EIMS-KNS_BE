using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard.Accountant
{
    public class AccountantKpiDto
    {
        public int RejectedCount { get; set; }   // 7 ngày qua
        public int DraftsCount { get; set; }     // 30 ngày qua
        public int SentToday { get; set; }       // Update hôm nay
        public decimal TotalMonthlyRevenue { get; set; }
        public decimal LastMonthRevenue { get; set; }
        public double RevenueGrowthPercent { get; set; }
        public int PendingApproval { get; set; } // HĐ chờ duyệt
        public int UrgentTasks { get; set; }     // Task > 24h
    }
}