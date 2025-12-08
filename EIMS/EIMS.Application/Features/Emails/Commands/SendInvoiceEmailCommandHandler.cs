using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Emails.Commands
{
    public class SendInvoiceEmailCommandHandler : IRequestHandler<SendInvoiceEmailCommand, Result>
    {
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _uow;

        public SendInvoiceEmailCommandHandler(IEmailService emailService, IUnitOfWork uow)
        {
            _emailService = emailService;
            _uow = uow;
        }

        public async Task<Result> Handle(SendInvoiceEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _emailService.SendInvoiceEmailAsync(request);
            if (result.IsSuccess)
            {
                var invoice = await _uow.InvoicesRepository.GetByIdAsync(request.InvoiceId);
                invoice.InvoiceStatusID = 3;
                await _uow.InvoicesRepository.UpdateAsync(invoice);
                await _uow.InvoicesRepository.SaveChangesAsync();
                var history = new InvoiceHistory
                {
                    InvoiceID = request.InvoiceId,
                    ActionType = "Email Sent",
                    Date = DateTime.UtcNow
                };

                await _uow.InvoiceHistoryRepository.CreateAsync(history);
                await _uow.SaveChanges();
            }
            return result;
        }
    }
}
