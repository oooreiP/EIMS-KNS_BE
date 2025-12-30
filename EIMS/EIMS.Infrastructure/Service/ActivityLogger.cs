using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Infrastructure.Service
{
    public class ActivityLogger : IActivityLogger
    {
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUserService _currentUser;
        private readonly IHttpContextAccessor _httpContext;

        public ActivityLogger(IUnitOfWork uow, ICurrentUserService currentUser, IHttpContextAccessor httpContext)
        {
            _uow = uow;
            _currentUser = currentUser;
            _httpContext = httpContext;
        }

        public async Task LogAsync(string action, string description, bool isSuccess = true)
        {
            var log = new SystemActivityLog
            {
                UserId = _currentUser.UserId ?? "System",
                ActionName = action,
                Description = description,
                Status = isSuccess ? "Success" : "Failed",
                Timestamp = DateTime.UtcNow,
                IpAddress = GetClientIpAddress(),
                // TraceId lấy từ HttpContext để link với DataLog nếu cần
                TraceId = _httpContext.HttpContext?.TraceIdentifier
            };

            await _uow.SystemActivityLogRepository.CreateAsync(log);
            await _uow.SaveChanges();
        }
        private string GetClientIpAddress()
        {
            var httpContext = _httpContext.HttpContext;
            if (httpContext == null) return "Unknown";

            // 1. Ưu tiên lấy IP thật nếu chạy sau Proxy/Load Balancer (Nginx, Cloudflare...)
            if (httpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                return httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? "Unknown";
            }

            // 2. Lấy IP trực tiếp
            var ip = httpContext.Connection.RemoteIpAddress;

            // 3. Xử lý trường hợp Localhost IPv6
            if (ip != null)
            {
                // Nếu là IPv6 localhost (::1) thì trả về IPv4 (127.0.0.1)
                if (ip.ToString() == "::1") return "127.0.0.1";

                // Chuyển đổi về IPv4 nếu có thể (để tránh các dạng ::ffff:192.168.1.1)
                if (ip.IsIPv4MappedToIPv6) return ip.MapToIPv4().ToString();

                return ip.ToString();
            }

            return "Unknown";
        }
    }
}
