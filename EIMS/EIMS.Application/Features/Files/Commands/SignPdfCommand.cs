using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Files.Commands
{
    public record SignPdfCommand(
    byte[] PdfBytes,
    string SearchText,
    string RootPath
    ) : IRequest<Result<byte[]>>;
}
