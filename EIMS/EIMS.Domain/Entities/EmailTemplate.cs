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
        [StringLength(255)]
        public string Subject { get; set; } // Tiêu đề email (có chứa placeholder)

        [Required]
        public string BodyContent { get; set; } // Nội dung HTML (có chứa placeholder)

        public string? Description { get; set; } // Mô tả cho Admin dễ quản lý

        public bool IsActive { get; set; } = true;
    }
}
