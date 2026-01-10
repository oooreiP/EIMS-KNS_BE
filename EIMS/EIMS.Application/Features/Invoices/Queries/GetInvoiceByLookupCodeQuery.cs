using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs.Invoices;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Invoices.Queries
{
    public class GetInvoiceByLookupCodeQuery : IRequest<Result<InvoicePublicLookupResponse>>
    {
        public string LookupCode { get; set; }
        public string IPAddress { get; set; } // Lấy IP từ Controller truyền xuống
        public string UserAgent { get; set; }
    }
}