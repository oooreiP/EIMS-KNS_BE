using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Minutes.Commands
{
    public class CompleteMinuteInvoiceCommand : IRequest<Result<bool>>
    {
        public int MinuteInvoiceId { get; set; }

        public CompleteMinuteInvoiceCommand(int minuteInvoiceId)
        {
            MinuteInvoiceId = minuteInvoiceId;
        }
    }
}
