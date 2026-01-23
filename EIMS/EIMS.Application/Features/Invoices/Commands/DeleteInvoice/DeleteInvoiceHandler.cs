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

namespace EIMS.Application.Features.Invoices.Commands.DeleteInvoice
{
    public class DeleteInvoiceHandler : IRequestHandler<DeleteInvoiceCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteInvoiceHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await _unitOfWork.InvoicesRepository.GetByIdAsync(request.InvoiceID);
            if (invoice == null)
            {
                return Result.Fail<bool>($"Không tìm thấy hóa đơn có ID: {request.InvoiceID}");
            }
            if (invoice.InvoiceStatusID != (int)EInvoiceStatus.Draft)
            {
                return Result.Fail<bool>("Không thể xóa hóa đơn này vì đã được Ký hoặc Phát hành. Bạn chỉ có thể Hủy (Cancel) hoặc Thay thế.");
            }

            await _unitOfWork.InvoicesRepository.DeleteAsync(invoice);

            try
            {
                await _unitOfWork.SaveChanges();
                return Result.Ok(true);
            }
            catch (Exception ex)
            {
                return Result.Fail<bool>($"Lỗi khi lưu thay đổi: {ex.Message}");
            }
        }
    }
}
