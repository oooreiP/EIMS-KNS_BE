using EIMS.Application.DTOs.User;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.User.Queries
{
    public class GetMyProfileQuery : IRequest<Result<UserResponse>>
    {
    }
}
