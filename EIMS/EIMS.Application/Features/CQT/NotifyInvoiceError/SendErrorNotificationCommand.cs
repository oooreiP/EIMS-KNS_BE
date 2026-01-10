using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.CQT.NotifyInvoiceError
{
    public class SendErrorNotificationCommand : IRequest<Result<string>>
    {
        public int NotificationID { get; set; }
    }
}
