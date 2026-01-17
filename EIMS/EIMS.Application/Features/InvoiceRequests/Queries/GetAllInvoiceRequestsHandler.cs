using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
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
    public class GetAllInvoiceRequestsHandler : IRequestHandler<GetAllInvoiceRequestsQuery, Result<PaginatedList<GetInvoiceRequestDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllInvoiceRequestsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PaginatedList<GetInvoiceRequestDto>>> Handle(GetAllInvoiceRequestsQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.InvoiceRequestRepository.GetAllQueryable(includeProperties: "Customer,Sales,InvoiceRequestItems.Product,InvoiceRequestItems");
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var term = request.SearchTerm.Trim().ToLower();
                query = query.Where(x =>
                    x.InvoiceCustomerName.ToLower().Contains(term) ||
                    x.InvoiceCustomerTaxCode.Contains(term) ||
                    x.RequestID.ToString().Contains(term)
                );
            }

            if (request.StatusId.HasValue && request.StatusId.Value > 0)
            {
                query = query.Where(x => x.RequestStatusID == request.StatusId.Value);
            }
            if (request.SaleId.HasValue && request.SaleId.Value > 0)
            {
                query = query.Where(x => x.SaleID == request.SaleId.Value);
            }
            query = query.OrderByDescending(x => x.CreatedAt);
            var dtoQuery = query.Select(x => new GetInvoiceRequestDto
            {
                RequestID = x.RequestID,
                CustomerName = x.InvoiceCustomerName,
                TaxCode = x.InvoiceCustomerTaxCode,
                TotalAmount = x.TotalAmount,
                StatusName = x.RequestStatus.StatusName,
                StatusId = x.RequestStatusID,
                SaleName = x.Sales != null ? x.Sales.FullName : "N/A",
                CreatedAt = x.CreatedAt
            });
            var paginatedResult = await PaginatedList<GetInvoiceRequestDto>
            .CreateAsync(dtoQuery, request.PageIndex, request.PageSize);

            return Result.Ok(paginatedResult);
        }
    }
}
