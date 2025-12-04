using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.TaxLogs.Commands
{
    public class CreateTaxLogCommand : IRequest<Result<int>>
    {
        public int InvoiceID { get; set; }
        public string RequestPayload { get; set; }
        public string? ResponsePayload { get; set; }
        public int TaxApiStatusID { get; set; }
    }
}
