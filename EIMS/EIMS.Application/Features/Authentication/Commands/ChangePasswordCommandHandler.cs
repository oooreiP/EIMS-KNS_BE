using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Authentication.Commands
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
    {
        private readonly IApplicationDBContext _context;
        private readonly IPasswordHasher _passwordHasher;
        public ChangePasswordCommandHandler(IApplicationDBContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
        public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(new object[] { request.AuthenticatedUserId }, cancellationToken);
            if (user == null)
            return Result.Fail(new Error("User not found").WithMetadata("ErrorCode", "Auth.ChangePassword.UserNotFound"));
            if( !_passwordHasher.Verify(request.CurrentPassword, user.PasswordHash))
                return Result.Fail(new Error("Current password is incorrect").WithMetadata("ErrorCode", "Auth.ChangePassword.IncorrectPassword"));
            //hash new password 
            user.PasswordHash = _passwordHasher.Hash(request.NewPassword);
            //update flag 
            user.IsPasswordChangeRequired = false;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }
    }
}