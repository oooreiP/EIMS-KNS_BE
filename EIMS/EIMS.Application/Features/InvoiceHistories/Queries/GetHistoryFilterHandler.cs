using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.InvoiceHistories;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceHistories.Queries
{
    public class GetHistoryFilterHandler : IRequestHandler<GetHistoryFilterQuery, Result<List<InvoiceHistoryDto>>>
    {
        private readonly IUnitOfWork _uow;

        public GetHistoryFilterHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<Result<List<InvoiceHistoryDto>>> Handle(GetHistoryFilterQuery request, CancellationToken token)
        {
            var query = _uow.InvoiceHistoryRepository.GetAllQueryable();

            // 1. Filter Date
            if (request.FromDate.HasValue)
                query = query.Where(x => x.Date >= request.FromDate.Value);

            if (request.ToDate.HasValue)
                query = query.Where(x => x.Date <= request.ToDate.Value);

            // 2. Filter Action
            if (!string.IsNullOrEmpty(request.ActionType))
                query = query.Where(x => x.ActionType == request.ActionType);

            // 3. Filter Invoice
            if (request.InvoiceId.HasValue)
                query = query.Where(x => x.InvoiceID == request.InvoiceId.Value);

            // 4. Projection
            var list = await query
                .Include(x => x.Performer)
                .OrderByDescending(x => x.Date)
                .Take(100)
                .Select(x => new InvoiceHistoryDto
                {
                    HistoryID = x.HistoryID,
                    InvoiceID = x.InvoiceID,
                    ActionType = x.ActionType,
                    Date = x.Date,
                    PerformedBy = x.PerformedBy,
                    PerformerName = x.Performer != null ? x.Performer.FullName : "System",
                    ReferenceInvoiceID = x.ReferenceInvoiceID
                })
                .ToListAsync(token);

            return Result.Ok(list);
        }
    }
}
