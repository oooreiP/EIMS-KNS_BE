using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Emails.Queries
{
    public class GetEmailTemplateVariablesQuery : IRequest<Result<List<string>>>
    {
        public string Category { get; set; }
    }
}
