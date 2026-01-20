using EIMS.Application.Commons.Models;
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
    public class GetErrorNotificationListQuery : IRequest<Result<PaginatedList<ErrorNotificationDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Keyword { get; set; } 
        public int? Status { get; set; }   
        public int? CreatedBy { get; set; }   
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
