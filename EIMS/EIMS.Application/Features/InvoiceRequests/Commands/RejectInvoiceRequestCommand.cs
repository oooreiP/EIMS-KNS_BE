using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceRequests.Commands
{
    public class RejectInvoiceRequestCommand : IRequest<Result<int>>
    {
        public int RequestId { get; set; }
        public string RejectReason { get; set; } 
    }
}
