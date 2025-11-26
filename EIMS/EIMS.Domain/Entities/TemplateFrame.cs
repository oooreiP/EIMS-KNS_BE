using System.ComponentModel.DataAnnotations;

namespace EIMS.Domain.Entities
{
    public class TemplateFrame
    {
        [Key]
        public int FrameID { get; set; }

        [Required]
        [StringLength(255)]
        public string FrameName { get; set; } = string.Empty; 

        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; } = string.Empty; 
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation Property: A frame can be used by many templates
        public virtual ICollection<InvoiceTemplate> InvoiceTemplates { get; set; } = new List<InvoiceTemplate>();
    }
}