using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Minutes.Commands
{
    public class UpdateMinuteFileCommand : IRequest<Result<string>>
    {
        public int MinuteInvoiceId { get; set; }
        public IFormFile PdfFile { get; set; }

        public UpdateMinuteFileCommand(int minuteInvoiceId, IFormFile pdfFile)
        {
            MinuteInvoiceId = minuteInvoiceId;
            PdfFile = pdfFile;
        }
    }
}
