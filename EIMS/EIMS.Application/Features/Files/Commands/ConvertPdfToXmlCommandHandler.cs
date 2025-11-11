using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Files.Commands
{
    
    public class ConvertPdfToXmlCommandHandler : IRequestHandler<ConvertPdfToXmlCommand, Result<ConvertResultDto>>
    {
        private readonly IDocumentParserService _parser;
        private readonly IFileStorageService _cloud;

        public ConvertPdfToXmlCommandHandler(IDocumentParserService parser, IFileStorageService cloud)
        {
            _parser = parser;
            _cloud = cloud;
        }

        public async Task<Result<ConvertResultDto>> Handle(ConvertPdfToXmlCommand request, CancellationToken cancellationToken)
        {
            if (request.PdfFile == null || request.PdfFile.Length == 0)
                return Result.Fail(new Error("PDF is empty").WithMetadata("ErrorCode", "Parse.EmptyFile"));

            string? xmlPath = null;
            try
            {
                using var pdfStream = request.PdfFile.OpenReadStream();
                xmlPath = await _parser.ConvertPdfToXmlAsync(pdfStream);

                await using var xmlFs = File.OpenRead(xmlPath);
                var upload = await _cloud.UploadFileAsync(xmlFs, Path.GetFileName(xmlPath), request.Folder); ;
                var resultDto = new ConvertResultDto(upload.Value.Url, upload.Value.PublicId);
                return Result.Ok(resultDto);
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error(ex.Message).WithMetadata("ErrorCode", "Parse.Failed"));
            }
            finally
            {
                if (xmlPath != null && File.Exists(xmlPath))
                    File.Delete(xmlPath);
            }
        }
    }
}
