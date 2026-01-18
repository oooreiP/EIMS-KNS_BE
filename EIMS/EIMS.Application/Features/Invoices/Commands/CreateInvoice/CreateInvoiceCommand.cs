using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs;
using EIMS.Application.DTOs.InvoiceItem;
using EIMS.Application.DTOs.Invoices;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceCommand : BaseInvoiceCommand, IRequest<Result<CreateInvoiceResponse>>,IAuthenticatedCommand
    {
        public int? RequestID { get; set; }
        public int AuthenticatedUserId { get; set; }
        public int? CustomerId { get; set; }
    }
}
