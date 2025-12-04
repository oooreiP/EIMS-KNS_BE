using EIMS.Application.Commons.Interfaces;
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
            var result = await _emailService.SendInvoiceEmailAsync(
                request.RecipientEmail,
                request.invoiceId,
                request.Message
            );
            if (result.IsSuccess)
            {
              var invoice =  await _uow.InvoicesRepository.GetByIdAsync(request.invoiceId);
              invoice.InvoiceStatusID = 7;
              await _uow.InvoicesRepository.UpdateAsync(invoice);
              await _uow.InvoicesRepository.SaveChangesAsync();
            }
            return result;
        }
    }
}
