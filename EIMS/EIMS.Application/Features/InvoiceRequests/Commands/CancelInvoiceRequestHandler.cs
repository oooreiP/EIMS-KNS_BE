using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceRequests.Commands
{
    public class CancelInvoiceRequestHandler : IRequestHandler<CancelInvoiceRequestCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;
        public CancelInvoiceRequestHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<bool>> Handle(CancelInvoiceRequestCommand request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(_currentUser.UserId);
            if (userId == 0)
            {
                return Result.Fail(new Error($"Đăng nhập trước khi thực hiện.")
                    .WithMetadata("ErrorCode", "Unauthorize"));
            }
            var user = await _unitOfWork.UserRepository.GetAllQueryable()
                                        .Include(u => u.Role)
                                        .FirstOrDefaultAsync(u => u.UserID == userId, cancellationToken);

            if (user == null)
            {
                return Result.Fail(new Error($"Không thể tìm thấy người dùng với ID là {userId}")
                    .WithMetadata("ErrorCode", "User.NotFound"));
            }
            if (!user.Role.RoleName.Equals("Sale", StringComparison.OrdinalIgnoreCase))
                return Result.Fail(new Error("Chỉ có Sale mới được phép tạo Invoice Request"));          
            var invoiceRequest = await _unitOfWork.InvoiceRequestRepository.GetByIdAsync(request.RequestId);

            if (invoiceRequest == null)
            {
                return Result.Fail<bool>($"Không tìm thấy yêu cầu xuất hóa đơn với ID {request.RequestId}");
            }
            if (invoiceRequest.RequestStatusID != 1) 
            {
                return Result.Fail<bool>("Không thể hủy yêu cầu đã xử lý.");
            }
            if (user.UserID != invoiceRequest.SaleID)
            {
                return Result.Fail(new Error($"Không thể sửa yêu cầu của người khác")
                    .WithMetadata("ErrorCode", "Unauthorize"));
            }
            invoiceRequest.RequestStatusID = 4;
            try
            {
                await _unitOfWork.InvoiceRequestRepository.UpdateAsync(invoiceRequest);
                await _unitOfWork.SaveChanges();

                return Result.Ok(true);
            }
            catch (Exception ex)
            {
                return Result.Fail<bool>($"Lỗi khi hủy yêu cầu: {ex.Message}");
            }
        }
    }
}
