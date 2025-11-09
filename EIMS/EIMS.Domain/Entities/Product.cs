using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace EIMS.Domain.Entities
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        [StringLength(100)]
        public string? Unit { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BasePrice { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? VATRate { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public bool? IsActive { get; set; } = true;
        [Column(TypeName = "timestamp with time zone")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        // --- Navigation ---
        [JsonIgnore]
        public virtual Category Category { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
    }
}
