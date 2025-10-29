using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateAccessToken(User user);
        RefreshToken GenerateRefreshToken(int userId);
    }
}