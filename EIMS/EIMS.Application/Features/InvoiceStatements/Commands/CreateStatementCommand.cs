using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoiceStatements.Commands
{
    public class CreateStatementCommand : IRequest<Result<int>>, IAuthenticatedCommand
    {
        public int CustomerID { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int AuthenticatedUserId { get; set; }
    }
}