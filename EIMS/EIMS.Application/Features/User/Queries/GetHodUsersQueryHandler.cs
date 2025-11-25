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
    public class GetHodUsersQueryHandler : IRequestHandler<GetHodUsersQuery, Result<PaginatedList<UserResponse>>>
    {
        private readonly IApplicationDBContext _context;
        private readonly IMapper _mapper;
        public GetHodUsersQueryHandler(IApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<PaginatedList<UserResponse>>> Handle(GetHodUsersQuery request, CancellationToken cancellationToken)
        {
            // 1. Start query
            var query = _context.Users
                .AsNoTracking()
                .Include(u => u.Role)
                .Where(u => u.Role.RoleName == "HOD");

            // 2. Apply Filters
            if (request.IsActive.HasValue)
            {
                query = query.Where(u => u.IsActive == request.IsActive.Value);
            }

            // 3. Order by latest created (important for consistent pagination)
            query = query.OrderByDescending(u => u.CreatedAt);

            // 4. Project to DTO
            var projectQuery = query.ProjectTo<UserResponse>(_mapper.ConfigurationProvider);

            // 5. Paginate
            var paginatedList = await PaginatedList<UserResponse>.CreateAsync(
                projectQuery, 
                request.PageNumber, 
                request.PageSize
            );

            return Result.Ok(paginatedList);
        }
    }
}