using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard.Accountant
{
    public class TaskQueueItemDto
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; } // "High", "Medium", "Low"
        public string TaskType { get; set; } // "Rejected", "Draft", "Overdue"
        public DateTime TaskDate { get; set; } // Ngày dùng để sort
        public string? Reason { get; set; }    // Chỉ cho Priority High
    }
}