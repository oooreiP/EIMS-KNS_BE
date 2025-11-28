using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Files.Commands
{
        public record GenerateInvoicePdfCommand(int InvoiceId) : IRequest<Result<byte[]>>;
}