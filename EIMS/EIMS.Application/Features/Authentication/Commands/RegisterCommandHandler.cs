using EIMS.Application.Commons.Interfaces;
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
        private readonly IEmailSenderService _emailService;
        private readonly IUserRealtimeService _userRealtimeService;
        private readonly IDashboardRealtimeService _dashboardRealtimeService;

        private const string DEFAULT_ROLE_NAME = "Accountant";

        public RegisterCommandHandler(IApplicationDBContext context, IPasswordHasher passwordHasher, IEmailSenderService emailService, IUserRealtimeService userRealtimeService, IDashboardRealtimeService dashboardRealtimeService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
            _userRealtimeService = userRealtimeService;
            _dashboardRealtimeService = dashboardRealtimeService;
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
            Console.WriteLine(tempPassword);
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
                if (string.IsNullOrWhiteSpace(request.TaxCode))
                    return Result.Fail("TaxCode is required.");

                // 1. Check DB First
                var existingCustomer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.TaxCode == request.TaxCode, cancellationToken);

                if (existingCustomer != null)
                {
                    newUser.CustomerID = existingCustomer.CustomerID;
                }
                else
                {
                    // NOT FOUND: Now require the other fields to create it
                    if (string.IsNullOrWhiteSpace(request.CompanyName) || string.IsNullOrWhiteSpace(request.Address))
                    {
                        return Result.Fail("TaxCode not found. Please provide CompanyName and Address to create a new Customer.");
                    }

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
            await _userRealtimeService.NotifyUserChangedAsync(new EIMS.Application.Commons.Models.UserRealtimeEvent
            {
                UserId = newUser.UserID,
                ChangeType = "Created",
                RoleName = requestedRole.RoleName,
                IsActive = newUser.IsActive,
                Roles = new[] { "Admin" }
            }, cancellationToken);
            await _dashboardRealtimeService.NotifyDashboardChangedAsync(new EIMS.Application.Commons.Models.DashboardRealtimeEvent
            {
                Scope = "Users",
                ChangeType = "Created",
                EntityId = newUser.UserID,
                Roles = new[] { "Admin" }
            }, cancellationToken);
            // 5.Send welcome email to HOD
            string emailBody = $@"
            <h3>Welcome to EIMS</h3>
            <p>Your account has been created.</p>
            <p><strong>Email:</strong> {request.Email}</p>
            <p><strong>Temporary Password:</strong> {tempPassword}</p>
            <p>Please log in and change your password immediately.</p>
            <a href='https://www.knsinvoice.id.vn'>Click here to Login</a>";

            var mailRequest = new DTOs.Mails.MailRequest
            {
                Email = request.Email,
                Subject = "Welcome to EIMS - Your Account is Pending Activation",
                EmailBody = emailBody
            };
            _ = Task.Run(async () =>
                           {
                               try
                               {
                                   await _emailService.SendMailAsync(mailRequest);
                               }
                               catch (Exception ex)
                               {
                                   Console.WriteLine($"Email send error: {ex.Message}");
                               }
                           });
            return Result.Ok(newUser.UserID);
        }
    }
}