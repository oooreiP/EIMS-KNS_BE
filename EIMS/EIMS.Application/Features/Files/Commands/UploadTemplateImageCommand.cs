using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EIMS.Application.Features.Files.Commands
{
public record UploadTemplateImageCommand(IFormFile File, string FolderType) : IRequest<Result<string>>;
}