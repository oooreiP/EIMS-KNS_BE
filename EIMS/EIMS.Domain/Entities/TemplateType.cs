using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EIMS.Domain.Entities
{
    public class TemplateType
    {
        [Key]
        public int TemplateTypeID { get; set; }
        [Required]
        [StringLength(100)]
        public string TypeName { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string TypeCategory { get; set; } = string.Empty;

        // --- Navigation Properties ---
        [InverseProperty("TemplateType")]
        public virtual ICollection<InvoiceTemplate> InvoiceTemplates { get; set; } = new List<InvoiceTemplate>();
    }
}