using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Error = FluentResults.Error;

namespace EIMS.Infrastructure.Service
{
    public class FileStorageService : IFileStorageService
    {
        private readonly Cloudinary _cloudinaryService;
        private readonly ILogger<FileStorageService> _logger;
        private readonly string _folder;

        public FileStorageService(IConfiguration configuration, ILogger<FileStorageService> logger)
        {
            _logger = logger;
            _folder = configuration["Cloudinary:Folder"] ?? "uploads";

            // Initialize Cloudinary
            var cloudinaryUrl = configuration["Cloudinary:CloudinaryUrl"];
            if (!string.IsNullOrEmpty(cloudinaryUrl))
            {
                _cloudinaryService = new Cloudinary(cloudinaryUrl);
            }
            else
            {
                var account = new Account(
                    configuration["Cloudinary:CloudName"],
                    configuration["Cloudinary:ApiKey"],
                    configuration["Cloudinary:ApiSecret"]
                );
                _cloudinaryService = new Cloudinary(account);
            }
        }
        public async Task<Result<FileUploadResultDto>> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Result.Fail(new Error("File is empty or null").WithMetadata("ErrorCode", "Cloudinary.EmptyFile"));

            try
            {
                var fileName = file.FileName;
                var publicId = Path.GetFileNameWithoutExtension(file.FileName);               
                using var stream = file.OpenReadStream();
                UploadResult uploadResult;
                var fileExtension = Path.GetExtension(fileName).ToLower();
                if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || file.ContentType.StartsWith("image"))
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(fileName, stream),
                        PublicId = publicId,
                        Folder = _folder,
                        UseFilename = false,
                        UniqueFilename = true,
                        AccessMode = "public"
                    };
                    uploadResult = await _cloudinaryService.UploadAsync(uploadParams);
                }
                else
                {
                    var uploadParams = new RawUploadParams()
                    {
                        File = new FileDescription(fileName, stream),
                        PublicId = publicId,
                        Folder = _folder,
                        UseFilename = false,
                        UniqueFilename = true,
                        AccessMode = "public",
                        Type = "upload"
                    };
                    uploadResult = await _cloudinaryService.UploadAsync(uploadParams);
                }
                if (uploadResult.Error != null)
                {
                    _logger.LogError("Cloudinary upload error: {Error}", uploadResult.Error.Message);
                    return Result.Fail(new Error($"Cloudinary upload failed: {uploadResult.Error.Message}")
                        .WithMetadata("ErrorCode", "Cloudinary.UploadFailed"));
                }

                _logger.LogInformation("File uploaded successfully to Cloudinary: {PublicId}", uploadResult.PublicId);

                return Result.Ok(new FileUploadResultDto(
                    uploadResult.SecureUrl.ToString(),
                    uploadResult.PublicId,
                    fileName
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file to Cloudinary: {FileName}", file?.FileName);
                return Result.Fail(new Error(ex.Message).WithMetadata("ErrorCode", "Cloudinary.Exception"));
            }
        }
        public async Task<Result<XMLUploadResultDto>> UploadFileAsync(Stream fileStream, string fileName, string folder)
        {
            try
            {
                if (fileStream == null || fileStream.Length == 0)
                    return Result.Fail(new Error("File stream is empty").WithMetadata("ErrorCode", "Upload.EmptyStream"));

                var ext = Path.GetExtension(fileName).ToLower();
                var publicId = $"{folder}/{Path.GetFileNameWithoutExtension(fileName)}_{Guid.NewGuid()}";
                UploadResult uploadResult;

                if (ext is ".jpg" or ".jpeg" or ".png")
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(fileName, fileStream),
                        PublicId = publicId,
                        Folder = folder ?? _folder,
                        UniqueFilename = true,
                        AccessMode = "public"
                    };
                    uploadResult = await _cloudinaryService.UploadAsync(uploadParams);
                }
                else if (ext is ".mp4" or ".mov")
                {
                    var uploadParams = new VideoUploadParams
                    {
                        File = new FileDescription(fileName, fileStream),
                        PublicId = publicId,
                        Folder = folder ?? _folder,
                        UniqueFilename = true,
                        AccessMode = "public"
                    };
                    uploadResult = await _cloudinaryService.UploadAsync(uploadParams);
                }
                else
                {
                    var uploadParams = new RawUploadParams
                    {
                        File = new FileDescription(fileName, fileStream),
                        PublicId = publicId,
                        Folder = folder ?? _folder,
                        UniqueFilename = true,
                        AccessMode = "public"
                    };
                    uploadResult = await _cloudinaryService.UploadAsync(uploadParams);
                }

                if (uploadResult.Error != null)
                    return Result.Fail(new Error(uploadResult.Error.Message).WithMetadata("ErrorCode", "Upload.CloudinaryError"));

                return Result.Ok(new XMLUploadResultDto(uploadResult.SecureUrl.ToString(), uploadResult.PublicId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cloudinary upload failed: {FileName}", fileName);
                return Result.Fail(new Error(ex.Message).WithMetadata("ErrorCode", "Upload.Failed"));
            }
        }
        public async Task<bool> DeleteAsync(string publicId)
        {
            var del = await _cloudinaryService.DestroyAsync(new DeletionParams(publicId));
            return del.Result == "ok" || del.Result == "not_found";
        }
    }
}
