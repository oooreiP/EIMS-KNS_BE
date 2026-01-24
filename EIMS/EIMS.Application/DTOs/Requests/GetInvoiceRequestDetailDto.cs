using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Requests
{
    public class GetInvoiceRequestDetailDto
    {
        public int RequestID { get; set; }
        public int? CreatedInvoiceId { get; set; }
        public string StatusName { get; set; }
        public string CustomerName { get; set; }
        public string SaleName { get; set; }
        public decimal TotalAmount { get; set; }
        public string TotalAmountInWords { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? RejectReason { get; set; }
        public string? EvidenceFilePath { get; set; }
        public List<InvoiceRequestItemDto> Items { get; set; } = new();
    }
}
