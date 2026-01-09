using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected string GetClientIpAddress()
        {
            // 1. Check X-Forwarded-For (Standard for Proxies like Nginx)
            if (Request.Headers.TryGetValue("X-Forwarded-For", out var forwardedFor))
            {
                // The header can contain multiple IPs (Client, Proxy1, Proxy2). 
                // We take the first one (Client IP).
                return forwardedFor.ToString().Split(',')[0].Trim();
            }

            // 2. Check X-Real-IP (Nginx alternative)
            if (Request.Headers.TryGetValue("X-Real-IP", out var realIp))
            {
                return realIp.ToString();
            }

            // 3. Fallback to direct connection IP
            return HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        }
    }
}