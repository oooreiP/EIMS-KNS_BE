using EIMS.Application.DTOs.Authentication;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Commands
{
    public class LoginCommand : IRequest<Result<LoginResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }    
    }
}