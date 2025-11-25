using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs.User;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.User.Queries
{
    public class GetUserByIdQuery : IRequest<Result<UserResponse>>
    {
        public int UserId { get; }
        public GetUserByIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}