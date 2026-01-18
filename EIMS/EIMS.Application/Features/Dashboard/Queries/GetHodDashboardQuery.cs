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
    public class GetHodDashboardQuery : IRequest<Result<HodDashboardDto>>, IAuthenticatedCommand
    {
        public int? CustomerId { get; set; }
        public int AuthenticatedUserId { get; set; }
    }
}