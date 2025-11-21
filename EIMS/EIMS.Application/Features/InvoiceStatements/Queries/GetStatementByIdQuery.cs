using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs.InvoiceStatement;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoiceStatements.Queries
{
    public class GetStatementByIdQuery : IRequest<Result<StatementDetailResponse>>
    {
        public int StatementID { get; set; }
        public GetStatementByIdQuery(int id) => StatementID = id;
    }
}