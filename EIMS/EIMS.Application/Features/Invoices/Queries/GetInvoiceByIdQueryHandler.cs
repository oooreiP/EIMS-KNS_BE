using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Invoices;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Queries
{
    public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, Result<InvoiceDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetInvoiceByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<InvoiceDTO>> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
          // 1. Fetch from DB with ALL Includes
            var invoice = await _unitOfWork.InvoicesRepository.GetByIdAsync(
                request.Id, 
                // Ensure OriginalInvoice and TaxApiStatus are included
                "Customer,InvoiceStatus,InvoiceItems.Product,PaymentStatus,TaxApiLogs.TaxApiStatus,OriginalInvoice"
            );

            // 2. CHECK IF NULL (Fixes the 500 Error)
            if (invoice == null)
            {
                return Result.Fail($"Invoice with ID {request.Id} not found.");
            }

            // 3. Map and Return
            var invoiceDTO = _mapper.Map<InvoiceDTO>(invoice);
            return Result.Ok(invoiceDTO);
        }
    }
}
