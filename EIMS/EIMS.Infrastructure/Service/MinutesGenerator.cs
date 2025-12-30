using DocumentFormat.OpenXml.Packaging;
using EIMS.Domain.Entities;
using MiniSoftware;
using Microsoft.AspNetCore.Hosting;
using EIMS.Application.Commons.Models;
using Microsoft.Extensions.Options;
using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Enums;
namespace EIMS.Infrastructure.Service
{
    public class MinutesGenerator : IMinutesGenerator
    {
        private readonly string _templateFolder;
        public MinutesGenerator(IOptions<FileSettings> settings)
        {
            // 1. Lấy tên thư mục từ cấu hình (VD: "Templates")
            string folderName = settings.Value.TemplateFolderPath;

            // 2. Lấy đường dẫn gốc nơi ứng dụng đang chạy (bin/Debug/net...)
            // AppContext.BaseDirectory là cách chuẩn nhất để lấy đường dẫn file .dll/.exe đang chạy
            string basePath = AppContext.BaseDirectory;

            // 3. Ghép thành đường dẫn tuyệt đối
            _templateFolder = Path.Combine(basePath, folderName);
        }

        public async Task<byte[]> GenerateReplacementMinutesAsync(Invoice invoice, string reason, string contentBefore, string contentAfter,string adjustmentNumber, DateTime agreementDate)
        {
            // Đường dẫn file mẫu (Bạn nhớ copy file mẫu vào thư mục wwwroot/templates hoặc thư mục gốc)
            string templatePath = Path.Combine(_templateFolder, "BienBan_ThayThe.docx");
            return await GenerateMinutesFromTemplate(templatePath, invoice, reason, contentBefore, contentAfter, adjustmentNumber, agreementDate, "Thay thế");
        }

        public async Task<byte[]> GenerateAdjustmentMinutesAsync(Invoice invoice, string reason, string contentBefore, string contentAfter, string adjustmentNumber, DateTime agreementDate)
        {
            string templatePath = Path.Combine(_templateFolder, "BienBan_DieuChinh.docx");
            return await GenerateMinutesFromTemplate(templatePath, invoice, reason, contentBefore, contentAfter, adjustmentNumber, agreementDate, "Điều chỉnh");
        }

        private async Task<byte[]> GenerateMinutesFromTemplate(string templatePath, Invoice invoice, string reason, string contentBefore, string contentAfter, string adjustmentNumber, DateTime date, string typeName)
        {
            if (!File.Exists(templatePath))
                throw new FileNotFoundException($"Không tìm thấy file mẫu: {templatePath}");
            var template = invoice.Template;
            var serial = template.Serial;
            var prefix = serial.Prefix;
            string khmsHDon = prefix.PrefixID.ToString();
            string khHDon =
                $"{serial.SerialStatus.Symbol}" +
                $"{serial.Year}" +                
                $"{serial.InvoiceType.Symbol}" +
                $"{serial.Tail}";
            var value = new Dictionary<string, object>()
            {
                ["Day"] = date.Day.ToString("00"),
                ["Month"] = date.Month.ToString("00"),
                ["Year"] = date.Year.ToString(),
                ["Reason"] = reason,
                ["Prefix"] = khmsHDon,
                ["Serial"] = khHDon,
                ["Invoice_InvoiceNumber"] = invoice.InvoiceNumber.ToString(),
                ["Adjustment_InvoiceNumber"] = adjustmentNumber,
                ["ContentBefore"] = contentBefore,
                ["ContentAfter"] = contentAfter,
                ["SellerName"] = invoice.Company?.CompanyName,
                ["SellerAddress"] = invoice.Company?.Address,
                ["SellerTaxCode"] = invoice.Company?.TaxCode,
                ["BuyerName"] = invoice.Customer?.CustomerName,
                ["BuyerAddress"] = invoice.Customer?.Address,
                ["BuyerTaxCode"] = invoice.Customer?.TaxCode,
                ["InvoiceNumber"] = invoice.InvoiceNumber,
                ["InvoiceDate"] = invoice.CreatedAt.ToString("dd/MM/yyyy"),
                ["AdjustmentInvoiceDate"] = invoice.CreatedAt.ToString("dd/MM/yyyy")
            };

            using (var stream = new MemoryStream())
            {
                MiniWord.SaveAsByTemplate(stream, templatePath, value);

                return stream.ToArray();
            }
        }
    }
}

