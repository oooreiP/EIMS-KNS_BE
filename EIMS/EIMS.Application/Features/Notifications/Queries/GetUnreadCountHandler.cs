using EIMS.Application.Commons.Interfaces;
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
    public class GetUnreadCountHandler : IRequestHandler<GetUnreadCountQuery, Result<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUserService _currentUser;

        public GetUnreadCountHandler(IUnitOfWork uow, ICurrentUserService currentUser)
        {
            _uow = uow;
            _currentUser = currentUser;
        }

        public async Task<Result<int>> Handle(GetUnreadCountQuery request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(_currentUser.UserId);
            var count = await _uow.NotificationRepository.GetAllQueryable()
                .CountAsync(x => x.UserID == userId && x.NotificationStatusID == 1);

            return Result.Ok(count);
        }
    }
}
