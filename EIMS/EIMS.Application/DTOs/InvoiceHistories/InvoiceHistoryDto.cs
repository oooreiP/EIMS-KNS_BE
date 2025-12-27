using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.InvoiceHistories
{
    public class InvoiceHistoryDto
    {
        public int HistoryID { get; set; }
        public int InvoiceID { get; set; }
        public string ActionType { get; set; } 
        public DateTime Date { get; set; }
        public int? PerformedBy { get; set; }
        public string PerformerName { get; set; } 
        public int? ReferenceInvoiceID { get; set; }
        public string? ReferenceInvoiceNumber { get; set; } 
    }
}
