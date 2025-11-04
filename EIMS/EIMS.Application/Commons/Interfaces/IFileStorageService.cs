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
        Task<string> SaveFileAsync(IFormFile file, string fileName);
        Task<byte[]?> GetFileAsync(string filePath);
        Task<bool> DeleteFileAsync(string filePath);
        Task<string> SaveJsonFileWithOriginalNameAsync(IFormFile file, string originalFileName);
        Task<string> SaveImageFileWithOriginalNameAsync(IFormFile file, string originalFileName);
    }
}
