using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Emails.Commands
{
    public record SendInvoiceEmailCommand(
    string RecipientEmail,
    int invoiceId,
    string Message
) : IRequest<Result>;
}
