using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.User.Commands
{
    public class UpdateProfileHandler : IRequestHandler<UpdateProfileCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;
        public UpdateProfileHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(_currentUser.UserId);
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user == null)
            {
                return Result.Fail("User not found.");
            }
            user.FullName = request.FullName;
            user.PhoneNumber = request.PhoneNumber;
            if (!string.IsNullOrEmpty(request.EvidenceStoragePath))
            {
                user.EvidenceStoragePath = request.EvidenceStoragePath;
            }

            // 3. Lưu vào DB
            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveChanges();

            return Result.Ok();
        }
    }
}
