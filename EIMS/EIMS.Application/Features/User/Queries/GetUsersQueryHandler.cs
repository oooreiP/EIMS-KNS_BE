using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.User;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.User.Queries
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<PaginatedList<UserResponse>>>
    {
        private readonly IApplicationDBContext _context;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IApplicationDBContext context, IMapper mapper, IUnitOfWork uow)
        {
            _context = context;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<Result<PaginatedList<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            // 1. Start with the base query
            var query = _context.Users
                .Include(u => u.Role)
                .AsNoTracking(); 

            // 2. Apply Search Filter (if provided)
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                string term = request.SearchTerm.ToLower();
                query = query.Where(u =>
                    u.FullName.ToLower().Contains(term) ||
                    u.Email.ToLower().Contains(term) ||
                    (u.PhoneNumber != null && u.PhoneNumber.Contains(term)));
            }

            // 3. Apply Role Filter (if provided)
            if (!string.IsNullOrWhiteSpace(request.RoleFilter))
            {
                query = query.Where(u => u.Role.RoleName == request.RoleFilter);
            }

            // 4. Order by most recently created
            query = query.OrderByDescending(u => u.CreatedAt);

            // 5. Project to DTO
            // This ensures we only fetch the necessary columns from DB
            var projectQuery = query.ProjectTo<UserResponse>(_mapper.ConfigurationProvider);

            // 6. Execute Pagination
            var paginatedList = await PaginatedList<UserResponse>.CreateAsync(
                projectQuery,
                request.PageNumber,
                request.PageSize
            );

            return Result.Ok(paginatedList);
        }
    }
}
