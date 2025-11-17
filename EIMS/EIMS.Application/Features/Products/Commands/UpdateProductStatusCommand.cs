using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Products.Commands
{
    public class UpdateProductStatusCommand : IRequest<Result>
    {
        public int ProductID { get; set; }
        public bool IsActive { get; set; }
    
    }
}