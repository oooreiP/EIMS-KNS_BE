using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EIMS.Domain.Entities
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        [StringLength(50)]
        public string? Code { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        [StringLength(500)]
        public string? Description { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? VATRate { get; set; }
        [StringLength(50)]
        public string? CategoryType { get; set; }
        public bool? IsTaxable { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }

        // --- Navigation Properties ---
        [InverseProperty("Category")]
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
    }
}