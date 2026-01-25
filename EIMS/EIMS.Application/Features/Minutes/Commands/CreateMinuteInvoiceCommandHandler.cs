using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using EIMS.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Minutes.Commands
{
    public class CreateMinuteInvoiceCommandHandler : IRequestHandler<CreateMinuteInvoiceCommand, int>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICurrentUserService _currentUser;
        IFileStorageService _fileStorageService;
        public CreateMinuteInvoiceCommandHandler(
            IUnitOfWork uow,
            ICurrentUserService currentUser,
            IFileStorageService fileStorageService)
        {
            _uow = uow;
            _currentUser = currentUser;
            _fileStorageService = fileStorageService;
        }

        public async Task<int> Handle(CreateMinuteInvoiceCommand request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(_currentUser.UserId);
            var invoice = await _uow.InvoicesRepository
                .GetByIdAsync(request.InvoiceId);

            if (invoice == null) throw new Exception("Hóa đơn không tồn tại.");
            if (request.PdfFile == null || request.PdfFile.Length == 0)
                throw new Exception("Vui lòng đính kèm file biên bản PDF.");

            if (Path.GetExtension(request.PdfFile.FileName).ToLower() != ".pdf")
                throw new Exception("Chỉ chấp nhận file định dạng PDF.");
            string fileName = $"Minute_{invoice.InvoiceNumber}_{Guid.NewGuid()}.pdf";
            var uploadResult = await _fileStorageService.UploadFileAsync(request.PdfFile);

            if (uploadResult.IsFailed)
            {
                string errorMsg = uploadResult.Errors.FirstOrDefault()?.Message ?? "Upload thất bại.";
                throw new Exception($"Lỗi upload file: {errorMsg}");
            }
            string fileUrl = uploadResult.Value.Url;
            int count = await _uow.MinuteInvoiceRepository.CountAsync(m => m.InvoiceId == request.InvoiceId);
            string typePrefix = request.MinuteType == MinutesType.Adjustment ? "DC" : "TT";
            string minuteCode = $"BB-{typePrefix}-{invoice.InvoiceSymbol}_{invoice.InvoiceNumber}-{count + 1}";
            var newMinute = new MinuteInvoice
            {
                InvoiceId = request.InvoiceId,
                MinuteCode = minuteCode,
                MinutesType = request.MinuteType,
                Status = EMinuteStatus.Pending,

                Description = request.Description,
                FilePath = fileUrl,
                CreatedBy = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _uow.MinuteInvoiceRepository.CreateAsync(newMinute);
            await _uow.SaveChanges();
            return newMinute.MinutesInvoiceId;
        }
    }
}
