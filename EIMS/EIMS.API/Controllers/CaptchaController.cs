using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        private readonly ICaptchaService _captchaService;
        private readonly IMemoryCache _memoryCache;

        public CaptchaController(ICaptchaService captchaService, IMemoryCache memoryCache)
        {
            _captchaService = captchaService;
            _memoryCache = memoryCache;
        }

        [HttpGet("generate")]
        public IActionResult GenerateCaptcha()
        {
            var result = _captchaService.GenerateCaptchaImage(120, 50);
            Console.WriteLine($"[TESTING] Captcha Code: {result.CaptchaCode}");
            string captchaId = Guid.NewGuid().ToString();
            _memoryCache.Set(captchaId, result.CaptchaCode, TimeSpan.FromMinutes(5));

            return Ok(new
            {
                CaptchaId = captchaId,
                ImageBase64 = Convert.ToBase64String(result.CaptchaImageBytes)
            });
        }
    }
}