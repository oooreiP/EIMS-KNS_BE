using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Authentication.Commands
{
    public class LogoutCommand : IRequest<Result<Unit>>
    {
        public string RefreshToken { get; set; }
    }
}