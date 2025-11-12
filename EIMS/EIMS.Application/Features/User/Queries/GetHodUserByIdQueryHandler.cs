using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.User;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.User.Queries
{
    public class GetHodUserByIdQueryHandler : IRequestHandler<GetHodUserByIdQuery, Result<UserResponse>>
    {
        private readonly IApplicationDBContext _context;
        private readonly IMapper _mapper;
        public GetHodUserByIdQueryHandler(IApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<UserResponse>> Handle(GetHodUserByIdQuery request, CancellationToken cancellationToken)
        {
            //query to get HOD user by id 
            var user = await _context.Users
                                .Include(u => u.Role)
                                .FirstOrDefaultAsync(u => u.UserID == request.UserId && u.Role.RoleName == "HOD", cancellationToken);
            if (user == null)
            {
                return Result.Fail(new Error($"HoD with id {request.UserId} not found").WithMetadata("ErrorCode", "User.Hod.NotFound"));
            }
            var UserResponse = _mapper.Map<UserResponse>(user);
            return Result.Ok(UserResponse);
        }
    }
}