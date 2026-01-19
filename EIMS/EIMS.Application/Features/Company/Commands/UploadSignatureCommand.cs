using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Company.Commands
{
    public class UploadSignatureCommand : IRequest<Result<string>>
    {
        public int CompanyId { get; set; } 
        public IFormFile File { get; set; }
        public string Password { get; set; }
    }
}
