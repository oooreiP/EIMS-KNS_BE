using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EIMS.Domain.Entities
{
    public class InvoiceTemplate
    {
        [Key]
        public int TemplateID { get; set; }
        [Required]
        [StringLength(255)]
        public string TemplateName { get; set; } = string.Empty;
        [ForeignKey("TemplateTypeID")]
        public int TemplateTypeID { get; set; }
        [ForeignKey("SerialID")]
        public int SerialID { get; set; }
        public string? LayoutDefinition { get; set; }
        public bool IsActive { get; set; } = true;
        [ForeignKey("CreatedByUserID")]
        public int CreatedByUserID { get; set; }

        // --- Navigation Properties ---
        [InverseProperty("InvoiceTemplates")]
        public virtual TemplateType TemplateType { get; set; }
        [InverseProperty("Template")]
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        [InverseProperty("CreatedTemplates")]
        public virtual User CreatedBy { get; set; }
        [InverseProperty("InvoiceTemplates")]
        public virtual Serial Serial { get; set; }
    }
}