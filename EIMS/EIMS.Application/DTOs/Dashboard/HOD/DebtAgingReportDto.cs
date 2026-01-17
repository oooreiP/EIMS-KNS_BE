using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard.HOD
{
    public class DebtAgingReportDto
    {
        public DebtBucketDto WithinDue { get; set; } = new();
        public DebtBucketDto Overdue1To30 { get; set; } = new();
        public DebtBucketDto Overdue31To60 { get; set; } = new();
        public DebtBucketDto CriticalOverdue60Plus { get; set; } = new();
    }
}