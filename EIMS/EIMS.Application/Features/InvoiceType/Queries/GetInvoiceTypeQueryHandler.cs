using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.InvoiceType;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoiceType.Queries
{
    public class GetInvoiceTypeQueryHandler : IRequestHandler<GetInvoiceTypeQuery, Result<List<InvoiceTypeResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetInvoiceTypeQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<InvoiceTypeResponse>>> Handle(GetInvoiceTypeQuery request, CancellationToken cancellationToken)
        {
            var invoiceTypes = await _unitOfWork.InvoiceTypeRepository.GetAllAsync();
            var response = invoiceTypes.Select(p => new InvoiceTypeResponse
            {
                InvoiceTypeID = p.InvoiceTypeID,
                Symbol = p.Symbol,
                TypeName = p.TypeName
            }).ToList();
            return Result.Ok(response);

        }
    }
}