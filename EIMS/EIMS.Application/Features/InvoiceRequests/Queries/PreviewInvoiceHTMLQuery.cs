using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceRequests.Queries
{
    public class PreviewInvoiceHTMLQuery : IRequest<Result<string>>
    {
        public int RequestId { get; set; }
        public string RootPath { get; set; }
    }
}
