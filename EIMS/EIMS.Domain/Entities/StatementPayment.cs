using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EIMS.Domain.Entities
{
    public class StatementPayment
    {
        [Key]
        public int StatementPaymentID { get; set; }

        [ForeignKey("Statement")]
        public int StatementID { get; set; }

        [ForeignKey("Payment")]
        public int PaymentID { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal AppliedAmount { get; set; }

        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("CreatedBy")]
        public int? CreatedBy { get; set; }

        [InverseProperty("StatementPayments")]
        public virtual InvoiceStatement Statement { get; set; }

        [InverseProperty("StatementPayments")]
        public virtual InvoicePayment Payment { get; set; }
    }
}
