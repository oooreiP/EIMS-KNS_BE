using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.TaxAPIDTO
{
    public class SubmitInvoiceRequest
    {
        public int InvoiceId { get; set; }
        public int MessageCodeId { get; set; }
        public int DataCount { get; set; }
    }
}
