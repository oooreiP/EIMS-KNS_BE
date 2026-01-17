using EIMS.Application.DTOs.Invoices;
using EIMS.Application.DTOs.Requests;
using EIMS.Application.Features.Invoices.Commands;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceRequests.Queries
{
    public class GetSaleRequestByIDQuery : IRequest<Result<FillInvoiceForm>>
    {
        public int RequestId { get; set; }
        public GetSaleRequestByIDQuery(int id) => RequestId = id;
    }
}
