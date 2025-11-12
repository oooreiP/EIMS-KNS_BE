using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.User;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.User.Queries
{
    public class GetHodUsersQueryHandler : IRequestHandler<GetHodUsersQuery, Result<List<UserResponse>>>
    {
        private readonly IApplicationDBContext _context;
        private readonly IMapper _mapper;
        public GetHodUsersQueryHandler(IApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<List<UserResponse>>> Handle(GetHodUsersQuery request, CancellationToken cancellationToken)
        {
            //query user with role HOD
            var query = _context.Users
                          .Include(u => u.Role)
                          .Where(u => u.Role.RoleName == "HOD");
            //read query if isactive # null so use it
            if (request.IsActive.HasValue)
            {
                query = query.Where(u => u.IsActive == request.IsActive.Value);
            }
            //map result into DTOs
            var users = await query
                            .Select(u => _mapper.Map<UserResponse>(u))
                            .ToListAsync(cancellationToken);
            return Result.Ok(users);
        }
    }
}