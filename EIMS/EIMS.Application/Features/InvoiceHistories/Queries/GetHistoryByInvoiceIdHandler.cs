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
    public class GetHistoryByInvoiceIdHandler : IRequestHandler<GetHistoryByInvoiceIdQuery, Result<List<InvoiceHistoryDto>>>
    {
        private readonly IUnitOfWork _uow;

        public GetHistoryByInvoiceIdHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<Result<List<InvoiceHistoryDto>>> Handle(GetHistoryByInvoiceIdQuery request, CancellationToken token)
        {
            // Giả sử Repository có GetQueryable()
            var query = _uow.InvoiceHistoryRepository.GetAllQueryable();

            var list = await query
                .Where(x => x.InvoiceID == request.InvoiceId)
                .Include(x => x.Performer)      
                .Include(x => x.ReferenceInvoice)
                .OrderByDescending(x => x.Date) 
                .Select(x => new InvoiceHistoryDto
                {
                    HistoryID = x.HistoryID,
                    InvoiceID = x.InvoiceID,
                    ActionType = x.ActionType,
                    Date = x.Date,
                    PerformedBy = x.PerformedBy,
                    PerformerName = x.Performer != null ? x.Performer.FullName : "System/Auto",
                    ReferenceInvoiceID = x.ReferenceInvoiceID,
                    ReferenceInvoiceNumber = x.ReferenceInvoice != null
                                             ? x.ReferenceInvoice.InvoiceNumber.ToString()
                                             : null
                })
                .ToListAsync(token);

            return Result.Ok(list);
        }
    }
}
