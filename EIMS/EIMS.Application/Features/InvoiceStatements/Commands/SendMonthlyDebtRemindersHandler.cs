using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceStatements.Commands
{
    public class SendMonthlyDebtRemindersHandler : IRequestHandler<SendMonthlyDebtRemindersCommand, Result<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly INotificationService _notiService; // [1] Inject Interface

        public SendMonthlyDebtRemindersHandler(IUnitOfWork uow, INotificationService notiService)
        {
            _uow = uow;
            _notiService = notiService;
        }

        public async Task<Result<int>> Handle(SendMonthlyDebtRemindersCommand request, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var statements = await _uow.InvoiceStatementRepository.GetUnpaidStatementsInMonthAsync(now.Month, now.Year);
            if (!statements.Any()) return Result.Ok(0);
            var notificationsToCreate = new List<Notification>();
            int totalCount = 0;

            foreach (var stmt in statements)
            {
                var customerUsers = await _uow.UserRepository.GetUsersByCustomerIdAsync(stmt.CustomerID);
                if (!customerUsers.Any()) continue;

                decimal balanceDue = stmt.TotalAmount - stmt.PaidAmount;
                string content = $"Nhắc nợ: Bảng kê #{stmt.StatementCode} hạn {stmt.DueDate:dd/MM} thiếu {balanceDue:N0}đ.";

                foreach (var user in customerUsers)
                {
                    // A. Gom dữ liệu để lưu DB sau (Nhiệm vụ của Handler)
                    notificationsToCreate.Add(new Notification
                    {
                        UserID = user.UserID,
                        Content = content,
                        NotificationStatusID = 1,
                        NotificationTypeID = 4,
                        Time = DateTime.UtcNow
                    });
                    await _notiService.SendRealTimeAsync(
                        user.UserID,
                        content,
                        4
                    );

                    totalCount++;
                }
            }
            if (notificationsToCreate.Any())
            {
                await _uow.NotificationRepository.CreateRangeAsync(notificationsToCreate);
                await _uow.SaveChanges();
            }

            return Result.Ok(totalCount);
        }
    }
}
