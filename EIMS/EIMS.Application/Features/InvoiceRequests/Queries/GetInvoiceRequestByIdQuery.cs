using EIMS.Application.DTOs.Requests;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceRequests.Queries
{
    public class GetInvoiceRequestByIdQuery : IRequest<Result<GetInvoiceRequestDetailDto>>
    {
        public int RequestId { get; set; }
        public GetInvoiceRequestByIdQuery(int id) => RequestId = id;
    }
}
