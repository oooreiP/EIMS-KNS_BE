using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Requests;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceRequests.Commands
{
    public class UploadEvidenceCommandHandler : IRequestHandler<UploadEvidenceCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;

        public UploadEvidenceCommandHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
        }
        public async Task<Result<string>> Handle(UploadEvidenceCommand request, CancellationToken cancellationToken)
        {
            var invoiceRequest = await _unitOfWork.InvoiceRequestRepository.GetByIdAsync(request.RequestID,"Customer");
            if (invoiceRequest == null)
            {
                return Result.Fail(new Error($"Request not found.")
                   .WithMetadata("ErrorCode", "Not Found"));
            }
            if (request.PdfFile == null || request.PdfFile.Length == 0)
                throw new Exception("Vui lòng đính kèm file biên bản PDF.");

            if (Path.GetExtension(request.PdfFile.FileName).ToLower() != ".pdf")
                throw new Exception("Chỉ chấp nhận file định dạng PDF.");
            string fileName = $"Evidence_{invoiceRequest.Customer.CustomerName}{Guid.NewGuid()}.pdf";
            var uploadResult = await _fileStorageService.UploadFileAsync(request.PdfFile);

            if (uploadResult.IsFailed)
            {
                string errorMsg = uploadResult.Errors.FirstOrDefault()?.Message ?? "Upload thất bại.";
                throw new Exception($"Lỗi upload file: {errorMsg}");
            }
            string fileUrl = uploadResult.Value.Url;
            invoiceRequest.EvidenceFilePath = fileUrl;
            await _unitOfWork.InvoiceRequestRepository.UpdateAsync(invoiceRequest);
            await _unitOfWork.SaveChanges();
            return fileUrl;
        }
    }
}
