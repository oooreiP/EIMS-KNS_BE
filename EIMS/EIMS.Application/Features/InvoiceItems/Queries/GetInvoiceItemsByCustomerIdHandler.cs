using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceItems.Queries
{
    public class GetInvoiceItemsByCustomerIdHandler : IRequestHandler<GetInvoiceItemsByCustomerIdQuery, Result<List<InvoiceItemDto>>>
    {
        private readonly IUnitOfWork _uow;

        public GetInvoiceItemsByCustomerIdHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<List<InvoiceItemDto>>> Handle(GetInvoiceItemsByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var items = await _uow.InvoiceItemRepository.GetAllQueryable()
                .AsNoTracking()
                .Include(x => x.Invoice) 
                .Where(x => x.Invoice.CustomerID == request.CustomerId)
                .Include(x => x.Product)
                .Select(x => new InvoiceItemDto
                {
                    InvoiceItemID = x.InvoiceItemID,
                    InvoiceID = x.InvoiceID,
                    InvoiceNumber = x.Invoice.InvoiceNumber.ToString(),
                    ProductId = x.ProductID,
                    ProductName = x.Product.Name,
                    Unit = x.Product.Unit,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    Amount = x.Amount,
                    VATAmount = x.VATAmount,
                    IsAdjustmentItem = x.IsAdjustmentItem
                })
                .OrderByDescending(x => x.InvoiceID) 
                .ToListAsync(cancellationToken);

            return Result.Ok(items);
        }
    }
}
