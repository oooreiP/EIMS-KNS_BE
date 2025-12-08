using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Emails.Commands
{
    public class SendInvoiceEmailCommand : IRequest<Result>
    {
        [JsonIgnore]
        public int InvoiceId { get; set; }
        public int? EmailTemplateId { get; set; }
        public string? RecipientEmail { get; set; } 
        public List<string>? CcEmails { get; set; }
        public List<string>? BccEmails { get; set; }
        public string? CustomMessage { get; set; }
        public bool IncludeXml { get; set; } = false;
        public bool IncludePdf { get; set; } = true;
        public string? Language { get; set; } = "vi";
        public List<string>? ExternalAttachmentUrls { get; set; }
    }
}
