using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using EIMS.Domain.Enums;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceRequests.Commands
{
    public class RejectInvoiceRequestHandler : IRequestHandler<RejectInvoiceRequestCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notiService; // Để báo lại cho Sale

        public RejectInvoiceRequestHandler(IUnitOfWork unitOfWork, INotificationService notiService)
        {
            _unitOfWork = unitOfWork;
            _notiService = notiService;
        }

        public async Task<Result<int>> Handle(RejectInvoiceRequestCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.RejectReason))
                return Result.Fail("Vui lòng nhập lý do từ chối.");
            var request = await _unitOfWork.InvoiceRequestRepository.GetByIdAsync(command.RequestId);
            if (request == null)
                return Result.Fail($"Không tìm thấy yêu cầu số {command.RequestId}");
            if (request.RequestStatusID != (int)EInvoiceRequestStatus.Pending)
            {
                return Result.Fail("Chỉ có thể từ chối yêu cầu đang ở trạng thái Chờ duyệt.");
            }

            try
            {
                request.RequestStatusID = (int)EInvoiceRequestStatus.Rejected;
                request.Notes = command.RejectReason;
                _unitOfWork.InvoiceRequestRepository.UpdateAsync(request);
                await _unitOfWork.SaveChanges();
                if (request.SaleID.HasValue)
                {
                    string msg = $"Yêu cầu xuất hóa đơn #{request.RequestID} đã bị từ chối. Lý do: {command.RejectReason}";
                    await _notiService.SendToUserAsync(request.SaleID.Value, msg, typeId: 3); 
                }

                return Result.Ok(request.RequestID);
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error($"Lỗi khi từ chối yêu cầu: {ex.Message}").CausedBy(ex));
            }
        }
    }
}
