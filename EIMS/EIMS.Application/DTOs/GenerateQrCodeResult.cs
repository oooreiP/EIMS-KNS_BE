using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs
{
    public class GenerateQrCodeResult
    {
        public string QrString { get; set; } = string.Empty;
        public string CrcCode { get; set; } = string.Empty;
        public string Base64Image { get; set; } = string.Empty;
    }
}
