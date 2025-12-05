using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Invoices.Commands.ChangeInvoiceStatus
{
    public class UpdateInvoiceStatusCommandHandler : IRequestHandler<UpdateInvoiceStatusCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateInvoiceStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(UpdateInvoiceStatusCommand request, CancellationToken cancellationToken)
        {
            var invoice = await _unitOfWork.InvoicesRepository.GetByIdAsync(request.InvoiceId);
            if(invoice == null)
                return Result.Fail(new Error($"Invoice with ID {request.InvoiceId} not found")
                    .WithMetadata("ErrorCode", "Invoice.Status.NotFound"));
            invoice.InvoiceStatusID = request.InvoiceStatusId;
            await _unitOfWork.InvoicesRepository.UpdateAsync(invoice);
            await _unitOfWork.SaveChanges();
            return Result.Ok(invoice.InvoiceID);
        }
    }
}
