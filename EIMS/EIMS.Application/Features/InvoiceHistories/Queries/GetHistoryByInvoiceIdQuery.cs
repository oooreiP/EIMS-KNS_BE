using EIMS.Application.DTOs.InvoiceHistories;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceHistories.Queries
{
    public class GetHistoryByInvoiceIdQuery : IRequest<Result<List<InvoiceHistoryDto>>>
    {
        public int InvoiceId { get; set; }
        public GetHistoryByInvoiceIdQuery(int invoiceId) => InvoiceId = invoiceId;
    }   
}
