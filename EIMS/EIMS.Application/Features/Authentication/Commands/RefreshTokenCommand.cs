using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs.Authentication;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Authentication.Commands
{
    public class RefreshTokenCommand : IRequest<Result<RefreshTokenResponse>>
    {
        public string RefreshToken { get; set; }
    }
}