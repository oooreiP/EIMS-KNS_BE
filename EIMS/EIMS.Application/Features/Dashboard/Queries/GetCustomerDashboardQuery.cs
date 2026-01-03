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
    public class GetCustomerDashboardQuery : IRequest<Result<CustomerDashboardDto>>, IAuthenticatedCommand
    {
        public int? CustomerId { get; set; }
        public int AuthenticatedUserId { get; set; }
    }
}