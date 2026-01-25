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
            // 1. Base query
            var query = _uow.InvoiceStatementRepository.GetAllQueryable()
                .Include(s => s.Customer)
                .Include(s => s.StatementStatus)
                .AsNoTracking();

            // 2. Apply Filters
            if (request.CustomerID.HasValue)
            {
                query = query.Where(s => s.CustomerID == request.CustomerID.Value);
            }
            if (request.SalesId.HasValue)
            {
                query = query.Where(s => s.Customer != null && s.Customer.SaleID == request.SalesId.Value);
            }

            if (!string.IsNullOrEmpty(request.StatementCode))
            {
                query = query.Where(s => s.StatementCode.Contains(request.StatementCode));
            }

            // Lọc theo Ngày tạo bảng kê
            if (request.FromDate.HasValue)
            {
                query = query.Where(s => s.StatementDate >= request.FromDate.Value);
            }

            if (request.ToDate.HasValue)
            {
                var endDate = request.ToDate.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(s => s.StatementDate <= endDate);
            }
            if (request.PeriodMonth.HasValue)
                query = query.Where(s => s.PeriodMonth == request.PeriodMonth.Value);
            if (request.PeriodYear.HasValue)
                query = query.Where(s => s.PeriodYear == request.PeriodYear.Value);
            if (request.StatusID.HasValue)
            {
                query = query.Where(s => s.StatusID == request.StatusID.Value);
            }

            // 3. Ordering (Mới nhất lên đầu)
            query = query.OrderByDescending(s => s.StatementDate);
            var projectedQuery = query.Select(s => new StatementSummaryResponse
            {
                StatementID = s.StatementID,
                StatementCode = s.StatementCode ?? "",
                CustomerName = s.Customer != null ? s.Customer.CustomerName : "Unknown",
                StatementDate = s.StatementDate,
                Period = $"{s.PeriodMonth:D2}/{s.PeriodYear}", 

                OpeningBalance = s.OpeningBalance, 
                NewCharges = s.NewCharges,         
                PaidAmount = s.PaidAmount,         
                TotalAmount = s.TotalAmount,
                // -------------------------------

                TotalInvoices = s.TotalInvoices ?? 0,
                Status = s.StatementStatus != null ? s.StatementStatus.StatusName : "Unknown",
                StatusID = s.StatusID, 
                IsOverdue = (s.StatusID != 5) && (DateTime.UtcNow > s.DueDate)
            });
            var paginatedResult = await PaginatedList<StatementSummaryResponse>.CreateAsync(
                projectedQuery,
                request.PageNumber,
                request.PageSize
            );

            return Result.Ok(paginatedResult);
        }
    }
}