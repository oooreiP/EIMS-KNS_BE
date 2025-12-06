using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Commands.UpdateStatus
{
    public class UpdateInvoiceStatusCommandHandler : IRequestHandler<UpdateInvoiceStatusCommand, Result>
    {
        private readonly IUnitOfWork _uow;

        public UpdateInvoiceStatusCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result> Handle(UpdateInvoiceStatusCommand request, CancellationToken cancellationToken)
        {
            var invoice = await _uow.InvoicesRepository.GetByIdAsync(request.InvoiceId);
            if (invoice == null)
                return Result.Fail($"Không tìm thấy hóa đơn với ID {request.InvoiceId}");
            var statusExists = await _uow.TaxApiStatusRepository.GetAllQueryable()
                                        .AnyAsync(x => x.TaxApiStatusID == request.NewStatusId);
            int oldStatusId = invoice.InvoiceStatusID;

            if (oldStatusId == request.NewStatusId)
                return Result.Ok();
            invoice.InvoiceStatusID = request.NewStatusId;
            var history = new InvoiceHistory
            {
                InvoiceID = invoice.InvoiceID,
                ActionType = "Manual Status Update", 
                Date = DateTime.UtcNow,
                // PerformedBy = ... (Lấy từ UserID trong Claims)
            };
            await _uow.InvoiceHistoryRepository.CreateAsync(history);
            await _uow.InvoicesRepository.UpdateAsync(invoice);

            await _uow.SaveChanges();

            return Result.Ok();
        }
    }
}
