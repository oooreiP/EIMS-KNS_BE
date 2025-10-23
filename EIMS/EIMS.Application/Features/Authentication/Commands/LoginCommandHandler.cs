using AutoMapper;
using EIMS.Application.Common.Interfaces;
using EIMS.Application.DTOs;
using EIMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.Authentication.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;
        public LoginCommandHandler(IApplicationDbContext context, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator, IMapper mapper)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
            _mapper = mapper;
        }
        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
            {
                throw new Exception("Invalid email or password");
            }
            var accessToken = _jwtTokenGenerator.CreateAccessToken(user);
            var refreshToken = _jwtTokenGenerator.CreateRefreshToken();
            //luu rftoken trong db
            var refreshTokenEntity = new UserRefreshToken
            {
                UserId = user.UserId,
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = request.IpAddress
            };
            _context.UserRefreshTokens.Add(refreshTokenEntity);
            await _context.SaveChangesAsync(cancellationToken);
            var authResponse = _mapper.Map<AuthResponse>(user);
            authResponse.AccessToken = accessToken;
            authResponse.RefreshToken = refreshToken;
            return authResponse;
        }
    }
}