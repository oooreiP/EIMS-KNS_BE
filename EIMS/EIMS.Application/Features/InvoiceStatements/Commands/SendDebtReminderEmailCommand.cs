using FluentResults;
using MediatR;
using System.Text.Json.Serialization;

namespace EIMS.Application.Features.InvoiceStatements.Commands
{
    public class SendDebtReminderEmailCommand : IRequest<Result>
    {
        public int StatementId { get; set; }
        [JsonIgnore]
        public string? Subject { get; set; }
        [JsonIgnore]
        [JsonIgnore]
        public string? Message { get; set; }
        public bool IncludePdf { get; set; } = true;

        [JsonIgnore]
        public string RootPath { get; set; } = string.Empty;
    }
}