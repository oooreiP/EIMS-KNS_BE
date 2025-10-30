using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Authentication.Commands
{
    public class RegisterCommand : IRequest<Result<int>>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string RoleName { get; set; } // Specify the desired role name
    }
}