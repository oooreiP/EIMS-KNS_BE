using EIMS.Application.Common.Interfaces;
using EIMS.Domain.Entities;
using EIMS.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.Authentication.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;
        public RegisterCommandHandler(IApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
        public async Task<int> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                throw new Exception("User with this email already exist");
            }
            var user = new Users
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                PasswordHash = _passwordHasher.Hash(request.Password),
                Role = request.Role,
                Status = Status.Active
            };
            _context.Users.Add(user);
            await _context.SaveChangeAsync(cancellationToken);
            return user.UserId;
        }
    }
}