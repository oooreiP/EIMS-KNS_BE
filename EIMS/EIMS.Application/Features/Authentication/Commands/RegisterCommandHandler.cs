using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Mails;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.Authentication.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<int>>
    {
        private readonly IApplicationDBContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IEmailService _emailService;

        private const string DEFAULT_ROLE_NAME = "Accountant";

        public RegisterCommandHandler(IApplicationDBContext context, IPasswordHasher passwordHasher, IEmailService emailService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
        }

        public async Task<Result<int>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _context.Users
                .AnyAsync(u => u.Email == request.Email, cancellationToken);

            if (existingUser)
            {
                return Result.Fail(new Error("Email has been used").WithMetadata("ErrorCode", "Auth.Register.InvalidCredentials"));
            }

            var requestedRole = await _context.Roles
                .SingleOrDefaultAsync(r => r.RoleName == request.RoleName, cancellationToken);

            if (requestedRole == null)
            {
                throw new InvalidOperationException($"Default role '{request.RoleName}' not found.");
            }
            var tempPassword = Path.GetRandomFileName().Replace(".", "").Substring(0, 8);
            var passwordHash = _passwordHasher.Hash(tempPassword);
            var newUser = new Domain.Entities.User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = passwordHash,
                PhoneNumber = request.PhoneNumber,
                RoleID = requestedRole.RoleID,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                IsPasswordChangeRequired = true
            };
            if (requestedRole.RoleName == "Customer")
            {
                // 1. Validate required fields for a customer
                if (string.IsNullOrWhiteSpace(request.TaxCode) ||
                    string.IsNullOrWhiteSpace(request.CompanyName) ||
                    string.IsNullOrWhiteSpace(request.Address))
                {
                    return Result.Fail(new Error("For 'Customer' role, TaxCode, CompanyName, and Address are required.")
                        .WithMetadata("ErrorCode", "Auth.Register.CustomerInfoMissing"));
                }

                // 2. Check if this customer (by TaxCode) already exists
                var existingCustomer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.TaxCode == request.TaxCode, cancellationToken);

                if (existingCustomer != null)
                {
                    // 3a. If customer exists, link this new user to them
                    newUser.CustomerID = existingCustomer.CustomerID;
                }
                else
                {
                    // 3b. If customer does not exist, create a new one
                    var newCustomer = new Customer
                    {
                        CustomerName = request.CompanyName,
                        TaxCode = request.TaxCode,
                        Address = request.Address,
                        ContactEmail = request.Email, // Use the user's email as the contact
                        ContactPerson = request.FullName, // Use the user's name as the contact
                        ContactPhone = request.PhoneNumber
                    };
                    newUser.Customer = newCustomer;
                }
            }
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync(cancellationToken);
            // 5.Send welcome email to HOD
            string emailBody = $@"
            <h3>Welcome to EIMS</h3>
            <p>Your account has been created.</p>
            <p><strong>Email:</strong> {request.Email}</p>
            <p><strong>Temporary Password:</strong> {tempPassword}</p>
            <p>Please log in and change your password immediately.</p>
            <a href='http://your-frontend-url/login'>Click here to Login</a>";

            var mailRequest = new MailRequest
            {
                Email = request.Email,
                Subject = "Welcome to EIMS - Your HOD Account is Pending Activation",
                EmailBody = emailBody
            };
            await _emailService.SendMailAsync(mailRequest);
            return Result.Ok(newUser.UserID);
        }
    }
}