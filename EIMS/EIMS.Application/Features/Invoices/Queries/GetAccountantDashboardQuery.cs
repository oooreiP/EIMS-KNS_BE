using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Dashboard.Accountant;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Invoices.Queries
{
    public class GetAccountantDashboardQuery : IRequest<Result<AccountantDashboardDto>>, IAuthenticatedCommand
    {
        [JsonIgnore]
        public int AuthenticatedUserId { get; set; }
        [JsonIgnore]
        public int? CustomerId { get; set; }
    }
}