using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
   public class InvoicePayment
    {
        [Key]
        public int PaymentID { get; set; }

        [ForeignKey("InvoiceID")]
        public int InvoiceID { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal AmountPaid { get; set; } // The money received in this specific transaction

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        [StringLength(50)]
        public string? PaymentMethod { get; set; } // e.g., "Bank Transfer", "Cash", "VietQR"

        [StringLength(100)]
        public string? TransactionCode { get; set; } // e.g., "FT231200001" (Banking Ref)

        [StringLength(500)]
        public string? Note { get; set; } // e.g., "Deposit 30%"

        [ForeignKey("CreatedBy")]
        public int? CreatedBy { get; set; } // User who recorded this payment

        // --- Navigation Properties ---
        [InverseProperty("Payments")]
        public virtual Invoice Invoice { get; set; }
    }
}