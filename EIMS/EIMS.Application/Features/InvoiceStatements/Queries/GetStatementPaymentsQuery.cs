using EIMS.Application.DTOs.InvoiceStatement;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoiceStatements.Queries
{
    public class GetStatementPaymentsQuery : IRequest<Result<StatementPaymentsResponseDto>>
    {
        public int StatementId { get; }

        public GetStatementPaymentsQuery(int statementId)
        {
            StatementId = statementId;
        }
    }
}
