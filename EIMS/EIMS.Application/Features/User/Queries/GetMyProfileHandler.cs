using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.User;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.User.Queries
{
    public class GetMyProfileHandler : IRequestHandler<GetMyProfileQuery, Result<UserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork; // Hoặc DbContext tùy setup của bạn
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        public GetMyProfileHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<UserResponse>> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(_currentUser.UserId);
            // var user = await _unitOfWork.UserRepository.GetAllQueryable()
            //                             .Include(u => u.Role)
            //                             .FirstOrDefaultAsync(u => u.UserID == userId, cancellationToken);
             var user = await _unitOfWork.UserRepository.GetAllQueryable()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserID == request.AuthenticatedUserId, cancellationToken);

            if (user == null)
            {
                return Result.Fail(new Error($"User with id {userId} not found")
                    .WithMetadata("ErrorCode", "User.NotFound"));
            }
            var userResponse = _mapper.Map<UserResponse>(user);
            if (user.Role != null)
            {
                userResponse.RoleName = user.Role.RoleName;
            }

            return Result.Ok(userResponse);
        }
    }
}

