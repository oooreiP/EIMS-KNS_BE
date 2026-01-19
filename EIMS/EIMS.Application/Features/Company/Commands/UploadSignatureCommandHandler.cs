using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Company.Commands
{
    public class UploadSignatureCommandHandler : IRequestHandler<UploadSignatureCommand, Result<string>>
    {
        private readonly IUnitOfWork _uow;
        // Giả sử bạn có service mã hóa, nếu chưa có hãy xem phần Lưu ý bên dưới
        private readonly IEncryptionService _encryptionService;

        public UploadSignatureCommandHandler(IUnitOfWork uow, IEncryptionService encryptionService)
        {
            _uow = uow;
            _encryptionService = encryptionService;
        }

        public async Task<Result<string>> Handle(UploadSignatureCommand request, CancellationToken cancellationToken)
        {
            // 1. Validate Input
            if (request.File == null || request.File.Length == 0)
            {
                return Result.Fail("Vui lòng chọn file chứng thư số (.pfx, .p12).");
            }

            // 2. Lấy Company từ DB
            var company = await _uow.CompanyRepository.GetByIdAsync(request.CompanyId);
            if (company == null)
            {
                return Result.Fail("Không tìm thấy thông tin doanh nghiệp.");
            }
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await request.File.CopyToAsync(memoryStream, cancellationToken);
                fileBytes = memoryStream.ToArray();
            }
            try
            {
                // X509KeyStorageFlags.EphemeralKeySet giúp tránh lỗi permission trên server khi chỉ check pass
                using var cert = new X509Certificate2(fileBytes, request.Password, X509KeyStorageFlags.EphemeralKeySet);
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                return Result.Fail("Mật khẩu không đúng hoặc file chứng thư số bị lỗi.");
            }
            catch (Exception)
            {
                return Result.Fail("Định dạng file không hợp lệ.");
            }

            // 5. Lưu vào Entity
            company.DigitalSignature = fileBytes;
            string encryptedPassword = _encryptionService.Encrypt(request.Password);
            company.DigitalSignaturePassword = encryptedPassword;

            // Cập nhật loại chữ ký (nếu bạn dùng logic bảng Company mới tôi gợi ý ở trên)
            // company.SignatureType = 1; // 1 = Soft HSM (File)
            // 6. Save DB
            await _uow.CompanyRepository.UpdateAsync(company);
            await _uow.SaveChanges();

            return Result.Ok("Cập nhật chữ ký số thành công.");
        }
    }
}
