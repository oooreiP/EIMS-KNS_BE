using EIMS.Application.DTOs.InvoiceHistories;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceHistories.Queries
{
    public class GetHistoryFilterQuery : IRequest<Result<List<InvoiceHistoryDto>>>
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? ActionType { get; set; } 
        public int? InvoiceId { get; set; }    
    }
}
