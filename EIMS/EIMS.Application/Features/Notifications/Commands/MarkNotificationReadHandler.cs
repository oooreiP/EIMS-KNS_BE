using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Notifications.Commands
{
    public class MarkNotificationReadHandler : IRequestHandler<MarkNotificationReadCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUserService _currentUser;

        public MarkNotificationReadHandler(IUnitOfWork uow, ICurrentUserService currentUser)
        {
            _uow = uow;
            _currentUser = currentUser;
        }

        public async Task<Result> Handle(MarkNotificationReadCommand request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(_currentUser.UserId);

            var noti = await _uow.NotificationRepository.GetByIdAsync(request.NotificationID);

            if (noti == null) return Result.Fail("Không tìm thấy thông báo");
            if (noti.UserID != userId) return Result.Fail("Bạn không sở hữu thông báo này");
            noti.NotificationStatusID = 2;
            await _uow.NotificationRepository.UpdateAsync(noti);
            await _uow.SaveChanges();

            return Result.Ok();
        }
    }
}
