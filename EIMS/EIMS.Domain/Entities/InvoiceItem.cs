using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EIMS.Domain.Entities
{
    public class InvoiceItem
    {
        [Key]
        public int InvoiceItemID { get; set; }
        [ForeignKey("InvoiceID")]
        public int InvoiceID { get; set; }
        [ForeignKey("CategoryID")]
        public int CategoryID { get; set; }
        [Required]
        [StringLength(500)]
        public string ItemName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Unit { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal VATAmount { get; set; }
        //navigations
        [InverseProperty("InvoiceItems")]
        public virtual Invoice Invoice { get; set; }
        [InverseProperty("InvoiceItems")]
        public virtual Category Category { get; set; }
    }
}