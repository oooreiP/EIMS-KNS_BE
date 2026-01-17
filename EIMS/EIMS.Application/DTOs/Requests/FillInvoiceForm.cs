using EIMS.Application.Features.Invoices.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Requests
{
    public class FillInvoiceForm
    {
        public BaseInvoiceCommand InvoiceData { get; set; }
        public int RequestId { get; set; }
    }
}
