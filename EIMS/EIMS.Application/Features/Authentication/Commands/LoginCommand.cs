using EIMS.Application.DTOs.Authentication;
using MediatR;

namespace EIMS.Application.Features.Commands
{
    public class LoginCommand : IRequest<AuthResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }    
    }
}