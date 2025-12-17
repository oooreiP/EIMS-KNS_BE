using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Queries
{
    public class GetInvoiceHtmlViewQuery : IRequest<Result<string>>
    {
        public int InvoiceId { get; set; }
        public string RootPath { get; set; }

        public GetInvoiceHtmlViewQuery(int invoiceId, string rootPath)
        {
            InvoiceId = invoiceId;
            RootPath = rootPath;
        }
    }
}
