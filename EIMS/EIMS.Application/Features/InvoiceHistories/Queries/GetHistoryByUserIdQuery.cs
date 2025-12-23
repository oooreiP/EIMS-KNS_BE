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
    public class GetHistoryByUserIdQuery : IRequest<Result<List<InvoiceHistoryDto>>>
    {
        public int UserId { get; set; }
        public GetHistoryByUserIdQuery(int userId) => UserId = userId;
    }
}
