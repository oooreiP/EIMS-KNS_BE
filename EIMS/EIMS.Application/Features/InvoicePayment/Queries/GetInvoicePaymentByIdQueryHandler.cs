using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.InvoicePayment;
using FluentResults;
using Humanizer;
using MediatR;

namespace EIMS.Application.Features.InvoicePayment.Queries
{
    public class GetInvoicePaymentByIdQueryHandler : IRequestHandler<GetInvoicePaymentByIdQuery, Result<InvoicePaymentDetailDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetInvoicePaymentByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<InvoicePaymentDetailDTO>> Handle(GetInvoicePaymentByIdQuery request, CancellationToken cancellationToken)
        {
            var invoicePayment = await _unitOfWork.InvoicePaymentRepository
                                          .GetByIdAsync(request.PaymentID, "Invoice,Invoice.Customer");
            if (invoicePayment == null)
            {
                return Result.Fail<InvoicePaymentDetailDTO>("Không tìm thấy giao dịch thanh toán.");
            }
            var invoicePaymentDTO = _mapper.Map<InvoicePaymentDetailDTO>(invoicePayment);

            return Result.Ok(invoicePaymentDTO);

        }
    }
}