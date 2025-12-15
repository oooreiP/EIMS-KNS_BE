using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.InvoicePayment;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoicePayment.Queries
{
    public class GetInvoicePaymentQueriesHandler : IRequestHandler<GetInvoicePayments, Result<PaginatedList<InvoicePaymentDTO>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetInvoicePaymentQueriesHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Result<PaginatedList<InvoicePaymentDTO>>> Handle(GetInvoicePayments request, CancellationToken cancellationToken)
        {
            //get Queryable
            var query = _uow.InvoicePaymentRepository.GetAllQueryable(includeProperties: "Invoice,Invoice.Customer");
            //Filter
            if (request.InvoiceId.HasValue)
            {
                query = query.Where(x => x.InvoiceID == request.InvoiceId.Value);
            }
            if (request.CustomerId.HasValue)
            {
                query = query.Where(x => x.Invoice.CustomerID == request.CustomerId.Value);
            }
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                string term = request.SearchTerm.ToLower();
                query = query.Where(x =>
                    x.PaymentMethod.ToLower().Contains(term) ||
                    x.TransactionCode.ToLower().Contains(term)
                );
            }
            //Order
            query = query.OrderByDescending(x => x.PaymentDate);
            //Create PaginatedList
            var paginatedList = await PaginatedList<Domain.Entities.InvoicePayment>.CreateAsync(query, request.PageIndex, request.PageSize);
            //Map
            var invoicePaymentDTOs = _mapper.Map<List<InvoicePaymentDTO>>(paginatedList.Items);
            //Return
            var result = new PaginatedList<InvoicePaymentDTO>(
                invoicePaymentDTOs,
                paginatedList.TotalCount,
                paginatedList.PageIndex,
                request.PageSize
            );
            return Result.Ok(result);
        }
    }
}