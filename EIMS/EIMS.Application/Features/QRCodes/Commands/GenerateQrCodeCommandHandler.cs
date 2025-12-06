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

        public GenerateQrCodeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(GenerateQrCodeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var invoice = await _unitOfWork.InvoicesRepository.GetByIdAsync(request.InvoiceID);
                var lookUpCode = GenerateLookupCode();
                string targetUrl = $"{request.PortalBaseUrl.TrimEnd('/')}/view?code={lookUpCode}";
                string base64Image = GenerateQrImageBase64(targetUrl);
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

        private string GenerateQrImageBase64(string content)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.M);
            using var qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeBytes = qrCode.GetGraphic(20);

            return Convert.ToBase64String(qrCodeBytes);
        }
        public static string GenerateLookupCode()
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

