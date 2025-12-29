using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IQrCodeService
    {
        string GenerateQrImageBase64(string content);
        string GenerateLookupCode();
    }
}
