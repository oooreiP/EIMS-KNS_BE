using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs
{
    public record FileUploadResultDto(string Url, string PublicId, string FileName);
    public record XMLUploadResultDto(string Url, string PublicId);
}
