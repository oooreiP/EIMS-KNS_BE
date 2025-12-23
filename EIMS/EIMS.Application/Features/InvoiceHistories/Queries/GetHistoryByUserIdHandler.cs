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
    public class GetHistoryByUserIdHandler : IRequestHandler<GetHistoryByUserIdQuery, Result<List<InvoiceHistoryDto>>>
    {
        private readonly IUnitOfWork _uow;

        public GetHistoryByUserIdHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<Result<List<InvoiceHistoryDto>>> Handle(GetHistoryByUserIdQuery request, CancellationToken token)
        {
            var list = await _uow.InvoiceHistoryRepository.GetAllQueryable()
                .Where(x => x.PerformedBy == request.UserId)
                .OrderByDescending(x => x.Date)
                .Include(x => x.Performer)
                .Select(x => new InvoiceHistoryDto
                {
                    HistoryID = x.HistoryID,
                    InvoiceID = x.InvoiceID,
                    ActionType = x.ActionType,
                    Date = x.Date,
                    PerformedBy = x.PerformedBy,
                    PerformerName = x.Performer.FullName,
                    ReferenceInvoiceID = x.ReferenceInvoiceID
                })
                .ToListAsync(token);

            return Result.Ok(list);
        }
    }
}
