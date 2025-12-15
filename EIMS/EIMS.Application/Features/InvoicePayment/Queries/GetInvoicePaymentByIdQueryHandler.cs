using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.InvoicePayment;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoicePayment.Queries
{
    public class GetInvoicePaymentByIdQueryHandler : IRequestHandler<GetInvoicePaymentByIdQuery, Result<InvoicePaymentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetInvoicePaymentByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<InvoicePaymentDTO>> Handle(GetInvoicePaymentByIdQuery request, CancellationToken cancellationToken)
        {
            var invoicePayment = await _unitOfWork.InvoicePaymentRepository.GetByIdAsync(request.Id, "Invoice");
            var invoicePaymentDTO = _mapper.Map<InvoicePaymentDTO>(invoicePayment);
            return Result.Ok(invoicePaymentDTO);
        
        }
    }
}