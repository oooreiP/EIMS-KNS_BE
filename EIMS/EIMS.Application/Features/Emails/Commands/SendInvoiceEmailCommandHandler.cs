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

        public SendInvoiceEmailCommandHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<Result> Handle(SendInvoiceEmailCommand request, CancellationToken cancellationToken)
        {
            return await _emailService.SendInvoiceEmailAsync(
                request.RecipientEmail,
                request.CustomerName,
                request.InvoiceNumber,
                request.TotalAmount,
                request.Message,
                request.CloudinaryUrls
            );
        }
    }
}
