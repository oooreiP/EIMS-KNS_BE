using EIMS.Application.DTOs.Requests;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceRequests.Commands
{
    public class UploadEvidenceCommand : IRequest<Result<string>>
    {
        public int RequestID { get; set; }
        public IFormFile PdfFile { get; set; }

        public UploadEvidenceCommand(int requestID, IFormFile pdfFile)
        {
            RequestID = requestID;
            PdfFile = pdfFile;
        }
    }
}
