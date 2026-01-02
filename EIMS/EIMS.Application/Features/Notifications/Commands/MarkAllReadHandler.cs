using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Notifications.Commands
{
    public class MarkAllReadHandler : IRequestHandler<MarkAllReadCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUserService _currentUser;

        public MarkAllReadHandler(IUnitOfWork uow, ICurrentUserService currentUser)
        {
            _uow = uow;
            _currentUser = currentUser;
        }

        public async Task<Result> Handle(MarkAllReadCommand request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(_currentUser.UserId);

            // Lấy tất cả tin chưa đọc của user này
            var unreadNotifications = await _uow.NotificationRepository.GetAllQueryable()
                .Where(x => x.UserID == userId && x.NotificationStatusID == 1)
                .ToListAsync();

            if (unreadNotifications.Any())
            {
                foreach (var noti in unreadNotifications)
                {
                    noti.NotificationStatusID = 2; // 2 = Đã đọc
                }

                // Dùng UpdateRange cho tối ưu
                _uow.NotificationRepository.UpdateRange(unreadNotifications);
                await _uow.SaveChanges();
            }

            return Result.Ok();
        }
    }
}
