using System.Security.Authentication;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Authentication;
using EIMS.Application.Features.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FluentResults;
using EIMS.Domain.Enums;
namespace EIMS.Application.Features.Authentication.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
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
        public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            //check user
            var user = await _context.Users
                              .Include(u => u.Role)
                              .SingleOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user == null || !user.IsActive)
            {
                return Result.Fail(new Error("Invalid email or password").WithMetadata("ErrorCode", "Auth.Login.InvalidCredentials"));
            }
            // if (!user.IsActive)
            // {
            //     bool isHod = user.Role.RoleName == "HOD"
            //                     && (user.Status == UserAccountStatus.PendingEvidence
            //                         || user.Status == UserAccountStatus.PendingAdminReview);
            //     if (!isHod)
            //     {
            //         return Result.Fail(new Error("Invalid email or password").WithMetadata("ErrorCode", "Auth.Login.InvalidCredentials"));
            //     }
            // }
                //verify password
                var passwordIsValid = _passwordHasher.Verify(request.Password, user.PasswordHash);
                if (!passwordIsValid)
                {
                    return Result.Fail(new Error("Invalid email or password.").WithMetadata("ErrorCode", "Auth.Login.InvalidCredentials"));
                }
                //generate Tokens and Save Refresh Token
                var accessToken = _jwtTokenGenerator.GenerateAccessToken(user);
                var refreshToken = _jwtTokenGenerator.GenerateRefreshToken(user.UserID); // Generate RefreshToken object

                //save the refresh token to the database
                _context.RefreshTokens.Add(refreshToken);
                await _context.SaveChangesAsync(cancellationToken); // Save changes here
                var LoginResponse = new LoginResponse
                {
                    UserID = user.UserID,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role.RoleName,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken.Token,
                    CustomerID = user.CustomerID,
                    IsActive = user.IsActive,
                    RefreshTokenExpiry = refreshToken.Expires,
                    IsPasswordChangeRequired = user.IsPasswordChangeRequired
                };
                return Result.Ok(LoginResponse);
            }
        }
    }