using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.InvoiceStatement
{
    public class StatementSummaryResponse
    {
        public int StatementID { get; set; }
        public string StatementCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public DateTime StatementDate { get; set; }
        public string Period { get; set; } 
        public decimal OpeningBalance { get; set; } 
        public decimal NewCharges { get; set; }     
        public decimal PaidAmount { get; set; }    
        public decimal TotalAmount { get; set; }
        public int TotalInvoices { get; set; }
        public string Status { get; set; } = string.Empty;
        public int StatusID { get; set; } 
        public bool IsOverdue { get; set; } 
    }
}