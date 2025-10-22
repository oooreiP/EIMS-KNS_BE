using EIMS.Domain.Entities;

namespace EIMS.Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string CreateAccessToken(Users user);
        string CreateRefreshToken();
    }
}