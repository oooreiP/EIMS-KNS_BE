using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.TaxAPIDTO;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.ErrorNotifications.Queries
{
    public class GetErrorNotificationByIdHandler : IRequestHandler<GetErrorNotificationByIdQuery, Result<ErrorNotificationDto>>
    {
        private readonly IUnitOfWork _uow;

        public GetErrorNotificationByIdHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<ErrorNotificationDto>> Handle(GetErrorNotificationByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _uow.ErrorNotificationRepository.GetWithDetailsAsync(request.Id);
            if (entity == null) return Result.Fail("Không tìm thấy thông báo sai sót.");

            // Map Entity sang DTO
            var dto = new ErrorNotificationDto
            {
                Id = entity.InvoiceErrorNotificationID,
                NotificationNumber = entity.NotificationNumber,
                NotificationTypeCode = entity.NotificationTypeCode,
                TaxAuthorityName = entity.TaxAuthorityName,
                CreatedDate = entity.ReportDate,
                CreatedBy = entity.CreatedBy,
                StatusCode = entity.Status,
                Status = GetStatusName(entity.Status),
                MTDiep = entity.MTDiep,
                XMLPath = entity.XMLPath,
                TaxResponsePath = entity.TaxResponsePath,   
                Place = entity.Place,
                Details = entity.Details.Select(d => new ErrorNotificationDetailDto
                {
                    InvoiceId = d.InvoiceID ?? 0,
                    InvoiceSerial = d.InvoiceSerial,
                    InvoiceNumber = d.InvoiceNumber,
                    InvoiceDate = d.InvoiceDate,
                    ErrorType = d.ErrorType,
                    ErrorTypeName = GetErrorTypeName(d.ErrorType),
                    Reason = d.Reason
                }).ToList()
            };

            return Result.Ok(dto);
        }
        private string GetStatusName(int status) => status switch
        {
            1 => "Nháp",
            2 => "Đã ký",
            3 => "Đã gửi T-VAN",
            4 => "CQT Tiếp nhận",
            5 => "CQT Từ chối",
            _ => "Không xác định"
        };

        private string GetErrorTypeName(int type) => type switch
        {
            1 => "Hủy",
            2 => "Điều chỉnh",
            3 => "Thay thế",
            4 => "Giải trình",
            _ => "Khác"
        };
    }
}
