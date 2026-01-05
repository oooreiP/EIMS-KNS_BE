using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Dashboard.Sale;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Dashboard.Queries
{
    public class GetSalesDashboardQueryHandler : IRequestHandler<GetSalesDashboardQuery, Result<SalesDashboardDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSalesDashboardQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<SalesDashboardDto>> Handle(GetSalesDashboardQuery request, CancellationToken cancellationToken)
        {
            if (request.AuthenticatedUserId == 0)
                return Result.Fail("Invalid User ID");
            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.AuthenticatedUserId);
            if (user == null)
                return Result.Fail(new Error($"User with id {request.AuthenticatedUserId} not found").WithMetadata("Invalid User", "DashboardSale"));
            //role must be sale = 3
            if (user.RoleID != 3)
                return Result.Fail(new Error($"User with id {request.AuthenticatedUserId} is not a sale").WithMetadata("Invalid user", "DashboardSale.InvalidUser"));
            var stats = await _unitOfWork.InvoicesRepository
                .GetSalesDashboardStatsAsync(request.AuthenticatedUserId, cancellationToken);
            return Result.Ok(stats);
        }
    }
}