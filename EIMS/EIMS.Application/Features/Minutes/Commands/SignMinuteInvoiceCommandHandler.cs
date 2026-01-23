using EIMS.Application.Commons.Helpers;
using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Enums;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Minutes.Commands
{
    public class SignMinuteInvoiceCommandHandler : IRequestHandler<SignMinuteInvoiceCommand, Result<string>>
    {
        private readonly IUnitOfWork _uow; 
        private readonly IPdfService _pdfService;
        private readonly IInvoiceXMLService _invoiceXmlService;
        private readonly IFileStorageService _cloudinaryService;
        private readonly ICurrentUserService _currentUser;

        public SignMinuteInvoiceCommandHandler(
            IUnitOfWork uow,
            IPdfService pdfService,
            IInvoiceXMLService invoiceXmlService,
            IFileStorageService cloudinaryService,
            ICurrentUserService currentUser)
        {
            _uow = uow;
            _pdfService = pdfService;
            _invoiceXmlService = invoiceXmlService;
            _cloudinaryService = cloudinaryService;
            _currentUser = currentUser;
        }

        public async Task<Result<string>> Handle(SignMinuteInvoiceCommand request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(_currentUser.UserId);
            var minute = await _uow.MinuteInvoiceRepository.GetByIdAsync(request.MinuteInvoiceId);
            if (minute == null)
                return Result.Fail(new Error("Biên bản không tồn tại."));

            if (minute.IsSellerSigned)
                return Result.Fail(new Error("Biên bản này bên bán đã ký rồi."));

            // BƯỚC 2: Tải file PDF hiện tại về RAM
            byte[] pdfBytes;
            try
            {
                if (minute.FilePath.StartsWith("http"))
                {
                    // Gọi Service đã tách ra
                    pdfBytes = await _pdfService.DownloadFileBytesAsync(minute.FilePath);
                }
                else
                {
                    return Result.Fail(new Error("Hệ thống chỉ hỗ trợ ký file từ Cloud."));
                }
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error($"Không thể tải file PDF gốc: {ex.Message}"));
            }
            var certResult = await _invoiceXmlService.GetCertificateAsync(1); // ID công ty = 1
            if (certResult.IsFailed)
                return Result.Fail(certResult.Errors);

            var signingCert = certResult.Value;

            // Ký số vào file (Kết quả là byte[] đã ký)
            byte[] signedPdfBytes;
            try
            {
                signedPdfBytes = _pdfService.SignPdfAtText(pdfBytes, signingCert, request.SearchText);
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error($"Lỗi khi ký file PDF: {ex.Message}"));
            }

            // BƯỚC 4: Upload file ĐÃ KÝ lên lại Cloudinary
            // Tạo tên file mới để tránh cache hoặc ghi đè (thêm suffix _Signed)
            string newFileName = Path.GetFileNameWithoutExtension(minute.FilePath) + "_Signed_A.pdf";

            // Convert byte[] sang IFormFile bằng helper class ở trên
            IFormFile formFile = new MemoryFormFile(signedPdfBytes, newFileName);

            var uploadResult = await _cloudinaryService.UploadFileAsync(formFile);
            if (uploadResult.IsFailed)
                return Result.Fail(uploadResult.Errors);

            // BƯỚC 5: Cập nhật Database
            minute.FilePath = uploadResult.Value.Url; // Cập nhật link mới (file đã có chữ ký)
            minute.IsSellerSigned = true;
            minute.SellerSignedAt = DateTime.UtcNow;

            // Logic trạng thái: 
            // Nếu bên Mua đã ký rồi (trường hợp hiếm: khách ký trước) -> Hoàn thành
            // Nếu chưa -> Chờ khách ký
            if (minute.IsBuyerSigned)
            {
                minute.Status = EMinuteStatus.Complete; 
            }
            else
            {
                minute.Status = EMinuteStatus.Sent; 
            }

            await _uow.MinuteInvoiceRepository.UpdateAsync(minute); // Nếu dùng EF Core tracking thì dòng này có thể ko cần
            await _uow.SaveChanges();

            return Result.Ok(uploadResult.Value.Url);
        }
    }
}
