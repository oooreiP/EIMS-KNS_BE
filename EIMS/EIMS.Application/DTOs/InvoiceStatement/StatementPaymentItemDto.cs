using System;

namespace EIMS.Application.DTOs.InvoiceStatement
{
    public class StatementPaymentItemDto
    {
        public int StatementPaymentId { get; set; }
        public int PaymentId { get; set; }
        public int InvoiceId { get; set; }
        public long InvoiceNumber { get; set; }
        public decimal AppliedAmount { get; set; }
        public decimal InvoiceRemainingAfter { get; set; }
        public decimal StatementPaidAfter { get; set; }
        public decimal StatementBalanceAfter { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }
        public string? TransactionCode { get; set; }
        public string? Note { get; set; }
        public int? CreatedBy { get; set; }
    }
}
