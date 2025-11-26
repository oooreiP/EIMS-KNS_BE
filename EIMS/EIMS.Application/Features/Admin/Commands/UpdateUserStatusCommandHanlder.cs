using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Mails;
using EIMS.Domain.Enums;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.Admin.Commands
{
    public class UpdateUserStatusCommandHanlder : IRequestHandler<UpdateUserStatusCommand, Result>
    {
        private readonly IApplicationDBContext _context;
        private readonly IEmailService _emailService;
        public UpdateUserStatusCommandHanlder(IApplicationDBContext context, IMapper mapper, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        public async Task<Result> Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                                .Include(u => u.Role)
                                .FirstOrDefaultAsync(u => u.UserID == request.UserId);
            if (user == null)
                return Result.Fail(new Error("User not found").WithMetadata("ErroCode", "Admin.UpdateHodStatusCommand.UserNotFound"));
            if (request.NewStatus != true && request.NewStatus != false)
                return Result.Fail(new Error("Invalid status").WithMetadata("ErroCode", "Admin.UpdateHodStatusCommand.InvalidStatus"));
            string emailSubject;
            string emailBody;
            if (request.NewStatus == true)
            {
                user.IsActive = true;
                emailSubject = "EIMS Account Activated!";
                emailBody = $"<p>Dear {user.FullName},</p><p>Good news! Your account for EIMS has been successfully activated by an administrator. " +
                            $"You can now fully access the system at [Your Login URL Here].</p><p>Thank you.</p>";
            }
            else
            {
                user.IsActive = false;
                emailSubject = "EIMS Account Status Update";
                emailBody = $"<p>Dear {user.FullName},</p><p>We regret to inform you that your account" +
                            $"for EIMS has been declined by an administrator.</p>" +
                            (string.IsNullOrEmpty(request.AdminNotes) ? "" : $"<p><strong>Reason:</strong> {request.AdminNotes}</p>") +
                            "<p>Please contact support if you have any questions.</p><p>Thank you.</p>";
            }
            await _context.SaveChangesAsync(cancellationToken);
            // Send email
            var mailRequest = new MailRequest
            {
                Email = user.Email,
                Subject = emailSubject,
                EmailBody = emailBody
            };
            await _emailService.SendMailAsync(mailRequest);
            return Result.Ok();
        }
    }
}