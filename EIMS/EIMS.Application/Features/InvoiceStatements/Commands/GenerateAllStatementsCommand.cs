using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.InvoiceStatement;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoiceStatements.Commands
{
    public class GenerateAllStatementsCommand : IRequest<Result<GenerateBatchStatementResponse>>, IAuthenticatedCommand
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int AuthenticatedUserId { get; set; }
        public int? CustomerId { get; set; }
    }
}