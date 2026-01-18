using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Emails.Queries
{
    public class GetBaseContentByCodeQuery : IRequest<Result<string>>
    {
        public string TemplateCode { get; set; }

        public GetBaseContentByCodeQuery(string code)
        {
            TemplateCode = code;
        }
    }
}
