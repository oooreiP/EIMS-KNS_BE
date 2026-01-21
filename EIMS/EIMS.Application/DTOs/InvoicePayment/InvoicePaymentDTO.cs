using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.InvoicePayment
{
    public class InvoicePaymentDTO
    {
        public int PaymentID { get; set; }
        public int InvoiceID { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal? RemainingAmount { get; set; }
        public string? PaymentMethod { get; set; }
        public string? TransactionCode { get; set; }
        public string? Note { get; set; }
        public DateTime? PaymentDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}