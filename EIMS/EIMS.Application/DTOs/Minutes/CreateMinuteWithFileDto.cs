using EIMS.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Minutes
{
    public class CreateMinuteWithFileDto
    {
        public int InvoiceId { get; set; }
        public MinutesType MinuteType { get; set; }
        public string? Description { get; set; }
        public IFormFile PdfFile { get; set; }
    }
}
