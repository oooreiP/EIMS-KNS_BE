using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Dashboard;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Dashboard.Queries
{
    public class GetAdminDashboardQueryHandler : IRequestHandler<GetAdminDashboardQuery, Result<AdminDashboardDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAdminDashboardQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AdminDashboardDto>> Handle(GetAdminDashboardQuery request, CancellationToken cancellationToken)
        {
            if (request.AuthenticatedUserId == null)
            return Result.Fail("Invalid or missing user id");
            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.AuthenticatedUserId);
            if(user.RoleID != 1)
                return Result.Fail(new Error($"User with id {request.AuthenticatedUserId} is not admin"));
            var stats = await _unitOfWork.InvoicesRepository.GetAdminDashboardStatsAsync(cancellationToken);
            return Result.Ok(stats);
        }
    }
}