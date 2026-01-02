using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Notifications.Queries
{
    public class GetMyNotificationsQuery : IRequest<Result<PaginatedList<NotificationDto>>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool? OnlyUnread { get; set; }
    }
}
