using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs;
using FluentResults;
using MediatR;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.QRCodes.Commands
{
    public class GenerateQrCodeCommandHandler : IRequestHandler<GenerateQrCodeCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQrCodeService _qrCodeService;

        public GenerateQrCodeCommandHandler(IUnitOfWork unitOfWork, IQrCodeService qrCodeService)
        {
            _unitOfWork = unitOfWork;
            _qrCodeService = qrCodeService;
        }

        public async Task<Result<string>> Handle(GenerateQrCodeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var invoice = await _unitOfWork.InvoicesRepository.GetByIdAsync(request.InvoiceID);
                var lookUpCode = _qrCodeService.GenerateLookupCode();
                string targetUrl = $"{request.PortalBaseUrl.TrimEnd('/')}/view?code={lookUpCode}";
                string base64Image = _qrCodeService.GenerateQrImageBase64(targetUrl);
                invoice.QRCodeData = lookUpCode;
                await _unitOfWork.InvoicesRepository.UpdateAsync(invoice);
                await _unitOfWork.SaveChanges();
                return Result.Ok(base64Image);
            }
            catch (Exception ex)
            {
                return Result.Fail<string>(new Error("Lỗi tạo QR tra cứu").CausedBy(ex));
            }
        }               
    }
}

