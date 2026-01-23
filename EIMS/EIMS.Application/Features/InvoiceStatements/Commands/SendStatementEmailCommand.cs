using FluentResults;
using MediatR;
using System.Collections.Generic;

namespace EIMS.Application.Features.InvoiceStatements.Commands
{
    public class SendStatementEmailCommand : IRequest<Result>
    {
        public int StatementId { get; set; }
        public string? RecipientEmail { get; set; }
        public List<string>? CcEmails { get; set; }
        public List<string>? BccEmails { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public bool IncludePdf { get; set; } = true;
        public string RootPath { get; set; } = string.Empty;
    }
}
