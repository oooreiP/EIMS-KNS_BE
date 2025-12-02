using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoiceTemplate.Queries
{
    public record ViewTemplateQuery(int TemplateId) : IRequest<Result<string>>;
}