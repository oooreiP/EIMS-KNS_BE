using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs;
using EIMS.Application.DTOs.InvoiceItems;
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
    public class GetInvoiceItemsByInvoiceIdHandler : IRequestHandler<GetInvoiceItemsByInvoiceIdQuery, Result<List<GetInvoiceItemsDTO>>>
    {
        private readonly IUnitOfWork _uow;

        public GetInvoiceItemsByInvoiceIdHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<List<GetInvoiceItemsDTO>>> Handle(GetInvoiceItemsByInvoiceIdQuery request, CancellationToken cancellationToken)
        {
            var items = await _uow.InvoiceItemRepository.GetAllQueryable()
                .AsNoTracking() 
                .Where(x => x.InvoiceID == request.InvoiceId)
                .Include(x => x.Product) // Chỉ cần Include Product để lấy tên
                .Select(x => new GetInvoiceItemsDTO
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
                .ToListAsync(cancellationToken);

            return Result.Ok(items);
        }
    }
}
