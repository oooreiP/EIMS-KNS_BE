using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Dashboard.Accountant;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Invoices.Queries
{
    public class GetAccountantDashboardQueryHandler : IRequestHandler<GetAccountantDashboardQuery, Result<AccountantDashboardDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAccountantDashboardQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AccountantDashboardDto>> Handle(GetAccountantDashboardQuery request, CancellationToken cancellationToken)
        {
            if (request.AuthenticatedUserId == 0) return Result.Fail("Invalid User ID");

            var data = await _unitOfWork.InvoicesRepository.GetAccountantDashboardAsync(request.AuthenticatedUserId, cancellationToken);
            return Result.Ok(data);
        }
    }
}