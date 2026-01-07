using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.Invoices;
using EIMS.Domain.Entities;
using MediatR;

namespace EIMS.Application.Features.Invoices.Queries
{
    public class GetInvoicesHodQueryHandler : IRequestHandler<GetInvoicesHodQuery, PaginatedList<InvoiceDTO>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetInvoicesHodQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<PaginatedList<InvoiceDTO>> Handle(GetInvoicesHodQuery request, CancellationToken cancellationToken)
        {
            //base query
            var query = _uow.InvoicesRepository.GetAllQueryable(
                includeProperties: "Customer,InvoiceStatus,InvoiceItems.Product,PaymentStatus,TaxApiLogs.TaxApiStatus,OriginalInvoice");
            query = query.Where( x => x.InvoiceStatusID != 1);

            //search term
            if(!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                string term = request.SearchTerm.ToLower();
                query = query.Where( x=>
                    x.InvoiceNumber.ToString().Contains(term) ||
                    x.Customer.CustomerName.ToLower().Contains(term) ||
                    x.InvoiceCustomerName.ToLower().Contains(term) ||
                    x.Customer.TaxCode.Contains(term)
                );
            }

            //date range(issued date)
            if( request.FromDate.HasValue )
                query = query.Where( x => x.IssuedDate >= request.FromDate.Value.ToUniversalTime() );
            if( request.ToDate.HasValue )
                query = query.Where( x => x.IssuedDate <= request.ToDate.Value.ToUniversalTime() );

            //amount range
            if( request.MinAmount.HasValue )
                query = query.Where( x => x.TotalAmount >= request.MinAmount.Value );
            if( request.MaxAmount.HasValue )
                query = query.Where( x => x.TotalAmount <= request.MaxAmount.Value );

            //status
            if( request.InvoiceStatusId.HasValue )
                query = query.Where( x => x.InvoiceStatusID == request.InvoiceStatusId.Value );
            if( request.PaymentStatusId.HasValue )
                query = query.Where( x => x.PaymentStatusID == request.PaymentStatusId.Value );

            //Sort
            query = _uow.InvoicesRepository.ApplySorting( query, request.SortColumn, request.SortDirection );

            //pagination and Mapping
            var paginatedInvoices = await PaginatedList<Invoice>.CreateAsync( query, request.PageIndex, request.PageSize );  
            var invoiceDtos = _mapper.Map<List<InvoiceDTO>>(paginatedInvoices.Items);
            return new PaginatedList<InvoiceDTO>(
                invoiceDtos,
                paginatedInvoices.TotalCount,
                paginatedInvoices.PageIndex,
                request.PageSize
            );
        }
    }
}