using AutoMapper;
using AutoMapper.QueryableExtensions;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.Invoices;
using EIMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Queries
{
    public class GetAllInvoicesQueryHandler : IRequestHandler<GetAllInvoicesQuery, PaginatedList<InvoiceDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllInvoicesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedList<InvoiceDTO>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            // 1. Get Queryable from Repository including necessary navigation properties
            // Ensure these property names match your Entity navigation properties strictly
            var query = _unitOfWork.InvoicesRepository.GetAllQueryable(includeProperties: "Customer,InvoiceStatus,InvoiceItems.Product,PaymentStatus,TaxApiLogs.TaxApiStatus");

            // 2. Filter by Status
            if (request.StatusId.HasValue)
            {
                query = query.Where(x => x.InvoiceStatusID == request.StatusId.Value);
            }
            if (request.PaymentStatusId.HasValue)
            {
                query = query.Where(x => x.PaymentStatusID == request.PaymentStatusId.Value);
            }   
            // 3. Filter by Search Term
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                string term = request.SearchTerm.ToLower();
                // Note: .ToString() in LINQ to Entities can sometimes be restricted depending on the provider.
                // It is safer to search strictly on string columns.
                query = query.Where(x =>
                    x.InvoiceNumber.ToString().Contains(term) ||
                    x.Customer.CustomerName.ToLower().Contains(term) ||
                    x.Customer.TaxCode.Contains(term)
                );
            }

            // 4. Order by created date descending
            query = query.OrderByDescending(x => x.CreatedAt);

            // 5. Create PaginatedList of Entities
            var paginatedInvoices = await PaginatedList<Invoice>.CreateAsync(query, request.PageIndex, request.PageSize);

            // 6. Map to DTOs
            var invoiceDtos = query.ProjectTo<InvoiceDTO>(_mapper.ConfigurationProvider);

            var paginatedResult = await PaginatedList<InvoiceDTO>.CreateAsync(
                invoiceDtos,
                paginatedInvoices.TotalCount,
                paginatedInvoices.PageIndex,
                request.PageSize 
            );

            return paginatedResult;
        }
    }
}


