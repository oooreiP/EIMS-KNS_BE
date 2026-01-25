using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Minutes.Commands
{
    public class MinuteSignedEvent : INotification
    {
        public int MinuteId { get; }
        public MinuteSignedEvent(int minuteId)
        {
            MinuteId = minuteId;
        }
    }
}
