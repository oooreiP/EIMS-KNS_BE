using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Emails.Commands
{
    public class SendInvoiceEmailCommandHandler : IRequestHandler<SendInvoiceEmailCommand, Result>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public SendInvoiceEmailCommandHandler(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task<Result> Handle(SendInvoiceEmailCommand request, CancellationToken cancellationToken)
        {
            // 1. Chạy tác vụ ngầm (Fire-and-Forget)
            // Task.Run sẽ đẩy việc này sang một Thread khác, không chặn luồng chính
            Task.Run(async () =>
            {
                await ProcessEmailInBackground(request);
            });

            // 2. Trả về kết quả NGAY LẬP TỨC cho FE
            // Lưu ý: Lúc này chưa biết gửi thành công hay thất bại, chỉ biết là "Đã tiếp nhận"
            return Task.FromResult(Result.Ok());
        }

        // Hàm xử lý logic nằm tách biệt
        private async Task ProcessEmailInBackground(SendInvoiceEmailCommand request)
        {
            // BẮT BUỘC: Tạo Scope mới. Tất cả Service (UOW, EmailService) phải lấy từ scope này.
            using (var scope = _scopeFactory.CreateScope())
            {
                // Resolve các service cần thiết trong Scope mới
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<SendInvoiceEmailCommandHandler>>();

                try
                {
                    logger.LogInformation($"Bắt đầu gửi email ngầm cho Invoice {request.InvoiceId}...");

                    // 1. Gọi Email Service (Logic cũ)
                    var result = await emailService.SendInvoiceEmailAsync(request);

                    if (result.IsSuccess)
                    {
                        // 2. Cập nhật DB (Logic cũ) - Dùng 'uow' mới tạo, không dùng _uow cũ
                        var invoice = await uow.InvoicesRepository.GetByIdAsync(request.InvoiceId);
                        if (invoice != null)
                        {
                            invoice.InvoiceStatusID = 3; // Ví dụ trạng thái Sent
                            await uow.InvoicesRepository.UpdateAsync(invoice);

                            // Lưu lịch sử
                            var history = new InvoiceHistory
                            {
                                InvoiceID = request.InvoiceId,
                                ActionType = "Email Sent",
                                Date = DateTime.UtcNow
                            };
                            await uow.InvoiceHistoryRepository.CreateAsync(history);

                            await uow.SaveChanges();
                            logger.LogInformation($"Gửi email thành công và đã cập nhật Invoice {request.InvoiceId}");
                        }
                    }
                    else
                    {
                        logger.LogError($"Gửi email thất bại cho Invoice {request.InvoiceId}. Lỗi: {result.Errors.FirstOrDefault()?.Message}");
                        // TODO: Có thể update trạng thái hóa đơn thành "Gửi lỗi" nếu cần
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"CRITICAL ERROR khi chạy Background Job cho Invoice {request.InvoiceId}");
                }
            } 
        }
    }
}
