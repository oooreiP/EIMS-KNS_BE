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
            customerInfo.Email = customerEntity.ContactEmail;
            customerInfo.Phone = customerEntity.ContactPhone;

            // Date UTC 
            DateTime? fromDateUtc = request.FromDate.HasValue
                 ? DateTime.SpecifyKind(request.FromDate.Value, DateTimeKind.Utc)
                 : null;
            DateTime? toDateUtc = request.ToDate.HasValue
                ? DateTime.SpecifyKind(request.ToDate.Value, DateTimeKind.Utc)
                : null;
            var validDebtStatus = 2;
            // Invoice status valid (đã phát hành, đã ký...)
            var validDebtStatuses = new[] { 2, 8, 9, 12, 15 };


            // base query 
            var baseInvoiceQuery = _uow.InvoicesRepository.GetAllQueryable()
                  .Include(i => i.InvoiceStatus)
                  .Where(i => i.CustomerID == request.CustomerId && i.InvoiceStatusID == validDebtStatus);
            // 4. Calculate Summary(sum on db)

            var summaryData = await baseInvoiceQuery
                .Select(i => new
                {
                    i.TotalAmount,
                    // calculate paid amount
                    PaidAmount = i.Payments.Sum(p => p.AmountPaid),
                    i.PaymentDueDate,
                    // check overdue
                    IsOverdue = (i.PaymentDueDate ?? i.CreatedAt.AddDays(30)) < DateTime.UtcNow,
                    MaxPaymentDate = i.Payments.Any() ? i.Payments.Max(p => p.PaymentDate) : (DateTime?)null
                })
                .ToListAsync(cancellationToken);

            var summary = new CustomerDebtSummaryDto
            {
                CustomerId = customerEntity.CustomerID,
                CustomerName = customerEntity.CustomerName,
                TaxCode = customerEntity.TaxCode,
                Email = customerEntity.ContactEmail,
                Phone = customerEntity.ContactPhone,
                Address = customerEntity.Address,
                // total debt
                TotalDebt = summaryData.Sum(x => x.TotalAmount - x.PaidAmount),

                // Total paid
                TotalPaid = summaryData.Sum(x => x.PaidAmount),

                // overdue debt (overdue and dbt >0)
                OverdueDebt = summaryData
                    .Where(x => x.IsOverdue && (x.TotalAmount - x.PaidAmount) > 0)
                    .Sum(x => x.TotalAmount - x.PaidAmount),
                InvoiceCount = summaryData.Count,
                UnpaidInvoiceCount = summaryData.Count(x => x.TotalAmount > x.PaidAmount),

                LastPaymentDate = summaryData.Any()
                    ? summaryData.Max(x => x.MaxPaymentDate)
                    : null
            };

            // 5. UNPAID INVOICES LIST 
            var invoiceQuery = baseInvoiceQuery;

            // Filter
            if (fromDateUtc.HasValue) invoiceQuery = invoiceQuery.Where(i => i.IssuedDate >= fromDateUtc.Value);
            if (toDateUtc.HasValue) invoiceQuery = invoiceQuery.Where(i => i.IssuedDate <= toDateUtc.Value);
            if (!string.IsNullOrEmpty(request.SearchInvoiceNumber))
                invoiceQuery = invoiceQuery.Where(i => i.InvoiceNumber.ToString().Contains(request.SearchInvoiceNumber));
            bool isDesc = (request.SortOrder?.ToLower() != "asc");

            // sort date default
            invoiceQuery = isDesc
                ? invoiceQuery.OrderByDescending(i => i.IssuedDate ?? i.CreatedAt)
                : invoiceQuery.OrderBy(i => i.IssuedDate ?? i.CreatedAt);

            if (!string.IsNullOrEmpty(request.SortBy))
            {
                switch (request.SortBy.ToLower())
                {
                    case "amount":
                        invoiceQuery = isDesc
                            ? invoiceQuery.OrderByDescending(i => i.TotalAmount)
                            : invoiceQuery.OrderBy(i => i.TotalAmount);
                        break;
                    case "duedate":
                        invoiceQuery = isDesc
                            ? invoiceQuery.OrderByDescending(i => i.PaymentDueDate)
                            : invoiceQuery.OrderBy(i => i.PaymentDueDate);
                        break;
                }
            }
            int totalUnpaid = await invoiceQuery.CountAsync(cancellationToken);

            // Query lấy dữ liệu trang
            var unpaidItems = await invoiceQuery
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

                    PaymentStatus = (i.Payments.Sum(p => p.AmountPaid) >= i.TotalAmount) ? "Paid" :
                                    (i.Payments.Sum(p => p.AmountPaid) > 0) ? "Partially Paid" :
                                    "Unpaid",
                    Description = i.Notes ?? "", 

                    // Logic Overdue: (Hạn < Hiện tại)
                    IsOverdue = (i.PaymentDueDate ?? i.CreatedAt.AddDays(30)) < DateTime.UtcNow,
                    InvoiceStatusID = i.InvoiceStatusID,
                    InvoiceStatus = i.InvoiceStatus.StatusName ?? "Issued"
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
                    p.PaymentID,
                    p.PaymentDate,
                    p.AmountPaid,
                    p.PaymentMethod,
                    p.TransactionCode,
                    p.InvoiceID,
                    InvoiceNumber = p.Invoice.InvoiceNumber,
                    p.Note,
                    p.CreatedBy
                })
                .ToListAsync(cancellationToken);

            // Get Users Dictionary
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

            // Map In-Memory
            var paymentItems = rawPayments.Select(p => new PaymentHistoryItemDto
            {
                PaymentId = p.PaymentID,
                PaymentDate = p.PaymentDate,
                AmountPaid = p.AmountPaid,
                PaymentMethod = p.PaymentMethod ?? "Unknown",
                TransactionCode = p.TransactionCode ?? "",
                InvoiceNumber = p.InvoiceNumber.ToString(),
                InvoiceId = p.InvoiceID,
                Note = p.Note ?? "",
                UserId = p.CreatedBy,
                UserName = (p.CreatedBy.HasValue && userDict.ContainsKey(p.CreatedBy.Value))
                           ? userDict[p.CreatedBy.Value]
                           : "System"
            }).ToList();

            // 7. Construct Final Response
            var response = new CustomerDebtDetailDto
            {
                Customer = customerInfo,
                Summary = summary,
                Invoices = new PaginatedResult<UnpaidInvoiceItemDto>(unpaidItems, totalUnpaid, request.InvoicePageIndex, request.InvoicePageSize),
                PaymentHistory = new PaginatedResult<PaymentHistoryItemDto>(paymentItems, totalPayments, request.PaymentPageIndex, request.PaymentPageSize)
            };

            return Result.Ok(response);
        }
    }
}