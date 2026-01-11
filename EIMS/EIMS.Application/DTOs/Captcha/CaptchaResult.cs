using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Captcha
{
    public class CaptchaResult
    {
        public string CaptchaCode { get; set; } // Mã chữ (lưu vào Cache)
        public byte[] CaptchaImageBytes { get; set; } // Ảnh (gửi về Client)
        public DateTime Timestamp { get; set; }
    }
}
