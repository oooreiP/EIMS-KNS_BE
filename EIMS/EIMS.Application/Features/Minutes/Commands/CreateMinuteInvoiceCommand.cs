using EIMS.Application.DTOs.Minutes;
using EIMS.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Minutes.Commands
{
    public class CreateMinuteInvoiceCommand : IRequest<int>
    {
        public int InvoiceId { get; set; }
        public MinutesType MinuteType { get; set; }
        public string Description { get; set; }
        public string? Reason { get; set; }
        public IFormFile PdfFile { get; set; } 

        public CreateMinuteInvoiceCommand(CreateMinuteWithFileDto dto)
        {
            InvoiceId = dto.InvoiceId;
            MinuteType = dto.MinuteType;
            Description = dto.Description;
            PdfFile = dto.PdfFile;
        }
    }
}
