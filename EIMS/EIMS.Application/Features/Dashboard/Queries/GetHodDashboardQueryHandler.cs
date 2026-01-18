using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Dashboard.HOD;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Dashboard.Queries
{
    public class GetHodDashboardQueryHandler : IRequestHandler<GetHodDashboardQuery, Result<HodDashboardDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetHodDashboardQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<HodDashboardDto>> Handle(GetHodDashboardQuery request, CancellationToken cancellationToken)
        {
            if (request.AuthenticatedUserId == null)
            return Result.Fail("Invalid or missing user id");
            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.AuthenticatedUserId);
            if(user.RoleID != 4)
                return Result.Fail(new Error($"User with id {request.AuthenticatedUserId} is not HOD"));
            var stats = await _unitOfWork.InvoicesRepository.GetHodDashboardStatsAsync(cancellationToken);
            return Result.Ok(stats);
        }
    }
}