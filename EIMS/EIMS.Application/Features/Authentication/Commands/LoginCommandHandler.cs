using System.Security.Authentication;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Authentication;
using EIMS.Application.Features.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace EIMS.Application.Features.Authentication.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly IApplicationDBContext _context;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IPasswordHasher _passwordHasher;
        public LoginCommandHandler(IApplicationDBContext context, IJwtTokenGenerator jwtTokenGenerator, IPasswordHasher passwordHasher)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
            _passwordHasher = passwordHasher;
        }
        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            //check user
            var user = await _context.Users
                              .Include(u => u.Role)
                              .SingleOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user == null || !user.IsActive)
            {
                throw new AuthenticationException("Invalid email or password");
            }
            //verify password
            var passwordIsValid = _passwordHasher.Verify(request.Password, user.PasswordHash);
            if (!passwordIsValid)
            {
                throw new AuthenticationException("Invalid email or password");
            }
            //generate access token
            var accessToken = _jwtTokenGenerator.GenerateAccessToken(user);
            //generate, store and return AuthResponse
            return new AuthResponse
            {
                UserID = user.UserID,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.RoleName,
                AccessToken = accessToken
            };
    }
    }
}