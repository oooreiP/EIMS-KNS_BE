using EIMS.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.Authentication.Commands
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        private readonly IApplicationDbContext _context;

        public LogoutCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var tokenEntity = await _context.UserRefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken);

            if (tokenEntity == null) return Unit.Value; // Already logged out or invalid

            _context.UserRefreshTokens.Remove(tokenEntity);
            await _context.SaveChangeAsync(cancellationToken);
            return Unit.Value;
        }
    }
}