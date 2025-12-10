using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoicePayment.Commands
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Result<int>>
    {
        private readonly IUnitOfWork _uow;
        public CreatePaymentCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<Result<int>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            await using var transaction = await _uow.BeginTransactionAsync();
            try
            {
                //fetch invoice 
                var invoice = await _uow.InvoicesRepository.GetByIdAsync(request.InvoiceId, "Payments");
                if (invoice == null)
                    return Result.Fail($"Invoice with id {request.InvoiceId} not found");
                if (invoice.InvoiceStatusID == 1)
                {
                    return Result.Fail("Cannot add payment to a Draft invoice. Please Sign/Issue the invoice first.");
                }
                //calcute balance
                decimal currentlyPaid = invoice.Payments.Sum(p => p.AmountPaid);
                decimal remaining = invoice.TotalAmount - currentlyPaid;
                if (request.Amount > remaining)
                    return Result.Fail($"Payment amount ({request.Amount:N0}) exceeds remaining balance ({remaining:N0}).");
                //create payment
                var payment = new Domain.Entities.InvoicePayment
                {
                    InvoiceID = request.InvoiceId,
                    AmountPaid = request.Amount,
                    PaymentDate = request.PaymentDate ?? DateTime.UtcNow,
                    PaymentMethod = request.PaymentMethod,
                    TransactionCode = request.TransactionCode,
                    Note = request.Note,
                    CreatedBy = request.UserId
                };
                await _uow.InvoicePaymentRepository.CreateAsync(payment);
                var newTotalPaid = currentlyPaid + request.Amount;
                if (newTotalPaid >= invoice.TotalAmount)
                {
                    invoice.PaymentStatusID = 3;
                }
                else if (newTotalPaid > 0)
                {
                    invoice.PaymentStatusID = 2;
                }
                await _uow.InvoicesRepository.UpdateAsync(invoice);
                // 5. Commit
                await _uow.SaveChanges();
                await _uow.CommitAsync();
                return Result.Ok(payment.PaymentID);
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                return Result.Fail(new Error(ex.Message));
            }
        }
    }
}