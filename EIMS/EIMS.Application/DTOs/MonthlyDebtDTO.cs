using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs
{
    public class MonthlyDebtDTO
    {
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; } 
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }      
        public decimal PaidAmount { get; set; }      
        public decimal RemainingAmount { get; set; } 
        public decimal OverdueAmount { get; set; }    
        public string Status { get; set; }           
    }
}
