using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.Authentication.Commands
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result<Unit>>
    {
        private readonly IApplicationDBContext _context;

        public LogoutCommandHandler(IApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            // Find the refresh token in the database
            var refreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken, cancellationToken);

            // If found, remove it
            if (refreshToken != null)
            {
                _context.RefreshTokens.Remove(refreshToken);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return Result.Ok(Unit.Value);
        }
    }
}