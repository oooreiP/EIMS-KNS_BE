using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.InvoiceTemplate
{
    public class TemplateSummaryResponse
    {
        public int TemplateID { get; set; }
        public string TemplateName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string Serial { get; set; } = string.Empty; // e.g., "1C25TYY"
        public string TemplateTypeName { get; set; } = string.Empty;
    }
}