using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.InvoiceTemplate
{
    public class UpdateTemplateRequest
    {
        [Required]
        public int TemplateID { get; set; }

        [Required, StringLength(255)]
        public string TemplateName { get; set; } = string.Empty;

        [Required]
        public object LayoutDefinition { get; set; }
        public int TemplateFrameID { get; set; } // ID of the background frame
        public string? LogoUrl { get; set; }      // Cloudinary URL
        public bool IsActive { get; set; }
    }
}