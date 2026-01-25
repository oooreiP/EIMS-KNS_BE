using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Enums;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Minutes.Commands
{
    public class CompleteMinuteInvoiceCommandHandler : IRequestHandler<CompleteMinuteInvoiceCommand, Result<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUserService _currentUser;

        public CompleteMinuteInvoiceCommandHandler(IUnitOfWork uow, ICurrentUserService currentUser)
        {
            _uow = uow;
            _currentUser = currentUser;
        }

        public async Task<Result<bool>> Handle(CompleteMinuteInvoiceCommand request, CancellationToken cancellationToken)
        {
            var minute = await _uow.MinuteInvoiceRepository.GetByIdAsync(request.MinuteInvoiceId);

            if (minute == null)
            {
                return Result.Fail("Không tìm thấy biên bản này.");
            }

            // 2. Validate Trạng thái hiện tại
            if (minute.Status == EMinuteStatus.Complete)
            {
                return Result.Fail("Biên bản này đã hoàn thành rồi, không cần cập nhật lại.");
            }

            if (minute.Status == EMinuteStatus.Cancelled)
            {
                return Result.Fail("Không thể hoàn thành biên bản đã bị hủy.");
            }
            minute.IsBuyerSigned = true;
            minute.Status = EMinuteStatus.Complete;

            // minute.UpdatedBy = int.Parse(_currentUser.UserId);
            // minute.UpdatedAt = DateTime.UtcNow;

            // 5. Lưu vào DB
            await _uow.MinuteInvoiceRepository.UpdateAsync(minute);
            await _uow.SaveChanges();
            return Result.Ok(true);
        }
    }
}
