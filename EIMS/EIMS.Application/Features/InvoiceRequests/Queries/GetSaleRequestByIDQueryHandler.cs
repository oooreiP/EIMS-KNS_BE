using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs;
using EIMS.Application.DTOs.InvoiceItem;
using EIMS.Application.DTOs.Requests;
using EIMS.Application.Features.InvoiceRequests.Commands;
using EIMS.Application.Features.Invoices.Commands;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.InvoiceRequests.Queries
{
    public class GetSaleRequestByIDQueryHandler : IRequestHandler<GetSaleRequestByIDQuery, Result<FillInvoiceForm>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetSaleRequestByIDQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<FillInvoiceForm>> Handle(GetSaleRequestByIDQuery request, CancellationToken cancellationToken)
        {
            var invoiceRequest = await _unitOfWork.InvoiceRequestRepository.GetAllQueryable()
                .Include(i => i.Customer)
                .Include(i => i.InvoiceRequestItems)
                .ThenInclude(it => it.Product)
                .FirstOrDefaultAsync(i => i.RequestID == request.RequestId, cancellationToken);
            if (invoiceRequest == null)
            {
                return Result.Fail($"Invoice with Id {request.RequestId} not found");
            }
            var resultCommand = new FillInvoiceForm
            {
                RequestId = request.RequestId,
                InvoiceData = new EmptyRequest
                {
                    TemplateID = -1,
                    InvoiceStatusID = 1,
                    CompanyID = invoiceRequest.CompanyID ?? 1,
                    SalesID = invoiceRequest.SaleID,
                    MinRows = invoiceRequest.MinRows,
                    Notes = invoiceRequest.Notes,
                    PaymentMethod = invoiceRequest.PaymentMethod,
                    CustomerID = invoiceRequest.CustomerID,
                    CustomerName = invoiceRequest.InvoiceCustomerName,
                    TaxCode = invoiceRequest.InvoiceCustomerTaxCode ?? string.Empty,
                    Address = invoiceRequest.InvoiceCustomerAddress,
                    ContactEmail = invoiceRequest.Customer?.ContactEmail,
                    ContactPerson = invoiceRequest.Customer?.ContactPerson,
                    ContactPhone = invoiceRequest.Customer?.ContactPhone,
                    Amount = invoiceRequest.SubtotalAmount,
                    TaxAmount = invoiceRequest.VATAmount,
                    TotalAmount = invoiceRequest.TotalAmount,
                    Items = invoiceRequest.InvoiceRequestItems?.Select(item => new CreateInvoiceItemRequest
                    {
                        ProductId = item.ProductID,
                        ProductName = item.Product.Name,
                        Unit = item.Product.Unit,
                        Quantity = item.Quantity,
                        Amount = item.Amount,
                        VATAmount = item.VATAmount
                    }).ToList() ?? new List<CreateInvoiceItemRequest>()
                }
            };
            return Result.Ok(resultCommand);
        }
    }
}
