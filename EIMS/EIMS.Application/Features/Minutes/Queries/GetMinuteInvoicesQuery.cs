using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.Minutes;
using EIMS.Domain.Enums;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Minutes.Queries
{
    public class GetMinuteInvoicesQuery : IRequest<Result<PaginatedList<MinuteInvoiceDto>>>
    {

        public int? SaleId { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; } 
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public EMinuteStatus? Status { get; set; } 
        public MinutesType? MinuteType { get; set; } 
    }
}
