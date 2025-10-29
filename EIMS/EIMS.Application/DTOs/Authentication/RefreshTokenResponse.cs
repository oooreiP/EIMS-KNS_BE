using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Authentication
{
    public class RefreshTokenResponse : AuthResponse
    {
        public string NewRefreshToken { get; set; }
        public DateTime NewRefreshTokenExpiry { get; set; }
    }
}