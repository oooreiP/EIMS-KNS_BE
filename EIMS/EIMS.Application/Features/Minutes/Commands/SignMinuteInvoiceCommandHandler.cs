using EIMS.Application.Commons.Helpers;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Mails;
using EIMS.Domain.Entities;
using EIMS.Domain.Enums;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
        private readonly IInvoiceXMLService _xmlService;
        private readonly IFileStorageService _fileStorage;
        private readonly IMediator _mediator;

        public SignMinuteInvoiceCommandHandler(
            IUnitOfWork uow,
            IPdfService pdfService,
            IInvoiceXMLService xmlService,
            IFileStorageService fileStorage,
            IMediator mediator)
        {
            _uow = uow;
            _pdfService = pdfService;
            _xmlService = xmlService;
            _fileStorage = fileStorage;
            _mediator = mediator;
        }

        public async Task<Result<string>> Handle(SignMinuteInvoiceCommand request, CancellationToken cancellationToken)
        {
            // 1. Lấy dữ liệu
            var minute = await _uow.MinuteInvoiceRepository.GetByIdAsync(request.MinuteInvoiceId); // Không cần include Customer ở đây cho nhẹ
            if (minute == null) return Result.Fail("Không tìm thấy biên bản.");
            if (minute.IsSellerSigned) return Result.Fail("Biên bản đã được ký trước đó.");
            byte[] pdfBytes;
            if (minute.FilePath.StartsWith("http"))
                pdfBytes = await _pdfService.DownloadFileBytesAsync(minute.FilePath);
            else
                return Result.Fail("Đường dẫn file không hợp lệ.");
            var certResult = await _xmlService.GetCertificateAsync(1); // ID công ty
            if (certResult.IsFailed) return Result.Fail(certResult.Errors[0].Message);
            var fontPath = Path.Combine(request.RootPath, "Fonts", "arial.ttf");
            byte[] signedPdfBytes;
            try
            {
                signedPdfBytes = _pdfService.SignPdfAtText(pdfBytes, certResult.Value, request.SearchText, fontPath);
            }
            catch (Exception ex)
            {
                return Result.Fail($"Lỗi khi ký PDF: {ex.Message}");
            }

            string newFileName = Path.GetFileNameWithoutExtension(minute.FilePath) + "_Signed_B.pdf";
            IFormFile formFile = new MemoryFormFile(signedPdfBytes, newFileName); 

            var uploadResult = await _fileStorage.UploadFileAsync(formFile);
            if (uploadResult.IsFailed) return Result.Fail("Lỗi upload file đã ký.");
            minute.FilePath = uploadResult.Value.Url;
            minute.IsSellerSigned = true;
            minute.SellerSignedAt = DateTime.UtcNow;
            if (minute.IsBuyerSigned)
                minute.Status = EMinuteStatus.Complete;
            else
                minute.Status = EMinuteStatus.Sent;

            _uow.MinuteInvoiceRepository.UpdateAsync(minute);
            await _uow.SaveChanges();
            await _mediator.Publish(new MinuteSignedEvent(minute.MinutesInvoiceId), cancellationToken);

            return Result.Ok(minute.FilePath);
        }
    }
}
