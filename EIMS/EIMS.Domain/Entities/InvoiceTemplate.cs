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
        public int InvoiceTypeID { get; set; }
        [ForeignKey("InvoiceTypeID")]
        public int SerialID { get; set; }
        [ForeignKey("SerialID")]
        public long CurrentInvoiceNumber { get; set; } = 0;
        public string? LayoutDefinition { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedByID { get; set; }
        [ForeignKey("CreatedByID")]

        // --- Navigation Properties ---
        [InverseProperty("TemplateType")]
        public virtual TemplateType TemplateType { get; set; }
        [InverseProperty("Invoices")]
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        [InverseProperty("CreatedBy")]
        public virtual User CreatedBy { get; set; }
        [InverseProperty("Serial")]
        public virtual Serial Serial { get; set; }
    }
}