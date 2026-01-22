using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.InvoiceStatement
{
    public class StatementDetailResponse
    {
        public int StatementID { get; set; }
        public string StatementCode { get; set; }
        public string CustomerName { get; set; }
        public DateTime StatementDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public string Period { get; set; } 
        public decimal OpeningBalance { get; set; } 
        public decimal NewCharges { get; set; }     
        public decimal PaidAmount { get; set; } 
        public string Status { get; set; } 

        // The list of invoices in this statement
        public List<StatementProductDto> ProductSummaries { get; set; } = new();
        public List<StatementInvoiceDto> Invoices { get; set; } = new List<StatementInvoiceDto>();
    }
}