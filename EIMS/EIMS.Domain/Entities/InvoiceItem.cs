using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EIMS.Domain.Entities
{
    public class InvoiceItem
    {
        [Key]
        public int InvoiceItemID { get; set; }

        [ForeignKey("Invoice")]
        public int InvoiceID { get; set; }
        [ForeignKey("OriginalInvoiceItem")]
        public int? OriginalInvoiceItemID { get; set; }

        [ForeignKey("Product")]
        public int ProductID { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public double Quantity { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal VATAmount { get; set; }
        public bool IsAdjustmentItem { get; set; } = false;

        // --- Navigation Properties ---
        [InverseProperty("InvoiceItems")]
        public virtual Invoice Invoice { get; set; }

        [InverseProperty("InvoiceItems")]
        public virtual Product Product { get; set; }
        public virtual InvoiceItem? OriginalInvoiceItem { get; set; }
    }
}