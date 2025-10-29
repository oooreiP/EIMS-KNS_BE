using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Authentication.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, int>
    {
        private readonly IApplicationDBContext _context;
        private readonly IPasswordHasher _passwordHasher;

        private const string DEFAULT_ROLE_NAME = "Accountant"; // Ensure this role exists in your DB

        public RegisterCommandHandler(IApplicationDBContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<int> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _context.Users
                .AnyAsync(u => u.Email == request.Email, cancellationToken);

            if (existingUser)
            {
                throw new InvalidOperationException("Email address is already registered.");
            }

            var defaultRole = await _context.Roles
                .SingleOrDefaultAsync(r => r.RoleName == DEFAULT_ROLE_NAME, cancellationToken);

            if (defaultRole == null)
            {
                throw new InvalidOperationException($"Default role '{DEFAULT_ROLE_NAME}' not found.");
            }

            var passwordHash = _passwordHasher.Hash(request.Password);

            var newUser = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = passwordHash,
                PhoneNumber = request.PhoneNumber,
                RoleID = defaultRole.RoleID,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync(cancellationToken);

            return newUser.UserID;
        }
    }
}