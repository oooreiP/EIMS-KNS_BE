using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.LogsDTO;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.AuditLogs.Queries
{
    public class GetAuditLogsQuery : IRequest<Result<PaginatedList<AuditLogDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        public int? UserID { get; set; }
        public string? TableName { get; set; } // Lọc theo bảng (VD: Invoices)
        public string? Action { get; set; }    // Lọc theo hành động (VD: Modified)
        public string? SearchTerm { get; set; } // Tìm kiếm chung trong Old/New Values
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
