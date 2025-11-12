using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs;
using EIMS.Domain.Enums;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.User.Commands
{
    public class UploadEvidenceCommandHandler : IRequestHandler<UploadEvidenceCommand, Result>
    {
        private readonly IApplicationDBContext _context;
        private readonly IFileStorageService _fileStorageService;

        public UploadEvidenceCommandHandler(IApplicationDBContext context, IFileStorageService fileStorageService)
        {
            _context = context;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result> Handle(UploadEvidenceCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserID == request.AuthenticatedUserId, cancellationToken);

            if (user == null)
            {
                return Result.Fail(new Error("User not found.").WithMetadata("ErrorCode", "User.UploadEvidence.UserNotFound"));
            }

            // Check RoleName from the included Role object
            if (user.Role == null || user.Role.RoleName != "HOD")
            {
                return Result.Fail(new Error("User is not an HOD.").WithMetadata("ErrorCode", "User.UploadEvidence.NotHOD"));
            }

            if (user.Status != UserAccountStatus.PendingEvidence)
            {
                return Result.Fail(new Error("User is not eligible to upload evidence at this time.").WithMetadata("ErrorCode", "User.UploadEvidence.NotEligible"));
            }

            if (request.EvidenceFile == null || request.EvidenceFile.Length == 0)
            {
                return Result.Fail(new Error("No evidence file provided.").WithMetadata("ErrorCode", "User.UploadEvidence.NoFile"));
            }
            try
            {
                // Upload the new file
                var fileName = $"{user.UserID}-{Guid.NewGuid()}{Path.GetExtension(request.EvidenceFile.FileName)}";
                string evidenceFolder = "hod-evidence";

                Result<XMLUploadResultDto> uploadResult;
                await using (var stream = request.EvidenceFile.OpenReadStream())
                {
                    uploadResult = await _fileStorageService.UploadFileAsync(
                        stream, fileName, evidenceFolder);
                }

                if (uploadResult.IsFailed)
                {
                    return Result.Fail(uploadResult.Errors); // Pass Cloudinary errors back
                }

                // Update user with the URL from Cloudinary
                user.EvidenceStoragePath = uploadResult.Value.Url;
                user.Status = UserAccountStatus.PendingAdminReview;

                await _context.SaveChangesAsync(cancellationToken);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                // Log the error
                return Result.Fail($"Failed to upload evidence: {ex.Message}");
            }
        }
    }
}