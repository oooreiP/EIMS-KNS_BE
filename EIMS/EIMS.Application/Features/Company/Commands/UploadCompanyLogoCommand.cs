using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EIMS.Application.Features.Company.Commands
{
   public class UploadCompanyLogoCommand : IRequest<Result<string>>
    {
        public int CompanyId { get; set; }
        public IFormFile File { get; set; } = null!;
    }
}