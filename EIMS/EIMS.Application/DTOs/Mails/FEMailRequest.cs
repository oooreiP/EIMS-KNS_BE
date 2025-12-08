using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Mails
{
    public class FEMailRequest
    {
        public string ToEmail { get; set; }
        public List<string> CcEmails { get; set; } = new();  
        public List<string> BccEmails { get; set; } = new(); 
        public string Subject { get; set; }
        public string EmailBody { get; set; }

        // Danh sách URL file đính kèm (Gộp cả PDF, XML và File ngoài vào đây)
        public List<string> AttachmentUrls { get; set; } = new();
    }
}
