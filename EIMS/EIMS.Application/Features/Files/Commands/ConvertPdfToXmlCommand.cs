using EIMS.Application.DTOs;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Files.Commands
{
    public record ConvertPdfToXmlCommand(IFormFile PdfFile, string? Folder) : IRequest<Result<ConvertResultDto>>;
}
