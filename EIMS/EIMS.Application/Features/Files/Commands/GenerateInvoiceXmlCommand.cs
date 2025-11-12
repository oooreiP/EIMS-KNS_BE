using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Files.Commands
{
    public record GenerateInvoiceXmlCommand(int InvoiceId) : IRequest<Result<string>>;
}
