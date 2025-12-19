using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.InvoicePayment;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoicePayment.Queries
{
    public class GetPaymentsByInvoiceIdHandler : IRequestHandler<GetPaymentsByInvoiceIdQuery, Result<List<InvoicePaymentDTO>>>
    {
        private readonly IUnitOfWork _uow;
        // private readonly IMapper _mapper; 

        public GetPaymentsByInvoiceIdHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<List<InvoicePaymentDTO>>> Handle(GetPaymentsByInvoiceIdQuery request, CancellationToken cancellationToken)
        {
            var invoiceExists = await _uow.InvoicesRepository.GetAllQueryable()
                .AnyAsync(x => x.InvoiceID == request.InvoiceId, cancellationToken);

            if (!invoiceExists)
            {
                return Result.Fail($"Không tìm thấy hóa đơn với ID: {request.InvoiceId}");
            }
            var paymentsQuery = _uow.InvoicePaymentRepository.GetAllQueryable();

            var paymentDtos = await paymentsQuery
            .Where(p => p.InvoiceID == request.InvoiceId)
                .OrderByDescending(p => p.PaymentDate) 
                .Select(p => new InvoicePaymentDTO
                {
                    InvoiceID = p.InvoiceID,
                    PaymentID = p.PaymentID,
                    AmountPaid = p.AmountPaid,
                    PaymentDate = p.PaymentDate,
                    PaymentMethod = p.PaymentMethod,
                    TransactionCode = p.TransactionCode,
                    Note = p.Note,
                    CreatedBy = p.CreatedBy
                })
                .ToListAsync(cancellationToken);

            return Result.Ok(paymentDtos);
        }
    }
}
