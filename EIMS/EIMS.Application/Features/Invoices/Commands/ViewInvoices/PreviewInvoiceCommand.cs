using EIMS.Application.Features.Invoices.Commands.CreateInvoice;
using FluentResults;
using MediatR;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Commands.ViewInvoices
{
    public class PreviewInvoiceCommand : BaseInvoiceCommand, IRequest<Result<string>>
    {
        [JsonIgnore]
        public string? RootPath { get; set; }
    }
}
