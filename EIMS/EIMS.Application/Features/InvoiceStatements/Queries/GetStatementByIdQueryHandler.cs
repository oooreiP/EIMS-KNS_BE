using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.InvoiceStatement;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoiceStatements.Queries
{
    public class GetStatementByIdQueryHandler : IRequestHandler<GetStatementByIdQuery, Result<StatementDetailResponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IStatementService _statementService;
        public GetStatementByIdQueryHandler(IUnitOfWork uow, IStatementService statementService) 
        { 
            _uow = uow;
            _statementService = statementService;
        }
        public async Task<Result<StatementDetailResponse>> Handle(GetStatementByIdQuery request, CancellationToken cancellationToken)
        {
            var statement = await _uow.InvoiceStatementRepository
                                      .GetByIdWithInvoicesAsync(request.StatementID);
            if (statement == null)
                return Result.Fail(new Error("Statement not found").WithMetadata("StatementID", request.StatementID));
            var invoices = statement.StatementDetails.Select(d => d.Invoice).ToList();
            var response = new StatementDetailResponse
            {
                StatementID = statement.StatementID,
                StatementCode = statement.StatementCode,
                CustomerName = statement.Customer?.CustomerName ?? "Unknown",
                StatementDate = statement.StatementDate,
                Period = $"{statement.PeriodMonth:D2}/{statement.PeriodYear}",
                OpeningBalance = statement.OpeningBalance,
                NewCharges = statement.NewCharges,
                PaidAmount = statement.PaidAmount,
                TotalAmount = statement.TotalAmount,
                Status = statement.StatementStatus?.StatusName ?? "Draft",
                Invoices = statement.StatementDetails.Select(d => new StatementInvoiceDto
                {
                    InvoiceID = d.Invoice.InvoiceID,
                    InvoiceNumber = d.Invoice.InvoiceNumber,
                    SignDate = d.Invoice.SignDate,
                    TotalAmount = d.Invoice.TotalAmount,
                    OwedAmount = d.OutstandingAmount,
                    PaymentStatus = d.Invoice.PaymentStatusID.ToString()
                }).ToList(),
                ProductSummaries = _statementService.CalculateStatementProductSummary(invoices)
            };
            return Result.Ok(response);
        }
    }
}
