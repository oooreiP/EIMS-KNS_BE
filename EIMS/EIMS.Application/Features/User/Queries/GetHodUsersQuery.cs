using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs.User;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.User.Queries
{
    public class GetHodUsersQuery : IRequest<Result<List<UserResponse>>>
    {
        // If IsActive is null, get all.
        // If IsActive is true, get active.
        // If IsActive is false, get inactive.
        public bool? IsActive { get; set; }
    }
}