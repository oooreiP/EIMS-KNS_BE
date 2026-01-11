using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs.Captcha;

namespace EIMS.Application.Commons.Interfaces
{
    public interface ICaptchaService
    {
        CaptchaResult GenerateCaptchaImage(int width, int height);
    }
}