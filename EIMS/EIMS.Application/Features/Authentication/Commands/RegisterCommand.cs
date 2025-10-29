using MediatR;

namespace EIMS.Application.Features.Authentication.Commands
{
    public class RegisterCommand : IRequest<int> // Returns the new UserID
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
    }
}