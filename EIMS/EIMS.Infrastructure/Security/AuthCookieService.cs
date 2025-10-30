
using EIMS.Application.Commons.Interfaces;
using Microsoft.AspNetCore.Http;

namespace EIMS.Infrastructure.Security
{
    public class AuthCookieService : IAuthCookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthCookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void ClearRefreshTokenCookie()
        {
            var response = _httpContextAccessor.HttpContext?.Response;
            if(response != null)
            {
                response.Cookies.Delete("refreshToken", new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });
            }
        }

        public void SetRefreshTokenCookie(string token, DateTime expires)
        {
            var response = _httpContextAccessor.HttpContext?.Response;
            if(response != null)
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = expires
                };
                response.Cookies.Append("refreshToken", token, cookieOptions);
            }
        }
    }
}