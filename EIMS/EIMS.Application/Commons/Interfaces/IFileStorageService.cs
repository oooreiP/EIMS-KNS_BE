using EIMS.Application.DTOs;
using FluentResults;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IFileStorageService
    {
        Task<Result<FileUploadResultDto>> UploadFileAsync(IFormFile file);
        Task<Result<XMLUploadResultDto>> UploadFileAsync(Stream fileStream, string fileName, string folder);
        Task<bool> DeleteAsync(string publicId);
    }
}
