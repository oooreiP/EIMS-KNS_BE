using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs.User;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.User.Commands
{
    public class RegisterHodCommand : IRequest<Result<UserResponse>>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        // Admin might provide a temporary password or system generates it
        // public string TemporaryPassword { get; set; }
    }
}