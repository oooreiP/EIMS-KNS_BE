using System;

namespace EIMS.Application.DTOs.Authentication
{
    public class LoginResponse : AuthResponse
    {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }
}