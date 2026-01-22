using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.InvoiceStatement;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoiceStatements.Queries
{
    public class GetInvoiceStatementsQuery : IRequest<Result<PaginatedList<StatementSummaryResponse>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? CustomerID { get; set; }
        public string? StatementCode { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? PeriodMonth { get; set; }
        public int? PeriodYear { get; set; }
        public int? StatusID { get; set; }
    }
}