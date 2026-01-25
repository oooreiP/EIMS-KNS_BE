using FluentResults;
using MediatR;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EIMS.Application.Features.InvoiceStatements.Commands
{
    public class SendStatementEmailCommand : IRequest<Result>
    {
        public int StatementId { get; set; }
        [JsonIgnore]
        public string? RecipientEmail { get; set; }
        public bool IncludePdf { get; set; } = true;
        public string RootPath { get; set; } = string.Empty;
    }
}
