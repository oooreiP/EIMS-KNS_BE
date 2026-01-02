using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Dashboard;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.Dashboard.Queries
{
    public class GetCustomerDashboardQueryHandler : IRequestHandler<GetCustomerDashboardQuery, Result<CustomerDashboardDto>>
    {
        private readonly IUnitOfWork _uow;

        public GetCustomerDashboardQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<CustomerDashboardDto>> Handle(GetCustomerDashboardQuery request, CancellationToken cancellationToken)
        {
            // 1. Security Check (Populated by UserIdentityBehaviour)
            if (request.CustomerId == null)
            {
                return Result.Fail("Access Denied: You are not logged in as a Customer.");
            }

            // 2. Call the optimized Repository method
            var dashboardDto = await _uow.InvoicesRepository
                .GetCustomerDashboardStatsAsync(request.CustomerId.Value);

            return Result.Ok(dashboardDto);
        }
    }
}