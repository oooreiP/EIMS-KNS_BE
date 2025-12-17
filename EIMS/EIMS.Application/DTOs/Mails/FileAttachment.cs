using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Mails
{
    public class FileAttachment
    {
        public string FileName { get; set; }
        public string? FileUrl { get; set; }
        public byte[] FileContent { get; set; }
    }
}
