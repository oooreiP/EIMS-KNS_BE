using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Customer;
using EIMS.Application.DTOs.Invoices; // Đảm bảo namespace chứa UnpaidInvoiceItemDto
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.Customers.Queries
{
    public class GetCustomerDebtDetailQueryHandler : IRequestHandler<GetCustomerDebtDetailQuery, Result<CustomerDebtDetailDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetCustomerDebtDetailQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<CustomerDebtDetailDto>> Handle(GetCustomerDebtDetailQuery request, CancellationToken cancellationToken)
        {
            // 1. Validation
            if (request.InvoicePageSize > 100 || request.PaymentPageSize > 100)
                return Result.Fail("PageSize cannot exceed 100.");

            var customerEntity = await _uow.CustomerRepository.GetByIdAsync(request.CustomerId);
            if (customerEntity == null)
                return Result.Fail($"Customer with ID {request.CustomerId} not found");

            // 2. Map Customer Info
            var customerInfo = _mapper.Map<CustomerInfoDto>(customerEntity);

            // Date UTC 
            DateTime? fromDateUtc = request.FromDate.HasValue
                 ? DateTime.SpecifyKind(request.FromDate.Value, DateTimeKind.Utc)
                 : null;
            DateTime? toDateUtc = request.ToDate.HasValue
                ? DateTime.SpecifyKind(request.ToDate.Value, DateTimeKind.Utc)
                : null;

            // Invoice status valid (đã phát hành, đã ký...)
            var validDebtStatuses = new[] { 2, 8, 9, 12, 15 };


            // base query 
            var baseInvoiceQuery = _uow.InvoicesRepository.GetAllQueryable()
                .Where(i => i.CustomerID == request.CustomerId && validDebtStatuses.Contains(i.InvoiceStatusID));

            // 4. Calculate Summary(sum on db)

            var summaryData = await baseInvoiceQuery
                .Select(i => new
                {
                    i.TotalAmount,
                    // calculate paid amount
                    PaidAmount = i.Payments.Sum(p => p.AmountPaid),
                    i.PaymentDueDate,
                    // check overdue
                    IsOverdue = (i.PaymentDueDate ?? i.CreatedAt.AddDays(30)) < DateTime.UtcNow
                })
                .ToListAsync(cancellationToken);


            var summary = new CustomerDebtSummaryDto
            {
                // total debt
                TotalDebt = summaryData.Sum(x => x.TotalAmount - x.PaidAmount),

                // Total paid
                TotalPaid = summaryData.Sum(x => x.PaidAmount),

                // overdue debt (overdue and dbt >0)
                OverdueDebt = summaryData
                    .Where(x => x.IsOverdue && (x.TotalAmount - x.PaidAmount) > 0)
                    .Sum(x => x.TotalAmount - x.PaidAmount)
            };

            // 5. UNPAID INVOICES LIST 
            var unpaidQuery = baseInvoiceQuery
                .Where(i => i.TotalAmount > i.Payments.Sum(p => p.AmountPaid));

            // Filter
            if (fromDateUtc.HasValue) unpaidQuery = unpaidQuery.Where(i => i.IssuedDate >= fromDateUtc.Value);
            if (toDateUtc.HasValue) unpaidQuery = unpaidQuery.Where(i => i.IssuedDate <= toDateUtc.Value);
            if (!string.IsNullOrEmpty(request.SearchInvoiceNumber))
                unpaidQuery = unpaidQuery.Where(i => i.InvoiceNumber.ToString().Contains(request.SearchInvoiceNumber));
            bool isDesc = (request.SortOrder?.ToLower() != "asc");

            // sort date default
            unpaidQuery = isDesc
                ? unpaidQuery.OrderByDescending(i => i.IssuedDate ?? i.CreatedAt)
                : unpaidQuery.OrderBy(i => i.IssuedDate ?? i.CreatedAt);

            if (!string.IsNullOrEmpty(request.SortBy))
            {
                switch (request.SortBy.ToLower())
                {
                    case "amount":
                        unpaidQuery = isDesc
                            ? unpaidQuery.OrderByDescending(i => i.TotalAmount)
                            : unpaidQuery.OrderBy(i => i.TotalAmount);
                        break;
                    case "duedate":
                        unpaidQuery = isDesc
                            ? unpaidQuery.OrderByDescending(i => i.PaymentDueDate)
                            : unpaidQuery.OrderBy(i => i.PaymentDueDate);
                        break;
                }
            }
            int totalUnpaid = await unpaidQuery.CountAsync(cancellationToken);

            // Query lấy dữ liệu trang
            var unpaidItems = await unpaidQuery
                .OrderByDescending(i => i.IssuedDate) // Mới nhất lên đầu
                .Skip((request.InvoicePageIndex - 1) * request.InvoicePageSize)
                .Take(request.InvoicePageSize)
                .Select(i => new UnpaidInvoiceItemDto
                {
                    InvoiceId = i.InvoiceID,
                    InvoiceNumber = i.InvoiceNumber.ToString(),
                    InvoiceDate = i.SignDate ?? i.IssuedDate ?? i.CreatedAt,
                    DueDate = i.PaymentDueDate,
                    TotalAmount = i.TotalAmount,

                    //  Select calculate
                    PaidAmount = i.Payments.Sum(p => p.AmountPaid),
                    RemainingAmount = i.TotalAmount - i.Payments.Sum(p => p.AmountPaid),

                    PaymentStatus = (i.Payments.Sum(p => p.AmountPaid) > 0) ? "Partially Paid" : "Unpaid",
                    Description = i.Notes ?? "", // Map field Note

                    // Logic Overdue: (Hạn < Hiện tại)
                    IsOverdue = (i.PaymentDueDate ?? i.CreatedAt.AddDays(30)) < DateTime.UtcNow
                })
                .ToListAsync(cancellationToken);

            // 6. PAYMENT HISTORY
            var paymentQuery = _uow.InvoicePaymentRepository.GetAllQueryable()
                .Include(p => p.Invoice)
                .Where(p => p.Invoice.CustomerID == request.CustomerId);

            if (fromDateUtc.HasValue) paymentQuery = paymentQuery.Where(p => p.PaymentDate >= fromDateUtc.Value);
            if (toDateUtc.HasValue) paymentQuery = paymentQuery.Where(p => p.PaymentDate <= toDateUtc.Value);

            int totalPayments = await paymentQuery.CountAsync(cancellationToken);
            var rawPayments = await paymentQuery
                .OrderByDescending(p => p.PaymentDate)
                .Skip((request.PaymentPageIndex - 1) * request.PaymentPageSize)
                .Take(request.PaymentPageSize)
                .Select(p => new
                {
                    p.PaymentID, // Chú ý: Entity thường tên là InvoicePaymentID hoặc PaymentID
                    p.PaymentDate,
                    p.AmountPaid,
                    p.PaymentMethod,
                    p.TransactionCode,
                    p.InvoiceID,
                    InvoiceNumber = p.Invoice.InvoiceNumber,
                    p.Note, // ✅ Field Note
                    p.CreatedBy // ✅ Field UserId (người tạo)
                })
                .ToListAsync(cancellationToken);
            var userIds = rawPayments.Where(p => p.CreatedBy.HasValue)
                                 .Select(p => p.CreatedBy.Value)
                                 .Distinct()
                                 .ToList();

            var userDict = new Dictionary<int, string>();
            if (userIds.Any())
            {
                userDict = await _uow.UserRepository.GetAllQueryable()
                    .Where(u => userIds.Contains(u.UserID))
                    .ToDictionaryAsync(u => u.UserID, u => u.FullName ?? u.FullName, cancellationToken);
            }
            var paymentItems = await paymentQuery
                .OrderByDescending(p => p.PaymentDate)
                .Skip((request.PaymentPageIndex - 1) * request.PaymentPageSize)
                .Take(request.PaymentPageSize)
                .Select(p => new PaymentHistoryItemDto
                {
                    PaymentId = p.PaymentID,
                    PaymentDate = p.PaymentDate,
                    AmountPaid = p.AmountPaid,
                    PaymentMethod = p.PaymentMethod ?? "Unknown", // Handle null
                    TransactionCode = p.TransactionCode ?? "",
                    InvoiceNumber = p.Invoice.InvoiceNumber.ToString(), // Để biết trả cho hóa đơn nào
                    InvoiceId = p.InvoiceID,
                    Note = p.Note ?? "",
                    UserId = p.CreatedBy,
                    UserName = (p.CreatedBy.HasValue && userDict.ContainsKey(p.CreatedBy.Value))
                           ? userDict[p.CreatedBy.Value]
                           : "System"
                })
                .ToListAsync(cancellationToken);

            // 7. Construct Final Response
            var response = new CustomerDebtDetailDto
            {
                Customer = customerInfo,
                Summary = summary,
                UnpaidInvoices = new PaginatedResult<UnpaidInvoiceItemDto>(unpaidItems, totalUnpaid, request.InvoicePageIndex, request.InvoicePageSize),
                PaymentHistory = new PaginatedResult<PaymentHistoryItemDto>(paymentItems, totalPayments, request.PaymentPageIndex, request.PaymentPageSize)
            };

            return Result.Ok(response);
        }
    }
}