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
    public class GetSystemActivityLogsQuery : IRequest<Result<PaginatedList<SystemActivityLogDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        public string? UserId { get; set; }
        public string? IpAddress { get; set; } 
        public string? ActionName { get; set; } 
        public string? Status { get; set; }    
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
