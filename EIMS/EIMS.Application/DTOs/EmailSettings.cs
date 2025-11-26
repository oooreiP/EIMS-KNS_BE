using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs
{
    public class EmailSettings
    {
        public string ApiToken { get; set; } = string.Empty;
        public string FromEmail { get; set; } = string.Empty; // Must be the verified domain email from MailerSend
        public string FromName { get; set; } = "EIMS System";
    }
}