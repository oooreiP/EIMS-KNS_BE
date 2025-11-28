using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [ForeignKey("TemplateFrameID")]
        public int TemplateFrameID { get; set; }
        [StringLength(500)]
        public string? LogoUrl { get; set; } // Nullable, stores Cloudinary URL for the logo
        [Column(TypeName = "jsonb")]
        public string? LayoutDefinition { get; set; }
        public bool IsActive { get; set; } = true;
        [ForeignKey("CreatedByUserID")]
        public int CreatedByUserID { get; set; }

        // --- Navigation Properties ---
        [InverseProperty("InvoiceTemplates")]
        public virtual TemplateType TemplateType { get; set; }
        [JsonIgnore]
        [InverseProperty("Template")]
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        [InverseProperty("CreatedTemplates")]
        public virtual User CreatedBy { get; set; }
        [InverseProperty("InvoiceTemplates")]
        public virtual Serial Serial { get; set; }
        [InverseProperty("InvoiceTemplates")]
        public virtual TemplateFrame? TemplateFrame { get; set; }
    }
}