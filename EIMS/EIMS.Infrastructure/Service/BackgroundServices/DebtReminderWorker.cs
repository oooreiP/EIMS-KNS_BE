using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Infrastructure.Service.BackgroundServices
{
    public class DebtReminderWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DebtReminderWorker> _logger;

        public DebtReminderWorker(IServiceProvider serviceProvider, ILogger<DebtReminderWorker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;

                // Logic: Chỉ chạy lúc 8h sáng giờ Việt Nam (UTC+7) => 1h sáng UTC
                if (now.Hour == 1 && now.Minute == 0)
                {
                    await CheckAndNotifyDebt(stoppingToken);

                    // Ngủ 23 tiếng để tránh chạy lại trong cùng 1 ngày
                    await Task.Delay(TimeSpan.FromHours(23), stoppingToken);
                }
                else
                {
                    // Check lại mỗi phút
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
            }
        }
        private async Task CheckAndNotifyDebt(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var notiService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                var today = DateTime.UtcNow.Date;
                var nextWeek = today.AddDays(7); 
                var statements = await uow.InvoiceStatementRepository.GetAllQueryable()
                    .Where(s =>
                        (s.DueDate.Date == today || s.DueDate.Date == nextWeek) &&
                        s.PaidAmount < s.TotalAmount &&
                        s.StatusID != 6
                    )
                    .ToListAsync(stoppingToken);

                int count = 0;

                foreach (var stmt in statements)
                {
                    var customerUsers = await uow.UserRepository.GetUsersByCustomerIdAsync(stmt.CustomerID);

                    if (!customerUsers.Any()) continue;
                    decimal balanceDue = stmt.TotalAmount - stmt.PaidAmount;
                    string content;
                    if (stmt.DueDate.Date == today)
                    {
                        content = $"[QUÁ HẠN] Hôm nay là hạn thanh toán Bảng kê #{stmt.StatementCode}. Số tiền: {balanceDue:N0}đ.";
                    }
                    else
                    {
                        content = $"[SẮP ĐẾN HẠN] Bảng kê #{stmt.StatementCode} sẽ hết hạn sau 7 ngày nữa ({stmt.DueDate:dd/MM}). Số tiền: {balanceDue:N0}đ.";
                    }

                    // 5. Gửi cho tất cả User của khách hàng đó
                    foreach (var user in customerUsers)
                    {
                        await notiService.SendToUserAsync(
                            user.UserID,
                            content,
                            4
                        );
                    }
                    count++;
                }

                if (count > 0)
                {
                    _logger.LogInformation($"[DebtWorker] Đã gửi nhắc nợ cho {count} bảng kê (Gồm cả đến hạn và sắp đến hạn).");
                }
            }
        }
    }
}
