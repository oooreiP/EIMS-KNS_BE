using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs;
using MediatR;

namespace EIMS.Application.Features.Authentication.Commands
{
    public class LoginCommand : IRequest<AuthResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string IpAdress { get; set; }
    }
}