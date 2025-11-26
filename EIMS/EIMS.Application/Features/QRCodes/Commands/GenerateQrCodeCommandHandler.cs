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
    public class GenerateQrCodeCommandHandler : IRequestHandler<GenerateQrCodeCommand, Result<GenerateQrCodeResult>>
    {
        public async Task<Result<GenerateQrCodeResult>> Handle(GenerateQrCodeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //B1: Build data string theo chuẩn QR hóa đơn (Phụ lục XIII)
                var qrData = BuildQrData(request);

                //B2: Tính CRC (ID 63)
                var crc = CalculateCRC16(qrData);

                var fullQr = $"{qrData}6304{crc}";

                //B3: Tạo QR Code Image base64
                string base64 = GenerateQrImageBase64(fullQr);

                return Result.Ok(new GenerateQrCodeResult
                {
                    QrString = fullQr,
                    CrcCode = crc,
                    Base64Image = base64
                });
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error("QR generation failed").CausedBy(ex));
            }
        }

        private string BuildQrData(GenerateQrCodeCommand req)
        {
            // Dữ liệu tối giản theo định dạng Tổng Cục Thuế
            // 00 = Version, 01 = Init method, 26 = Seller info, 52..62 = Info, 99 = Invoice Info
            string date = req.IssueDate.ToString("yyyyMMdd");
            string amount = ((int)req.TotalAmount).ToString("D6");

            var builder = new StringBuilder();
            builder.Append("000201");                    // Version 01
            builder.Append("010212");                    // Dynamic QR
            builder.Append("2608");                      // Seller Info Group (Length placeholder)
            builder.Append($"0010A00000077501");         // GUID + prefix
            builder.Append($"0107{req.SellerTaxCode}");  // Seller tax
            builder.Append("5204"); builder.Append("0000"); // Merchant category (default)
            builder.Append("5303704");                   // Currency (704 = VND)
            builder.Append($"54{amount.Length:D2}{amount}");
            builder.Append("5802VN");                    // Country code
            builder.Append($"590{req.SellerName.Length}{req.SellerName}"); // Seller Name
            builder.Append($"600{req.City.Length}{req.City}");             // City
            builder.Append("6216");                      // Additional data
            builder.Append($"000{req.InvoiceNo.Length}{req.InvoiceNo}");   // Invoice No
            builder.Append($"0508{date}");
            builder.Append($"0606{amount}");
            builder.Append("99");                        // Start tax info
            builder.Append("6304");                      // CRC placeholder

            return builder.ToString();
        }

        private string CalculateCRC16(string data)
        {
            ushort crc = 0xFFFF;
            byte[] bytes = Encoding.ASCII.GetBytes(data);

            foreach (var b in bytes)
            {
                crc ^= (ushort)(b << 8);
                for (int i = 0; i < 8; i++)
                    crc = (ushort)((crc & 0x8000) != 0 ? (crc << 1) ^ 0x1021 : (crc << 1));
            }

            return crc.ToString("X4"); // 4-digit uppercase hex
        }

        private string GenerateQrImageBase64(string qrString)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrData = qrGenerator.CreateQrCode(qrString, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new QRCode(qrData);
            using Bitmap qrBitmap = qrCode.GetGraphic(5);
            using var ms = new MemoryStream();
            qrBitmap.Save(ms, ImageFormat.Png);
            return Convert.ToBase64String(ms.ToArray());
        }
    }
}

