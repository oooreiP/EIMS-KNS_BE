using AutoMapper;
using EIMS.Application.Commons.Interfaces;
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
    public class GetAllInvoicesQueryHandler : IRequestHandler<GetAllInvoicesQuery, IEnumerable<InvoiceDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllInvoicesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InvoiceDTO>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices =  await _unitOfWork.InvoicesRepository.GetAllAsync(includeProperties: "InvoiceItems");
            var invoiceDTOs = _mapper.Map<IEnumerable<InvoiceDTO>>(invoices);
            return invoiceDTOs;
        }
    }
}
