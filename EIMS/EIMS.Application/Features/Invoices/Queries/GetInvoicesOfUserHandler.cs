using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.Invoices;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Invoices.Queries
{
    public class GetInvoicesOfUserHandler : IRequestHandler<GetInvoicesOfUser, Result<PaginatedList<InvoiceDTO>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
            public GetInvoicesOfUserHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public Task<Result<PaginatedList<InvoiceDTO>>> Handle(GetInvoicesOfUser request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}