using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Enums;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Minutes.Commands
{
    public class UpdateMinuteFileHandler : IRequestHandler<UpdateMinuteFileCommand, Result<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IFileStorageService _fileStorageService;
        private readonly ICurrentUserService _currentUser;

        public UpdateMinuteFileHandler(
            IUnitOfWork uow,
            IFileStorageService fileStorageService,
            ICurrentUserService currentUser)
        {
            _uow = uow;
            _fileStorageService = fileStorageService;
            _currentUser = currentUser;
        }

        public async Task<Result<string>> Handle(UpdateMinuteFileCommand request, CancellationToken cancellationToken)
        {
            // 1. LẤY THÔNG TIN USER VÀ BIÊN BẢN
            var userId = int.Parse(_currentUser.UserId);
            var minute = await _uow.MinuteInvoiceRepository.GetByIdAsync(request.MinuteInvoiceId);          
            if (minute == null)
                return Result.Fail("Biên bản không tồn tại.");
            if (minute.Status.Equals(EMinuteStatus.Complete))
                return Result.Fail("Biên bản đã được hai bên cam kết, không thể cập nhật được nữa.");
            if (request.PdfFile == null || request.PdfFile.Length == 0)
                return Result.Fail("Vui lòng đính kèm file biên bản PDF.");
            if (Path.GetExtension(request.PdfFile.FileName).ToLower() != ".pdf")
                return Result.Fail("Chỉ chấp nhận file định dạng PDF.");
            if (!string.IsNullOrEmpty(minute.FilePath)) 
            {
                await _fileStorageService.DeleteAsync(minute.FilePath);
            }
            var uploadResult = await _fileStorageService.UploadFileAsync(request.PdfFile);

            if (uploadResult.IsFailed)
            {
                string errorMsg = uploadResult.Errors.FirstOrDefault()?.Message ?? "Upload thất bại.";
                return Result.Fail($"Lỗi upload file: {errorMsg}");
            }
            string newFileUrl = uploadResult.Value.Url;
            minute.FilePath = newFileUrl;
            minute.Status = EMinuteStatus.Complete;
            await _uow.MinuteInvoiceRepository.UpdateAsync(minute);
            await _uow.SaveChanges();
            return Result.Ok(newFileUrl);
        }
    }
}
