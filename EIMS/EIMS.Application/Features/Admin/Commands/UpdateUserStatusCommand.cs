using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Enums;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Admin.Commands
{
    public class UpdateUserStatusCommand : IRequest<Result>
    {
        public int UserId { get; set; }
        public bool NewStatus { get; set; } // Active or Declined
        public string? AdminNotes { get; set; } // Optional, for decline reasons
    }
}