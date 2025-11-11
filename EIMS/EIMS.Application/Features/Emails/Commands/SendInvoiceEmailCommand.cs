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
    string CustomerName,
    string InvoiceNumber,
    decimal TotalAmount,
    string Message,
    List<string> CloudinaryUrls
) : IRequest<Result>;
}
