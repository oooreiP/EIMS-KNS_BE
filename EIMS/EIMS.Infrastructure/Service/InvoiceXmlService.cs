using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Mapping;
using EIMS.Application.DTOs.XMLModels;
using EIMS.Domain.Entities;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace EIMS.Infrastructure.Service
{
    public class InvoiceXmlService : IInvoiceXMLService
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public InvoiceXmlService(IFileStorageService fileStorageService, HttpClient httpClient, IConfiguration config, IUnitOfWork unitOfWork)
        {
            _fileStorageService = fileStorageService;
            _httpClient = httpClient;
            _config = config;
            _unitOfWork = unitOfWork;
        }

        // Hàm tải XML từ Cloudinary về XmlDocument
        public async Task<XmlDocument> LoadXmlFromUrlAsync(string url)
        {
            var xmlContent = await _httpClient.GetStringAsync(url);
            var xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true; // QUAN TRỌNG: Giữ nguyên định dạng để không hỏng chữ ký
            xmlDoc.LoadXml(xmlContent);
            return xmlDoc;
        }
        public async Task<string> DownloadStringAsync(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            // TRƯỜNG HỢP 1: File nằm trên Cloud (Cloudinary, Azure, AWS...)
            if (path.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                // Tải nội dung text từ URL
                return await _httpClient.GetStringAsync(path);
            }

            // TRƯỜNG HỢP 2: File nằm trên ổ cứng Server (Local Disk)
            if (System.IO.File.Exists(path))
            {
                // Đọc trực tiếp từ ổ cứng
                return await System.IO.File.ReadAllTextAsync(path);
            }

            throw new FileNotFoundException($"Không tìm thấy file XML tại đường dẫn: {path}");
        }
        // Hàm lưu XmlDocument lên Cloudinary và trả về URL mới
        public async Task<string> UploadXmlAsync(XmlDocument xmlDoc, string fileName)
        {
            using var stream = new MemoryStream();
            xmlDoc.Save(stream);
            stream.Position = 0;

            var uploadResult = await _fileStorageService.UploadFileAsync(stream, fileName, "invoices");
            if (uploadResult.IsFailed) throw new Exception("Upload failed");

            return uploadResult.Value.Url;
        }
        public Result<X509Certificate2> GetCertificate(string? serialNumber = null)
        {
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                var path = _config["Signature:PfxPath"];
                var password = _config["Signature:Password"];
                string fullPath = Path.Combine(basePath, path);
                // Load cert
                var flags = X509KeyStorageFlags.EphemeralKeySet | X509KeyStorageFlags.Exportable;
                var cert = new X509Certificate2(fullPath, password,flags);
                return Result.Ok(cert);
            }
            catch (Exception ex)
            {
                return Result.Fail($"Lỗi tải chứng thư số: {ex.Message}");
            }
        }
        public void EmbedMccqtIntoXml(XmlDocument xmlDoc, string mccqtValue)
        {
            // 1. Tìm vị trí (root) và thẻ MCCQT đã tồn tại
            var root = xmlDoc.DocumentElement; // Thẻ <HDon>

            // Tìm thẻ MCCQT bằng tên
            var mccqtElement = (XmlElement)root.SelectSingleNode("MCCQT");

            if (mccqtElement != null)
            {
                // 2. Nếu thẻ MCCQT đã tồn tại (do đã được serialize từ model)
                // Cập nhật giá trị MCCQT được truyền vào (giá trị 34 ký tự)
                mccqtElement.InnerText = mccqtValue;

                // Bỏ qua logic chèn thẻ (InsertBefore/AppendChild)
                // vì thẻ đã nằm đúng vị trí theo XSD khi serialize từ model.
            }
            else
            {
                // TRƯỜNG HỢP PHÒNG NGỪA: Nếu thẻ MCCQT chưa tồn tại
                // (Chỉ nên xảy ra nếu cấu trúc model của bạn không có MCCQT)

                // Tạo thẻ MCCQT mới
                var newMccqtElement = xmlDoc.CreateElement("MCCQT");
                newMccqtElement.InnerText = mccqtValue;

                // Tìm vị trí để chèn (Phải đúng thứ tự XSD)
                var dscksNode = root.SelectSingleNode("DSCKS");

                if (dscksNode != null)
                {
                    // Nếu đã có chữ ký, chèn MCCQT vào TRƯỚC chữ ký (theo thứ tự XSD)
                    root.InsertBefore(newMccqtElement, dscksNode);
                }
                else
                {
                    // Nếu chưa có chữ ký, chèn vào cuối (Cần xem XSD để chèn đúng vị trí)
                    root.AppendChild(newMccqtElement);
                }
            }
        }
        public async Task<string> GenerateNextNotificationNumberAsync()
        {
            int currentYear = DateTime.Now.Year;
            string prefix = $"TB/{currentYear}/";

            var lastLog = await _unitOfWork.TaxApiLogRepository.GetAllQueryable()
                .Where(x => x.SoTBao != null && x.SoTBao.StartsWith(prefix))
                .OrderByDescending(x => x.TaxLogID) // Hoặc x.Timestamp
                .Select(x => x.SoTBao)
                .FirstOrDefaultAsync();

            long nextSequence = 1;
            if (!string.IsNullOrEmpty(lastLog))
            {
                // lastLog dạng "TB/2025/000050"
                // Cắt bỏ phần prefix "TB/2025/" để lấy "000050"
                string numberPart = lastLog.Substring(prefix.Length);

                if (long.TryParse(numberPart, out long lastSequence))
                {
                    nextSequence = lastSequence + 1;
                }
            }

            // 4. Format số mới (PadZero 6 số: 000001)
            return $"{prefix}{nextSequence:D6}";
        }
        public async Task<Result> ValidateXmlForIssuanceAsync(string xmlUrl)
        {
            try
            {
                // 1. Tải file XML từ Cloudinary
                var xmlContent = await _httpClient.GetStringAsync(xmlUrl);
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlContent);

                // 2. Kiểm tra Chữ ký số (Thẻ <DSCKS> chứa <Signature>)
                // XPath "//" tìm ở bất cứ đâu trong document
                var signatureNode = xmlDoc.SelectSingleNode("//*[local-name()='DSCKS']/*[local-name()='Signature']");
                if (signatureNode == null)
                {
                    return Result.Fail("File XML chưa có Chữ ký số (Thẻ DSCKS/Signature).");
                }

                // 3. Kiểm tra Mã CQT (Thẻ <MCCQT>)
                var mccqtNode = xmlDoc.SelectSingleNode("//*[local-name()='MCCQT']");
                if (mccqtNode == null || string.IsNullOrWhiteSpace(mccqtNode.InnerText))
                {
                    return Result.Fail("File XML chưa có Mã của Cơ quan Thuế (Thẻ MCCQT).");
                }

                // Trả về thành công kèm giá trị MCCQT để tiện lưu DB nếu cần
                return Result.Ok().WithSuccess(mccqtNode.InnerText);
            }
            catch (Exception ex)
            {
                return Result.Fail($"Lỗi khi đọc file XML: {ex.Message}");
            }
        }
        public async Task<string> GenerateAndUploadXmlAsync(Invoice fullInvoice)
        {
            var xmlModel = InvoiceXmlMapper.MapInvoiceToXmlModel(fullInvoice);
            string fileName = fullInvoice.InvoiceNumber.HasValue
                ? $"Invoice_{fullInvoice.InvoiceNumber}.xml"
                : $"Invoice_Draft_{fullInvoice.InvoiceID}.xml";
            using (var memoryStream = new MemoryStream())
            {
                var ns = new XmlSerializerNamespaces();
                ns.Add("hdon", "http://tempuri.org/HDonSchema.xsd");
                var serializer = new XmlSerializer(typeof(HDon));
                serializer.Serialize(memoryStream, xmlModel, ns);
                memoryStream.Position = 0;
                var uploadResult = await _fileStorageService.UploadFileAsync(memoryStream, fileName, "invoices");
                if (uploadResult.IsFailed)
                {
                    // Ném lỗi để Handler bên ngoài bắt được
                    throw new Exception($"Upload XML thất bại: {uploadResult.Errors[0].Message}");
                }
                return uploadResult.Value.Url;
            }
        }
    }
}
