using System.Collections.Generic;
using EIMS.Application.DTOs.InvoicePayment;

namespace EIMS.Application.DTOs.InvoiceStatement
{
    public class StatementPaymentResultDto
    {
        public int StatementId { get; set; }
        public decimal AppliedAmount { get; set; }
        public decimal StatementPaidAmount { get; set; }
        public decimal StatementBalanceDue { get; set; }
        public int StatementStatusId { get; set; }
        public List<InvoicePaymentDTO> Payments { get; set; } = new();
    }
}
