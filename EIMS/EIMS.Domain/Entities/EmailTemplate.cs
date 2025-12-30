using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
    public class EmailTemplate
    {
        [Key]
        public int EmailTemplateID { get; set; }

        [Required]
        [StringLength(50)]
        public string TemplateCode { get; set; } // VD: "INVOICE_SEND", "INVOICE_CANCEL"

        [Required]
        [StringLength(10)]
        public string LanguageCode { get; set; } = "vi"; // "vi" hoặc "en"
        [Required]
        [StringLength(20)]
        public string Category { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string Subject { get; set; } // Tiêu đề email (có chứa placeholder)

        [Required]
        public string BodyContent { get; set; } // Nội dung HTML (có chứa placeholder)

        public bool IsSystemTemplate { get; set; } = false; // [cite: 8] Mặc định false

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // [cite: 9]
        public DateTime? UpdatedAt { get; set; } // [cite: 9]
    }
}
