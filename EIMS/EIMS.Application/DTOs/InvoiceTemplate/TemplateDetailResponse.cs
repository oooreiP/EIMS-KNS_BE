using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.InvoiceTemplate
{
    public class TemplateDetailResponse
    {
        public int TemplateID { get; set; }
        public string TemplateName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int SerialID { get; set; }
        public string Serial { get; set; } = string.Empty; // e.g., "1C25TYY"
        public int TemplateTypeID { get; set; }
        public string TemplateTypeName { get; set; } = string.Empty;
        public int? TemplateFrameID { get; set; }
        public string? FrameUrl { get; set; }
        public string? LogoUrl { get; set; }
        public object LayoutDefinition { get; set; }
        public SellerInfo Seller { get; set; }
    }

}