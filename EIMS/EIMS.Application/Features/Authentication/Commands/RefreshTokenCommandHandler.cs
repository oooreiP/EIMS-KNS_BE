using System.Security.Authentication;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Authentication;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.Authentication.Commands
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        private readonly IApplicationDBContext _context;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public RefreshTokenCommandHandler(IApplicationDBContext context, IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            //find token in db
            var savedRefreshToken = await _context.RefreshTokens
                .Include(rt => rt.User)
                    .ThenInclude(u => u.Role)
                .SingleOrDefaultAsync(rt => rt.Token == request.RefreshToken, cancellationToken);

            //validate token
            if (savedRefreshToken == null || !savedRefreshToken.IsActive)
            {
                throw new AuthenticationException("Invalid refresh token");
            }

            //generate new access token
            var user = savedRefreshToken.User;

            //token rotation
            var newRefreshToken = _jwtTokenGenerator.GenerateRefreshToken(user.UserID);
            _context.RefreshTokens.Remove(savedRefreshToken);
            _context.RefreshTokens.Add(newRefreshToken);
            await _context.SaveChangesAsync(cancellationToken);
            var newAccessToken = _jwtTokenGenerator.GenerateAccessToken(user);

            //generate new refresh token and save, disable the old one
            return new RefreshTokenResponse
            {
                UserID = user.UserID, // Corrected casing
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.RoleName,
                AccessToken = newAccessToken,
                NewRefreshToken = newRefreshToken.Token,      // Add this line
                NewRefreshTokenExpiry = newRefreshToken.Expires // Add this line
            };
        }
    }
}