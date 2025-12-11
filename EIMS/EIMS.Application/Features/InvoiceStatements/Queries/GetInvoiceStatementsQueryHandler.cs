using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.InvoiceStatement;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.InvoiceStatements.Queries
{
   public class GetInvoiceStatementsQueryHandler : IRequestHandler<GetInvoiceStatementsQuery, Result<PaginatedList<StatementSummaryResponse>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetInvoiceStatementsQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedList<StatementSummaryResponse>>> Handle(GetInvoiceStatementsQuery request, CancellationToken cancellationToken)
        {
            // 1.base query including necessary relations for display
            var query = _uow.InvoiceStatementRepository.GetAllQueryable()
                .Include(s => s.Customer)
                .Include(s => s.StatementStatus)
                .AsNoTracking();

            // 2. Apply Filters
            if (request.CustomerID.HasValue)
            {
                query = query.Where(s => s.CustomerID == request.CustomerID.Value);
            }

            if (!string.IsNullOrEmpty(request.StatementCode))
            {
                query = query.Where(s => s.StatementCode.Contains(request.StatementCode));
            }

            if (request.FromDate.HasValue)
            {
                // Reset time to start of day if needed, or rely on client passing correct time
                query = query.Where(s => s.StatementDate >= request.FromDate.Value);
            }

            if (request.ToDate.HasValue)
            {
                // Ensure we include the end date fully
                var endDate = request.ToDate.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(s => s.StatementDate <= endDate);
            }

            if (request.StatusID.HasValue)
            {
                query = query.Where(s => s.StatusID == request.StatusID.Value);
            }

            // 3. Ordering (Newest first)
            query = query.OrderByDescending(s => s.StatementDate);

            // 4. Projection to DTO
            // You can use AutoMapper here if you have a map configured: CreateMap<InvoiceStatement, StatementSummaryResponse>()
            var projectedQuery = query.Select(s => new StatementSummaryResponse
            {
                StatementID = s.StatementID,
                StatementCode = s.StatementCode,
                CustomerName = s.Customer != null ? s.Customer.CustomerName : "Unknown",
                StatementDate = s.StatementDate ?? DateTime.MinValue,
                TotalAmount = s.TotalAmount,
                TotalInvoices = s.TotalInvoices ?? 0,
                Status = s.StatementStatus != null ? s.StatementStatus.StatusName : "Unknown"
            });

            // 5. Pagination
            var paginatedResult = await PaginatedList<StatementSummaryResponse>.CreateAsync(
                projectedQuery, 
                request.PageNumber, 
                request.PageSize
            );

            return Result.Ok(paginatedResult);
        }
    }
}