using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Invoices
{
    public class AdjustmentInvoiceDTO
    {
        public int OriginalInvoiceId { get; set; }
        public int? TemplateId { get; set; }
        //public string ReferenceText { get; set; }
        public string? AdjustmentReason { get; set; }
        public List<InvoiceItemInputDto> AdjustmentItems { get; set; } = new();
    }
}
