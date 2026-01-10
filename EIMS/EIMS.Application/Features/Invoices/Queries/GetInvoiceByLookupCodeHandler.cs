using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Invoices;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace EIMS.Application.Features.Invoices.Queries
{
    public class GetInvoiceByLookupCodeHandler : IRequestHandler<GetInvoiceByLookupCodeQuery, Result<InvoicePublicLookupResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache; // Dùng để Rate Limit đơn giản
        private readonly IFileStorageService _fileStorageService; // Service lấy link PDF
        private readonly ILogger<GetInvoiceByLookupCodeHandler> _logger; // Inject Logger

        public GetInvoiceByLookupCodeHandler(IUnitOfWork unitOfWork, IMemoryCache cache, ILogger<GetInvoiceByLookupCodeHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
            _logger = logger;
        }
        public async Task<Result<InvoicePublicLookupResponse>> Handle(GetInvoiceByLookupCodeQuery request, CancellationToken cancellationToken)
        {
            // 1. Rate Limiting (Chặn spam từ 1 IP: tối đa 10 lần/phút)
            string cacheKey = $"Lookup_RateLimit_{request.IPAddress}";
            if (_cache.TryGetValue(cacheKey, out int count))
            {
                if (count >= 10)
                {
                    // Ghi log cảnh báo spam (tùy chọn)
                    return Result.Fail(new Error("Quá nhiều yêu cầu. Vui lòng thử lại sau 1 phút.").WithMetadata("ErrorCode", "429"));
                }
                _cache.Set(cacheKey, count + 1, TimeSpan.FromMinutes(1));
            }
            else
            {
                _cache.Set(cacheKey, 1, TimeSpan.FromMinutes(1));
            }

            // 2. Tìm hóa đơn trong DB
            // Điều kiện: LookupCode khớp VÀ Status = 2 (Đã phát hành) hoặc 16 (Đã ký - tùy nghiệp vụ)
            var invoice = await _unitOfWork.InvoicesRepository.GetAllQueryable()
     .Include(x => x.Company)
     .Include(x => x.Template)
         .ThenInclude(t => t.Serial)
             .ThenInclude(s => s.SerialStatus) 
     .Include(x => x.Template)
         .ThenInclude(t => t.Serial)
             .ThenInclude(s => s.InvoiceType)  
     .AsNoTracking()
     .FirstOrDefaultAsync(x => x.LookupCode == request.LookupCode
                            && (x.InvoiceStatusID == 2 || x.InvoiceStatusID == 16), cancellationToken);

            // 3. Ghi Log (Audit)
            try
            {
                var log = new InvoiceLookupLog
                {
                    LookupCode = request.LookupCode,
                    IPAddress = request.IPAddress,
                    UserAgent = request.UserAgent,
                    Time = DateTime.UtcNow,
                    IsSuccess = (invoice != null), // True nếu tìm thấy
                    FoundInvoiceID = invoice?.InvoiceID
                };

                // Thêm vào DB
                await _unitOfWork.InvoiceLookupLogRepository.CreateAsync(log);

                // Lưu log lại.
                // Vì invoice lấy ở trên dùng .AsNoTracking(), nên SaveChanges ở đây 
                // chỉ tác động lên bảng Log, an toàn hơn.
                await _unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                // Chương trình vẫn tiếp tục chạy xuống dưới để trả kết quả cho khách.
                _logger.LogError(ex, "Lỗi khi lưu lịch sử tra cứu hóa đơn: {Code}", request.LookupCode);
            }
            // 4. Xử lý kết quả
            if (invoice == null)
            {
                return Result.Fail(new Error("Không tìm thấy hóa đơn hoặc mã tra cứu không hợp lệ."));
            }

            // 5. Tạo Signed URL cho PDF (Link chỉ sống 1 giờ)
            // Giả sử FileStorageService có hàm GetPresignedUrl
            string signedPdfUrl = "";
            if (!string.IsNullOrEmpty(invoice.FilePath)) // Hoặc XMLPath tùy file bạn muốn trả
            {
                // signedPdfUrl = await _fileStorageService.GetPresignedUrlAsync(invoice.PdfPath, expiration: TimeSpan.FromHours(1));
                signedPdfUrl = invoice.FilePath; // Tạm thời trả path gốc nếu chưa có Signed URL
            }
            var serial = $"{invoice.Template?.Serial.PrefixID}{invoice.Template?.Serial.SerialStatus.Symbol}{invoice.Template?.Serial.Year}{invoice.Template?.Serial.InvoiceType.Symbol}{invoice.Template?.Serial.Tail}";
            // 6. Map sang DTO
            var response = new InvoicePublicLookupResponse
            {
                InvoiceNumber = (invoice.InvoiceNumber ?? 0).ToString("D7"), // Format 0000123
                SerialNumber = serial,
                IssueDate = invoice.IssuedDate ?? DateTime.MinValue,
                SellerName = invoice.Company?.CompanyName,
                BuyerName = invoice.InvoiceCustomerName,
                TotalAmount = invoice.TotalAmount,
                Status = "Đã phát hành",
                PdfUrl = signedPdfUrl
            };

            return Result.Ok(response);
        }
    }
}