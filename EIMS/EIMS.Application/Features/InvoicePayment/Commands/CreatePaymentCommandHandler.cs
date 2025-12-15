using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.InvoicePayment;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoicePayment.Commands
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Result<InvoicePaymentDTO>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CreatePaymentCommandHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Result<InvoicePaymentDTO>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
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
                var relatedStatements = await _uow.InvoiceStatementRepository
            .GetStatementsContainingInvoiceAsync(request.InvoiceId);

                foreach (var stmt in relatedStatements)
                {
                    // A. Update the Paid Amount on the statement
                    stmt.PaidAmount += request.Amount;

                    // B. Recalculate Statement Status
                    // 5 = Paid, 4 = Partially Paid, 3 = Sent
                    if (stmt.PaidAmount >= stmt.TotalAmount)
                    {
                        stmt.StatusID = 5; // Fully Paid
                    }
                    else if (stmt.PaidAmount > 0)
                    {
                        // Only switch to "Partially Paid" if it's currently "Sent" or "Draft"
                        // If it was already "Partially Paid", this keeps it there.
                        stmt.StatusID = 4;
                    }

                    // C. Update the Statement in DB
                    await _uow.InvoiceStatementRepository.UpdateAsync(stmt);
                }   
                // 5. Commit
                await _uow.SaveChanges();
                await _uow.CommitAsync();
                return Result.Ok(_mapper.Map<InvoicePaymentDTO>(payment));
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                return Result.Fail(new Error(ex.Message));
            }
        }
    }
}