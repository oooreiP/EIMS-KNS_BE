using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Admin.Commands
{
    public class ResetUserPasswordCommand : IRequest<Result<string>>, IAuthenticatedCommand
    {
        public string Email { get; set; }
        [JsonIgnore]
        public int AuthenticatedUserId { get; set; }
        [JsonIgnore]
        public int? CustomerId { get; set; }
    }
}