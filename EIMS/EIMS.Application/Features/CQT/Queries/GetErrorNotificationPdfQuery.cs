using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.CQT.Queries
{
    public class GetErrorNotificationPdfQuery : IRequest<Result<byte[]>>
    {
        public int NotificationId { get; set; }
    }
}
