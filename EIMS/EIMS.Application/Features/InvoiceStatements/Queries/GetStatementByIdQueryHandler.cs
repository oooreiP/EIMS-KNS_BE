using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.InvoiceStatement;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoiceStatements.Queries
{
    public class GetStatementByIdQueryHandler : IRequestHandler<GetStatementByIdQuery, Result<StatementDetailResponse>>
    {
        private readonly IUnitOfWork _uow;
        public GetStatementByIdQueryHandler(IUnitOfWork uow) => _uow = uow;
        public async Task<Result<StatementDetailResponse>> Handle(GetStatementByIdQuery request, CancellationToken cancellationToken)
        {
            var statement = await _uow.InvoiceStatementRepository
                                      .GetByIdWithInvoicesAsync(request.StatementID);
            if (statement == null)
                return Result.Fail(new Error("Statement not found").WithMetadata("StatementID", request.StatementID));
            var response = new StatementDetailResponse
            {
                StatementID = statement.StatementID,
                StatementCode = statement.StatementCode,
                CustomerName = statement.Customer?.CustomerName ?? "Unknown",
                StatementDate = statement.StatementDate ?? DateTime.MinValue,
                TotalAmount = statement.TotalAmount,
                Status = statement.StatementStatus?.StatusName ?? "Draft",
                Invoices = statement.StatementDetails.Select(d => new StatementInvoiceDto
                {
                    InvoiceID = d.Invoice.InvoiceID,
                    InvoiceNumber = d.Invoice.InvoiceNumber,
                    SignDate = d.Invoice.SignDate,
                    TotalAmount = d.Invoice.TotalAmount,
                    PaymentStatus = d.Invoice.PaymentStatusID.ToString()
                }).ToList()
            };
            return Result.Ok(response);
        }
    }
}
