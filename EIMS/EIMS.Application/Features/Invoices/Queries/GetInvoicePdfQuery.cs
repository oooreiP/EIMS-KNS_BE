using EIMS.Application.DTOs.File;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Queries
{
    public class GetInvoicePdfQuery : IRequest<Result<InvoicePdfDto>>
    {
        public int InvoiceId { get; set; }
        public string RootPath { get; set; }

        public GetInvoicePdfQuery(int invoiceId, string rootPath)
        {
            InvoiceId = invoiceId;
            RootPath = rootPath;
        }
    }  
}
