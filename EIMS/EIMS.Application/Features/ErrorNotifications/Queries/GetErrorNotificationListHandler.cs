using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.TaxAPIDTO;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.ErrorNotifications.Queries
{
    public class GetErrorNotificationListHandler : IRequestHandler<GetErrorNotificationListQuery, Result<PaginatedList<ErrorNotificationDto>>>
    {
        private readonly IUnitOfWork _uow;

        public GetErrorNotificationListHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<PaginatedList<ErrorNotificationDto>>> Handle(GetErrorNotificationListQuery request, CancellationToken cancellationToken)
        {
            var query = _uow.ErrorNotificationRepository.GetAllQueryable();
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.NotificationNumber.Contains(request.Keyword));
            }

            if (request.Status.HasValue && request.Status.Value > 0)
            {
                query = query.Where(x => x.Status == request.Status.Value);
            }

            if (request.CreatedBy.HasValue)
            {
                query = query.Where(x => x.CreatedBy == request.CreatedBy.Value);
            }
            if (request.FromDate.HasValue)
            {
                query = query.Where(x => x.ReportDate >= request.FromDate.Value);
            }

            if (request.ToDate.HasValue)
            {
                var endDate = request.ToDate.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(x => x.ReportDate <= endDate);
            }
            query = query.OrderByDescending(x => x.CreatedAt);
            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(entity => new ErrorNotificationDto
                {
                    Id = entity.InvoiceErrorNotificationID,
                    NotificationNumber = entity.NotificationNumber,
                    NotificationTypeCode = entity.NotificationTypeCode,
                    TaxAuthorityName = entity.TaxAuthorityName,
                    Place = entity.Place,
                    CreatedDate = entity.ReportDate,
                    StatusCode = entity.Status,
                    TaxResponsePath = entity.TaxResponsePath,
                    XMLPath = entity.XMLPath,
                    MTDiep = entity.MTDiep,
                    Status = entity.Status == 1 ? "Nháp" :
                             entity.Status == 2 ? "Đã ký" :
                             entity.Status == 3 ? "Đã gửi" :
                             entity.Status == 4 ? "Thành công" : "Thất bại",
                    InvoiceSerial = entity.Details.Count() == 1
                        ? entity.Details.FirstOrDefault().InvoiceSerial
                        : null,

                    InvoiceNumber = entity.Details.Count() == 1
                        ? entity.Details.FirstOrDefault().InvoiceNumber.ToString() 
                        : null,

                    InvoiceDate = entity.Details.Count() == 1
                        ? entity.Details.FirstOrDefault().InvoiceDate
                        : null,

                    CustomerName = entity.Details.Count() == 1
                        ? entity.Details.FirstOrDefault().Invoice.InvoiceCustomerName 
                        : null,

                    TotalAmount = entity.Details.Count() == 1
                        ? entity.Details.FirstOrDefault().Invoice.TotalAmount
                        : null
                })
                .ToListAsync(cancellationToken);

            return Result.Ok(new PaginatedList<ErrorNotificationDto>(items, totalCount, request.PageNumber, request.PageSize));
        }
    }
}
