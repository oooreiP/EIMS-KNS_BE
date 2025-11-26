using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Files.Commands
{
    public class UploadTemplateImageCommandHandler : IRequestHandler<UploadTemplateImageCommand, Result<string>>
    {
        private readonly IFileStorageService _fileStorageService;

        public UploadTemplateImageCommandHandler(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<string>> Handle(UploadTemplateImageCommand request, CancellationToken cancellationToken)
        {
            if (request.File == null || request.File.Length == 0)
                return Result.Fail("File is empty");

            // Organize uploads into specific folders in Cloudinary
            string folder = request.FolderType.ToLower() switch
            {
                "frame" => "invoice-frame",
                "logo" => "template-logos",
                _ => "uploads"
            };

            try
            {
                // Use the existing service to upload
                await using var stream = request.File.OpenReadStream();
                var uploadResult = await _fileStorageService.UploadFileAsync(stream, request.File.FileName, folder);

                if (uploadResult.IsFailed)
                    return Result.Fail(uploadResult.Errors);

                // Return only the URL needed for the database
                return Result.Ok(uploadResult.Value.Url);
            }
            catch (Exception ex)
            {
                return Result.Fail($"Upload failed: {ex.Message}");
            }
        }
    }
}