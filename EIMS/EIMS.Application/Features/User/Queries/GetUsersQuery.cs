using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.User;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.User.Queries
{
    public class GetUsersQuery : IRequest<Result<PaginatedList<UserResponse>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; } 
        public string? RoleFilter { get; set; } 
    }
}