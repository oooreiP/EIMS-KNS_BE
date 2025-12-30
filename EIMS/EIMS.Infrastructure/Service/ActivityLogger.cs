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
                IpAddress = _httpContext.HttpContext?.Connection?.RemoteIpAddress?.ToString(),
                // TraceId lấy từ HttpContext để link với DataLog nếu cần
                TraceId = _httpContext.HttpContext?.TraceIdentifier
            };

            await _uow.SystemActivityLogRepository.CreateAsync(log);
            await _uow.SaveChanges();
        }
    }
}
