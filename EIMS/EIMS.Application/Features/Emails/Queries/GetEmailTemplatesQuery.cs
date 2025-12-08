using EIMS.Application.DTOs;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Emails.Queries
{
    public class GetEmailTemplatesQuery : IRequest<Result<IEnumerable<EmailTemplateDto>>>
    {
        public string? SearchTerm { get; set; }
    }
}
