using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Notifications.Commands
{
    public class MarkNotificationReadCommand : IRequest<Result>
    {
        public int NotificationID { get; set; }
    }
}
