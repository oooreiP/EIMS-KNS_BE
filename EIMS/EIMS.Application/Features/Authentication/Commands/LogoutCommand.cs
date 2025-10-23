using MediatR;

namespace EIMS.Application.Features.Authentication.Commands
{
    public class LogoutCommand : IRequest
    {
        // We use the token itself to find the session to log out
        public string RefreshToken { get; set; }
    }
}