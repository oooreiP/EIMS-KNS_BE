using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Mails;
using EIMS.Application.DTOs.User;
using EIMS.Domain.Enums;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.User.Commands
{
    public class RegisterHodCommandHandler : IRequestHandler<RegisterHodCommand, Result<UserResponse>>
    {
        private readonly IApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IEmailService _emailService;
        private readonly IUserRealtimeService _userRealtimeService;
        private readonly IDashboardRealtimeService _dashboardRealtimeService;

        public RegisterHodCommandHandler(IApplicationDBContext context, IMapper mapper, IPasswordHasher passwordHasher, IEmailService emailService, IUserRealtimeService userRealtimeService, IDashboardRealtimeService dashboardRealtimeService)
        {
            _context = context;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
            _userRealtimeService = userRealtimeService;
            _dashboardRealtimeService = dashboardRealtimeService;
        }
        public async Task<Result<UserResponse>> Handle(RegisterHodCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
                return Result.Fail(new Error($"User with email {request.Email} already exists").WithMetadata("ErrorCode", "User.Register.Failed"));
            var hodRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == "HOD", cancellationToken);
            if (hodRole == null)
                return Result.Fail(new Error("HOD role not found").WithMetadata("ErrorCode", "User.Register.Failed"));
            var tempPassword = Path.GetRandomFileName().Replace(".", "").Substring(0, 8);
            var passwordHash = _passwordHasher.Hash(tempPassword);
            var user = new Domain.Entities.User
            {
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = passwordHash,
                RoleID = hodRole.RoleID,
                IsActive = false,
                Status = UserAccountStatus.PendingEvidence,
                CreatedAt = DateTime.UtcNow,
                IsPasswordChangeRequired = true
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            await _userRealtimeService.NotifyUserChangedAsync(new EIMS.Application.Commons.Models.UserRealtimeEvent
            {
                UserId = user.UserID,
                ChangeType = "Created",
                RoleName = hodRole.RoleName,
                IsActive = user.IsActive,
                Roles = new[] { "Admin" }
            }, cancellationToken);
            await _dashboardRealtimeService.NotifyDashboardChangedAsync(new EIMS.Application.Commons.Models.DashboardRealtimeEvent
            {
                Scope = "Users",
                ChangeType = "Created",
                EntityId = user.UserID,
                Roles = new[] { "Admin" }
            }, cancellationToken);

            // 5. Send welcome email to HOD
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
            var userResponse = _mapper.Map<UserResponse>(user);
            userResponse.RoleName = hodRole.RoleName;
            return Result.Ok(userResponse);
        }
    }
}