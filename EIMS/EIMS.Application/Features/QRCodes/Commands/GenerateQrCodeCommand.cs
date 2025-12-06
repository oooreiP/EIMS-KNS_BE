using EIMS.Application.DTOs;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.QRCodes.Commands
{
    public record GenerateQrCodeCommand(
    int InvoiceID,
    string PortalBaseUrl
    ) : IRequest<Result<string>>;
}
