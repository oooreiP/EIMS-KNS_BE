using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Features.Invoices.Queries;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Admin.Commands
{
    public class ResetUserPasswordCommandHandler : IRequestHandler<ResetUserPasswordCommand, Result<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IEmailSenderService _emailService;
        private readonly IPasswordHasher _passHash;
        private readonly IUserRealtimeService _userRealtimeService;

        public ResetUserPasswordCommandHandler(IUnitOfWork uow, IEmailSenderService emailService, IPasswordHasher passHash, IUserRealtimeService userRealtimeService)
        {
            _uow = uow;
            _emailService = emailService;
            _passHash = passHash;
            _userRealtimeService = userRealtimeService;
        }
        public async Task<Result<string>> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _uow.UserRepository.GetByEmailAsync(request.Email);
            if(user == null)
            return Result.Fail($"User {request.Email} is not existed");
            var admin = await _uow.UserRepository.GetByIdAsync(request.AuthenticatedUserId);
            if(admin == null)
                 return Result.Fail("User is not existed");
            if(admin.RoleID == 2)
            return Result.Fail("Performer is not an admin");
            var tempPassword = Path.GetRandomFileName().Replace(".", "").Substring(0, 8);
            var passwordHash = _passHash.Hash(tempPassword);
            user.PasswordHash = passwordHash;
            await _uow.UserRepository.UpdateAsync(user);
            await _uow.SaveChanges();
            await _userRealtimeService.NotifyUserChangedAsync(new EIMS.Application.Commons.Models.UserRealtimeEvent
            {
                UserId = user.UserID,
                ChangeType = "PasswordReset",
                RoleName = user.Role?.RoleName,
                Roles = new[] { "Admin" }
            }, cancellationToken);
            // 5.Send welcome email to HOD
            string emailBody = $@"
            <h3>Rest account EIMS</h3>
            <p>Your password has been reset.</p>
            <p><strong>Temporary Password:</strong> {tempPassword}</p>
            <p>Please log in and change your password immediately.</p>
            <a href='http://your-frontend-url/login'>Click here to Login</a>";

            var mailRequest = new DTOs.Mails.MailRequest
            {
                Email = request.Email,
                Subject = "Password reset EIMS - Your Account is Pending Activation",
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
            return Result.Ok(tempPassword);

        }
    }
}