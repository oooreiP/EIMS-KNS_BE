using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Notifications.Queries
{
    public class GetMyNotificationsHandler : IRequestHandler<GetMyNotificationsQuery, Result<PaginatedList<NotificationDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUserService _currentUser; 
        private readonly IMapper _mapper;

        public GetMyNotificationsHandler(IUnitOfWork uow, ICurrentUserService currentUser, IMapper mapper)
        {
            _uow = uow;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedList<NotificationDto>>> Handle(GetMyNotificationsQuery request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(_currentUser.UserId); // Lấy ID user hiện tại

            var query = _uow.NotificationRepository.GetAllQueryable()
                .Include(x => x.NotificationStatus)
                .Include(x => x.NotificationType)
                .Where(x => x.UserID == userId);

            // Filter chưa đọc (StatusID = 1)
            if (request.OnlyUnread == true)
            {
                query = query.Where(x => x.NotificationStatusID == 1);
            }

            query = query.OrderByDescending(x => x.Time); // Mới nhất lên đầu

            // Map sang DTO
            var dtos = query.Select(x => new NotificationDto
            {
                NotificationID = x.NotificationID,
                Content = x.Content,
                StatusName = x.NotificationStatus.StatusName,
                IsRead = x.NotificationStatusID == 2, // Giả định 2 là Đã đọc
                TypeName = x.NotificationType.TypeName,
                Time = x.Time
            });

            var list = await PaginatedList<NotificationDto>.CreateAsync(dtos, request.PageIndex, request.PageSize);
            return Result.Ok(list);
        }
    }
}
