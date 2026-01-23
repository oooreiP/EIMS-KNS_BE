using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Helpers
{
    public class MemoryFormFile : IFormFile
    {
        private readonly Stream _stream;
        private readonly string _fileName;
        private readonly string _contentType;

        public MemoryFormFile(byte[] content, string fileName, string contentType = "application/pdf")
        {
            _stream = new MemoryStream(content);
            _fileName = fileName;
            _contentType = contentType;
        }
        public IHeaderDictionary Headers => null;
        public string ContentType => _contentType;
        public string ContentDisposition => $"form-data; name=\"file\"; filename=\"{_fileName}\"";
        public long Length => _stream.Length;
        public string Name => "file";
        public string FileName => _fileName;

        public void CopyTo(Stream target) => _stream.CopyTo(target);
        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default) => _stream.CopyToAsync(target, cancellationToken);
        public Stream OpenReadStream() => _stream;
    }
}
