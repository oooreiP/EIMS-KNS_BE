using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Enums;
using MediatR;

namespace EIMS.Application.Features.Authentication.Commands
{
    public class RegisterCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
    }
}