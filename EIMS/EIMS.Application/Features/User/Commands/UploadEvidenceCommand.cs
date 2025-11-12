using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EIMS.Application.Features.User.Commands
{
    public class UploadEvidenceCommand : IRequest<Result>, IAuthenticatedCommand
    {
        public int AuthenticatedUserId { get; set; }
        public IFormFile EvidenceFile { get; set; } // Or ICollection<IFormFile> for multiple
    }
}