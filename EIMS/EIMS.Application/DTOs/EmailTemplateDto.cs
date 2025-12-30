using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs
{
    public class EmailTemplateDto
    {
        public int EmailTemplateID { get; set; }
        public string TemplateCode { get; set; }
        public string LanguageCode { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string BodyContent { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystemTemplate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
