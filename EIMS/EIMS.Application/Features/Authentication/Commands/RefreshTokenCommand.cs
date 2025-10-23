using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs;
using MediatR;

namespace EIMS.Application.Features.Authentication.Commands
{
    public class RefreshTokenCommand : IRequest<AuthResponse>
    {
        public string RefreshToken { get; set; }
        public string IpAdress { get; set; }
    }
}