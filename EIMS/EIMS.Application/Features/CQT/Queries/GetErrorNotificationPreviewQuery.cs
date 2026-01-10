using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.CQT.Queries
{
    public class GetErrorNotificationPreviewQuery : IRequest<Result<string>>
    {
        public int NotificationId { get; set; }
    }
}
