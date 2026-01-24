using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Requests;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceRequests.Queries
{
    public class GetInvoiceRequestByIdHandler : IRequestHandler<GetInvoiceRequestByIdQuery, Result<GetInvoiceRequestDetailDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetInvoiceRequestByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetInvoiceRequestDetailDto>> Handle(GetInvoiceRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.InvoiceRequestRepository.GetAllQueryable()
                .Include(x => x.RequestStatus)
                .Include(x => x.Sales)
                .Include(x => x.InvoiceRequestItems) .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.RequestID == request.RequestId, cancellationToken);

            if (entity == null)
                return Result.Fail(new Error("Invoice Request not found"));

            var dto = new GetInvoiceRequestDetailDto
            {
                RequestID = entity.RequestID,
                CreatedInvoiceId = entity.CreatedInvoiceID,
                StatusName = entity.RequestStatus.StatusName,
                CustomerName = entity.InvoiceCustomerName,
                SaleName = entity.Sales?.FullName ?? "Unknown",
                TotalAmount = entity.TotalAmount,
                TotalAmountInWords = entity.TotalAmountInWords,
                CreatedAt = entity.CreatedAt,
                EvidenceFilePath = entity.EvidenceFilePath,
                InvoiceCustomerType = entity.InvoiceCustomerType != null
                    ? entity.InvoiceCustomerType.ToString()
                    : null,
                RejectReason = entity.Notes, 
                Items = entity.InvoiceRequestItems.Select(i => new InvoiceRequestItemDto
                {
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Amount = i.Amount,
                    VATAmount = i.VATAmount
                }).ToList()
            };

            return Result.Ok(dto);
        }
    }
}
