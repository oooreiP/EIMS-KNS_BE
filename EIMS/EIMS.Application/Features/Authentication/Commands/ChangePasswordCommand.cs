using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Authentication.Commands
{
    public class ChangePasswordCommand : IRequest<Result>, IAuthenticatedCommand
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public int AuthenticatedUserId { get; set; }
        public int? CustomerId { get; set; }
    }
}