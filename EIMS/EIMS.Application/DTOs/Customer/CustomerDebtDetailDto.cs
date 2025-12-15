using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs.InvoicePayment;
using EIMS.Application.DTOs.Invoices;

namespace EIMS.Application.DTOs.Customer
{
    public class CustomerDebtDetailDto
    {
        public CustomerInfoDto Customer { get; set; }
        public DebtSummaryDto Summary { get; set; }
        public List<UnpaidInvoiceDto> UnpaidInvoices { get; set; }
        public List<PaymentHistoryDto> PaymentHistory { get; set; }
    }
}