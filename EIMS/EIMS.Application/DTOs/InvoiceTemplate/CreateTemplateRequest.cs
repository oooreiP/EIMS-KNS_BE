using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.InvoiceTemplate
{
    public class CreateTemplateRequest
    {
        [Required]
        [StringLength(255)]
        public string TemplateName { get; set; } = string.Empty;

        [Required]
        public int SerialID { get; set; }

        [Required]
        public int TemplateTypeID { get; set; }
        [Required]
        public TemplateConfig LayoutDefinition { get; set; } = new TemplateConfig();
        [Required]
        public int TemplateFrameID { get; set; }
        public string? LogoUrl { get; set; }
    }
}