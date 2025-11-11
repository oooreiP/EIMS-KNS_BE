using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Mails
{
    public class MailRequest
    {
        public string Email { get; set; } = string.Empty;         
        public string Subject { get; set; } = string.Empty;       
        public string EmailBody { get; set; } = string.Empty;     
        public List<string?> CloudinaryUrls { get; set; }         
    }
}
