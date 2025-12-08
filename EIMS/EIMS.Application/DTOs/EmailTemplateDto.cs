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
        public string TemplateCode { get; set; } // VD: INVOICE_SEND
        public string LanguageCode { get; set; } // vi / en
        public string Subject { get; set; }
        public string Description { get; set; }
        public string BodyContent { get; set; }  // HTML
        public bool IsActive { get; set; }
    }
}
