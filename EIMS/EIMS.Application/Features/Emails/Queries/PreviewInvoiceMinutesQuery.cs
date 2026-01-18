using EIMS.Application.DTOs;
using EIMS.Application.DTOs.Mails;
using EIMS.Domain.Enums;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Emails.Queries
{
    public class PreviewInvoiceMinutesQuery : IRequest<Result<FileAttachment>>
    {
        public int InvoiceId { get; set; } 
        public MinutesType Type { get; set; } 
        public string Reason { get; set; }
        public DateTime AgreementDate { get; set; }
        public string? ContentBefore { get; set; }
        public string? ContentAfter { get; set; }
    }
}
