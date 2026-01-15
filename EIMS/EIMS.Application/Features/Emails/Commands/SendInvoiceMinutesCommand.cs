using EIMS.Domain.Enums;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Emails.Commands
{
    public class SendInvoiceMinutesCommand : IRequest<Result>
    {
        public int InvoiceId { get; set; }
        public MinutesType Type { get; set; } 
        public string Reason { get; set; }    
        public string? ContentBefore { get; set; } 
        public string? ContentAfter { get; set; }  
        public DateTime AgreementDate { get; set; } = DateTime.Now; 

        public int? EmailTemplateId { get; set; }
        public string? RecipientEmail { get; set; }
        public List<string>? CcEmails { get; set; }
        public List<string>? BccEmails { get; set; }
        public string? CustomMessage { get; set; }
        public string? CertificateSerial { get; set; }
        public bool IncludeXml { get; set; } = true;
        public bool IncludePdf { get; set; } = true;
    }
}
