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
            // Đảm bảo CustomerInfoDto khớp với JSON yêu cầu
            var customerInfo = _mapper.Map<CustomerInfoDto>(customerEntity); 
            
            // Xử lý Date UTC an toàn
            DateTime? fromDateUtc = request.FromDate.HasValue
                 ? DateTime.SpecifyKind(request.FromDate.Value, DateTimeKind.Utc)
                 : null;
            DateTime? toDateUtc = request.ToDate.HasValue
                ? DateTime.SpecifyKind(request.ToDate.Value, DateTimeKind.Utc)
                : null;

            // Danh sách trạng thái hợp lệ (đã phát hành, đã ký...)
            var validDebtStatuses = new[] { 2, 8, 9, 12, 15 };

            // --- 3. PREPARE QUERIES ---
            
            // Query cơ sở: Lấy hóa đơn của khách, trạng thái hợp lệ
            var baseInvoiceQuery = _uow.InvoicesRepository.GetAllQueryable()
                .Where(i => i.CustomerID == request.CustomerId && validDebtStatuses.Contains(i.InvoiceStatusID));

            // 4. Calculate Summary (Dùng SUM trực tiếp trên DB để tối ưu)
            // LƯU Ý: Không dùng i.RemainingAmount nếu nó không phải cột trong DB. Phải tính toán trực tiếp.
            
            var summaryData = await baseInvoiceQuery
                .Select(i => new 
                {
                    i.TotalAmount,
                    // Tính tổng đã trả cho từng hóa đơn
                    PaidAmount = i.Payments.Sum(p => p.AmountPaid),
                    i.PaymentDueDate,
                    // Kiểm tra quá hạn ngay tại đây để dùng cho count
                    IsOverdue = (i.PaymentDueDate ?? i.CreatedAt.AddDays(30)) < DateTime.UtcNow
                })
                .ToListAsync(cancellationToken);

            // Tính toán trên RAM (vì data summary chỉ gồm các con số, rất nhẹ)
            var summary = new CustomerDebtSummaryDto
            {
                // Tổng số tiền hóa đơn
                TotalDebt = summaryData.Sum(x => x.TotalAmount - x.PaidAmount),
                
                // Tổng tiền đã thanh toán
                TotalPaid = summaryData.Sum(x => x.PaidAmount),
                
                // Nợ quá hạn (Chỉ tính những khoản còn dư nợ > 0 và đã hết hạn)
                OverdueDebt = summaryData
                    .Where(x => x.IsOverdue && (x.TotalAmount - x.PaidAmount) > 0)
                    .Sum(x => x.TotalAmount - x.PaidAmount)
            };

            // 5. UNPAID INVOICES LIST (Danh sách hóa đơn chưa thanh toán)
            // Lọc: Chỉ lấy hóa đơn còn nợ (Total > Paid)
            var unpaidQuery = baseInvoiceQuery
                .Where(i => i.TotalAmount > i.Payments.Sum(p => p.AmountPaid));

            // Filter ngày tháng (nếu có)
            if (fromDateUtc.HasValue) unpaidQuery = unpaidQuery.Where(i => i.IssuedDate >= fromDateUtc.Value);
            if (toDateUtc.HasValue) unpaidQuery = unpaidQuery.Where(i => i.IssuedDate <= toDateUtc.Value);
            if (!string.IsNullOrEmpty(request.SearchInvoiceNumber))
                unpaidQuery = unpaidQuery.Where(i => i.InvoiceNumber.ToString().Contains(request.SearchInvoiceNumber));

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
                    InvoiceDate = i.SignDate ?? i.IssuedDate ?? i.CreatedAt, // Ưu tiên ngày ký
                    DueDate = i.PaymentDueDate,
                    TotalAmount = i.TotalAmount,
                    
                    // Tính toán trực tiếp trong Select
                    PaidAmount = i.Payments.Sum(p => p.AmountPaid),
                    RemainingAmount = i.TotalAmount - i.Payments.Sum(p => p.AmountPaid),
                    
                    PaymentStatus = (i.Payments.Sum(p => p.AmountPaid) > 0) ? "Partially Paid" : "Unpaid",
                    Description = i.Notes ?? "", // Map field Note
                    
                    // Logic Overdue: (Hạn < Hiện tại)
                    IsOverdue = (i.PaymentDueDate ?? i.CreatedAt.AddDays(30)) < DateTime.UtcNow
                })
                .ToListAsync(cancellationToken);

            // 6. PAYMENT HISTORY (Lịch sử thanh toán)
            var paymentQuery = _uow.InvoicePaymentRepository.GetAllQueryable()
                .Include(p => p.Invoice)
                .Where(p => p.Invoice.CustomerID == request.CustomerId);

            if (fromDateUtc.HasValue) paymentQuery = paymentQuery.Where(p => p.PaymentDate >= fromDateUtc.Value);
            if (toDateUtc.HasValue) paymentQuery = paymentQuery.Where(p => p.PaymentDate <= toDateUtc.Value);

            int totalPayments = await paymentQuery.CountAsync(cancellationToken);

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
                    InvoiceNumber = p.Invoice.InvoiceNumber.ToString() // Để biết trả cho hóa đơn nào
                })
                .ToListAsync(cancellationToken);

            // 7. Construct Final Response
            var response = new CustomerDebtDetailDto
            {
                Customer = customerInfo,
                Summary = summary,
                // Đổi tên biến Invoices -> UnpaidInvoices theo yêu cầu
                UnpaidInvoices = new PaginatedResult<UnpaidInvoiceItemDto>(unpaidItems, totalUnpaid, request.InvoicePageIndex, request.InvoicePageSize),
                // Đổi tên biến Payments -> PaymentHistory theo yêu cầu
                PaymentHistory = new PaginatedResult<PaymentHistoryItemDto>(paymentItems, totalPayments, request.PaymentPageIndex, request.PaymentPageSize)
            };

            return Result.Ok(response);
        }
    }
}