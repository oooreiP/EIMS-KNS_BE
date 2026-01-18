using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceRequests.Commands
{
    public class CancelInvoiceRequestCommand : IRequest<Result<bool>>
    {
        public int RequestId { get; set; }

        public CancelInvoiceRequestCommand(int requestId)
        {
            RequestId = requestId;
        }
    }
}
