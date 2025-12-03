using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Customers.Commands
{
    public class UpdateCustomerStatusCommand : IRequest<Result>
    {
        public int CustomerId { get; set; }
        public bool IsActive { get; set; }
    }
}