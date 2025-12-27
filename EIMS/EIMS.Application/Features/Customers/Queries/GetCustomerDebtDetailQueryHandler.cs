using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Customer;
using EIMS.Application.DTOs.InvoicePayment;
using EIMS.Application.DTOs.Invoices;
using EIMS.Application.DTOs.InvoiceStatement;
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
            DateTime? fromDateUtc = request.FromDate.HasValue
                 ? DateTime.SpecifyKind(request.FromDate.Value, DateTimeKind.Utc)
                 : null;
            DateTime? toDateUtc = request.ToDate.HasValue
                ? DateTime.SpecifyKind(request.ToDate.Value, DateTimeKind.Utc)
                : null;

          var validDebtStatuses = new[] 
            { 
                2,  // Issued
                8,  // Signed
                9,  // Sent
                12, // TaxAuthority Approved
                15  // Send Error (Still a valid signed invoice)
            };

            // --- 4. PREPARE QUERIES ---
            
            // Base Invoice Query: Filter by Customer AND Valid Statuses
            var baseInvoiceQuery = _uow.InvoicesRepository.GetAllQueryable()
                .Where(i => i.CustomerID == request.CustomerId && validDebtStatuses.Contains(i.InvoiceStatusID));

            // Base Payment Query
            var basePaymentQuery = _uow.InvoicePaymentRepository.GetAllQueryable()
                .Where(p => p.Invoice.CustomerID == request.CustomerId);

            // 4. Calculate Summary (DebtSummaryDto)
            // We calculate this based on ALL data, not just the filtered/paged data
            var summary = new DebtSummaryDto
            {
                InvoiceCount = await baseInvoiceQuery.CountAsync(cancellationToken),

                UnpaidInvoiceCount = await baseInvoiceQuery
                    .CountAsync(i => i.PaymentStatusID != 3, cancellationToken), // Assuming 3 = Paid

                TotalDebt = await baseInvoiceQuery
                    .SumAsync(i => i.RemainingAmount, cancellationToken),

                TotalPaid = await baseInvoiceQuery
                    .SumAsync(i => i.PaidAmount, cancellationToken),

                OverdueDebt = await baseInvoiceQuery
                    .Where(i => i.PaymentDueDate < DateTime.UtcNow && i.RemainingAmount > 0)
                    .SumAsync(i => i.RemainingAmount, cancellationToken),

                LastPaymentDate = await basePaymentQuery
                    .OrderByDescending(p => p.PaymentDate)
                    .Select(p => (DateTime?)p.PaymentDate)
                    .FirstOrDefaultAsync(cancellationToken)
            };

            // 5. Filter & Paging for INVOICES List
            var filteredInvoiceQuery = baseInvoiceQuery;
            if (fromDateUtc.HasValue)
                filteredInvoiceQuery = filteredInvoiceQuery.Where(i => i.IssuedDate >= fromDateUtc.Value);
            if (toDateUtc.HasValue)
                filteredInvoiceQuery = filteredInvoiceQuery.Where(i => i.IssuedDate <= toDateUtc.Value);
            if (!string.IsNullOrEmpty(request.SearchInvoiceNumber))
                filteredInvoiceQuery = filteredInvoiceQuery.Where(i => i.InvoiceNumber.ToString().Contains(request.SearchInvoiceNumber));

            var totalInvoices = await filteredInvoiceQuery.CountAsync(cancellationToken);
            var invoiceItems = await filteredInvoiceQuery
                .OrderByDescending(i => i.IssuedDate)
                .Skip((request.InvoicePageIndex - 1) * request.InvoicePageSize)
                .Take(request.InvoicePageSize)
                .ProjectTo<StatementInvoiceDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            // 6. Filter & Paging for PAYMENTS List
            var filteredPaymentQuery = basePaymentQuery;
            if (request.FromDate.HasValue)
                filteredPaymentQuery = filteredPaymentQuery.Where(p => p.PaymentDate >= request.FromDate.Value);
            if (request.ToDate.HasValue)
                filteredPaymentQuery = filteredPaymentQuery.Where(p => p.PaymentDate <= request.ToDate.Value);

            var totalPayments = await filteredPaymentQuery.CountAsync(cancellationToken);
            var paymentItems = await filteredPaymentQuery
                .OrderByDescending(p => p.PaymentDate)
                .Skip((request.PaymentPageIndex - 1) * request.PaymentPageSize)
                .Take(request.PaymentPageSize)
                .ProjectTo<PaymentHistoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            // 7. Construct Final Response
            var response = new CustomerDebtDetailDto
            {
                Customer = customerInfo,
                Summary = summary,
                Invoices = new PaginatedResult<StatementInvoiceDto>(invoiceItems, totalInvoices, request.InvoicePageIndex, request.InvoicePageSize),
                Payments = new PaginatedResult<PaymentHistoryDto>(paymentItems, totalPayments, request.PaymentPageIndex, request.PaymentPageSize)
            };

            return Result.Ok(response);
        }
    }
}