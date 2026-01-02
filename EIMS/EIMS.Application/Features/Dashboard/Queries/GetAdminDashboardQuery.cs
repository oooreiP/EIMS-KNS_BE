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
   public class GetAdminDashboardQuery : IRequest<Result<AdminDashboardDto>>, IAuthenticatedCommand
    {
        public int AuthenticatedUserId { get; set; }
        public int? CustomerId { get; set; } 
    }
}