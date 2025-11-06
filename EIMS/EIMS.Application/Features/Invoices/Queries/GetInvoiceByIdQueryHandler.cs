using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Invoices;
using EIMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Queries
{
    public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, InvoiceDTO?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetInvoiceByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<InvoiceDTO?> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            var invoice = await _unitOfWork.InvoicesRepository.GetByIdAsync(request.Id);
            var invoiceDTO = _mapper.Map<InvoiceDTO>(invoice);
            return invoiceDTO;
        }
    }
}
