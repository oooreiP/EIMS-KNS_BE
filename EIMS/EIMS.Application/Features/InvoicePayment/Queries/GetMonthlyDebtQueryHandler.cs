using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs;
using EIMS.Application.DTOs.Customer;
using EIMS.Application.DTOs.Mails;
using EIMS.Application.DTOs.Payments;
using EIMS.Application.Features.InvoiceStatements.Queries;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoicePayment.Queries
{
    public class GetMonthlyDebtQueryHandler : IRequestHandler<GetMonthlyDebtQuery, Result<MonthlyDebtResult>>
    {
        private readonly IUnitOfWork _uow;

        public GetMonthlyDebtQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<MonthlyDebtResult>> Handle(GetMonthlyDebtQuery request, CancellationToken cancellationToken)
        {

            var query = _uow.InvoicesRepository.GetAllQueryable();
            query = query.Where(x => x.IssuedDate.Value.Month == request.Month &&
                                     x.IssuedDate.Value.Year == request.Year);
            if (request.CustomerId.HasValue)
            {
                query = query.Where(x => x.CustomerID == request.CustomerId.Value);
            }
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                string term = request.SearchTerm.ToLower();
                query = query.Where(x => x.Customer.CustomerName.ToLower().Contains(term));
            }
            var now = DateTime.UtcNow;
            var summaryDto = await query
                .GroupBy(x => 1) 
                .Select(g => new DebtSummaryDTO
                {
                    TotalReceivable = g.Sum(x => x.TotalAmount),
                    TotalPaid = g.Sum(x => x.Payments.Sum(p => p.AmountPaid)),
                    TotalRemaining = g.Sum(x => x.TotalAmount - x.Payments.Sum(p => p.AmountPaid)),
                    TotalOverdue = g.Sum(x => (x.PaymentDueDate < now && (x.TotalAmount - x.Payments.Sum(p => p.AmountPaid)) > 0)
                                              ? (x.TotalAmount - x.Payments.Sum(p => p.AmountPaid))
                                              : 0)
                })
                .FirstOrDefaultAsync(cancellationToken);
            if (summaryDto == null)
            {
                summaryDto = new DebtSummaryDTO(); 
            }
            var projection = query.Select(x => new MonthlyDebtDTO
            {
                InvoiceId = x.InvoiceID,
                InvoiceDate = x.IssuedDate ?? DateTime.UtcNow,
                DueDate = x.PaymentDueDate,
                CustomerName = x.Customer.CustomerName,
                TotalAmount = x.TotalAmount,
                PaidAmount = x.Payments.Sum(p => p.AmountPaid),
                RemainingAmount = x.TotalAmount - x.Payments.Sum(p => p.AmountPaid),
                OverdueAmount = (x.PaymentDueDate < now && (x.TotalAmount - x.Payments.Sum(p => p.AmountPaid)) > 0)
                                ? (x.TotalAmount - x.Payments.Sum(p => p.AmountPaid))
                                : 0,
                Status = (x.TotalAmount - x.Payments.Sum(p => p.AmountPaid)) <= 0 ? "Đã thanh toán" :
                         (x.PaymentDueDate < now ? "Quá hạn" : "Chờ thanh toán")
            });
            projection = projection.OrderByDescending(x => x.InvoiceDate);
            var paginatedList = await PaginatedList<MonthlyDebtDTO>.CreateAsync(
                projection,
                request.PageIndex,
                request.PageSize
            );

            return Result.Ok(new MonthlyDebtResult
            {
                Summary = summaryDto,
                Invoices = paginatedList
            });
        }
    }
}
