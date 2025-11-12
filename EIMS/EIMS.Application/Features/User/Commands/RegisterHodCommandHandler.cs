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
        public RegisterHodCommandHandler(IApplicationDBContext context, IMapper mapper, IPasswordHasher passwordHasher, IEmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
        }
        public async Task<Result<UserResponse>> Handle(RegisterHodCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email, cancellationToken))
                return Result.Fail(new Error($"User with email {request.Email} already exists").WithMetadata("ErrorCode", "User.Register.Failed"));
            var hodRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == "HOD", cancellationToken);
            if (hodRole == null)
                return Result.Fail(new Error("HOD role not found").WithMetadata("ErrorCode", "User.Register.Failed"));
            string temporaryPassword = "P3ssword!";
            var hashedPassword = _passwordHasher.Hash(temporaryPassword);
            var user = new Domain.Entities.User
            {
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = hashedPassword,
                RoleID = hodRole.RoleID,
                IsActive = false,
                Status = UserAccountStatus.PendingEvidence,
                CreatedAt = DateTime.UtcNow
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            // 5. Send welcome email to HOD
            string emailBody = $@"
                <div style='font-family:Arial,sans-serif; font-size:14px;'>
                    <p>Dear {request.FullName},</p>
                    <p>Your EIMS HOD account has been created by an administrator.</p>
                    <p>Your temporary password is: <strong>{temporaryPassword}</strong></p>
                    <p>Please log in to upload your evidence for activation. You can access the system at [Your Login URL Here].</p>
                    <p>Thank you.</p>
                </div>";

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