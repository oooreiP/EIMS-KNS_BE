using EIMS.Application.Common.Interfaces;
using EIMS.Application.DTOs;
using EIMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.Authentication.Commands
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public RefreshTokenCommandHandler(IApplicationDbContext context, IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            //find token in db 
            var tokenEntity = await _context.UserRefreshTokens
                                    .Include(rt => rt.User)
                                    .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken);
            if (tokenEntity == null || !tokenEntity.IsActive)
            {
                throw new Exception("Invalid or expired refresh token.");
            }
            var user = tokenEntity.User;
            //rotation
            //1.Create new tokens
            //2.Delete old token in db
            //3.save new token in db
            var newAccessToken = _jwtTokenGenerator.CreateAccessToken(user);
            var newRefreshToken = _jwtTokenGenerator.CreateRefreshToken();
            var newRefreshTokenEntity = new UserRefreshToken
            {
                UserId = user.UserId,
                Token = newRefreshToken,
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = request.IpAdress
            };
            _context.UserRefreshTokens.Add(newRefreshTokenEntity);
            await _context.SaveChangeAsync(cancellationToken);
            return new AuthResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                Email = user.Email,
                Name = user.Name,
                Role = user.Role.ToString()
            };
        }
    }
}