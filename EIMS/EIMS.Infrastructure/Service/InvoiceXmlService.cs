using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Vml.Office;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Mapping;
using EIMS.Application.DTOs.XMLModels;
using EIMS.Application.DTOs.XMLModels.PaymentStatements;
using EIMS.Application.DTOs.XMLModels.TB04;
using EIMS.Domain.Entities;
using FluentResults;
using Humanizer;
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
        private readonly IEncryptionService _encryptionService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public InvoiceXmlService(IFileStorageService fileStorageService, HttpClient httpClient, IConfiguration config, IUnitOfWork unitOfWork, IEncryptionService encryptionService)
        {
            _fileStorageService = fileStorageService;
            _httpClient = httpClient;
            _config = config;
            _unitOfWork = unitOfWork;
            _encryptionService = encryptionService;
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
        public async Task<Result<X509Certificate2>> GetCertificateAsync(int companyId)
        {
            // ---------------------------------------------------------
            // BƯỚC 1: CỐ GẮNG LẤY TỪ DATABASE (COMPANY)
            // ---------------------------------------------------------
            try
            {
                var company = await _unitOfWork.CompanyRepository.GetByIdAsync(companyId);
                if (company != null && company.DigitalSignature != null && company.DigitalSignature.Length > 0)
                {
                    string dbPassword = "";
                    if (!string.IsNullOrEmpty(company.DigitalSignaturePassword))
                    {
                        dbPassword = _encryptionService.Decrypt(company.DigitalSignaturePassword);
                    }

                    var dbFlags = X509KeyStorageFlags.EphemeralKeySet | X509KeyStorageFlags.Exportable;
                    var dbCert = new X509Certificate2(company.DigitalSignature, dbPassword, dbFlags);

                    // Nếu load thành công -> Trả về ngay
                    return Result.Ok(dbCert);
                }
            }
            catch (Exception)
            {
               Console.WriteLine("Không lấy được cert từ DB, chuyển sang file local...");
            }
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                var path = _config["Signature:PfxPath"];
                var password = _config["Signature:Password"];

                // Nếu cấu hình không tồn tại thì báo lỗi luôn
                if (string.IsNullOrEmpty(path))
                {
                    return Result.Fail<X509Certificate2>("Không tìm thấy cấu hình chữ ký số trong Database lẫn appsettings.");
                }

                string fullPath = Path.Combine(basePath, path);

                if (!File.Exists(fullPath))
                {
                    return Result.Fail<X509Certificate2>($"Không tìm thấy file chứng thư số tại: {fullPath}");
                }

                var flags = X509KeyStorageFlags.EphemeralKeySet | X509KeyStorageFlags.Exportable;
                var localCert = new X509Certificate2(fullPath, password, flags);

                return Result.Ok(localCert);
            }
            catch (Exception ex)
            {
                return Result.Fail<X509Certificate2>($"Lỗi tải chứng thư số (Fallback): {ex.Message}");
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
                ns.Add("", "http://tempuri.org/HDonSchema.xsd");
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
        public async Task<(XmlDocument XmlDoc, string MessageId)> Generate04SSXmlDocumentAsync(InvoiceErrorNotification notification)
        {
            // Cấu hình cứng (Hard-code) hoặc lấy từ Config
            string companyTaxCode =notification.TaxCode;
            string mnGui = "K" + companyTaxCode; // Thường T-VAN sẽ thêm prefix K
            var messageCode = new TaxMessageCode { MessageCode = "300", FlowType = 1 };
            var ttChung = InvoiceXmlMapper.GenerateTTChung(messageCode, 1, mnGui);
            var messageId = ttChung.MaThongDiep;
            var listHDon = new List<HDonTB04>();
            int stt = 1;

            foreach (var detail in notification.Details)
            {
                // Logic tách Ký hiệu: VD "1C24TAA" -> KHMS="1", KHH="C24TAA"
                string fullSerial = detail.InvoiceSerial?.Trim() ?? "";
                string khmsHDon = "1";
                string khHDon = fullSerial;
                if (!string.IsNullOrEmpty(detail.InvoiceSerial) && detail.InvoiceSerial.Length > 1)
                {
                    khmsHDon = detail.InvoiceSerial.Substring(0, 1);
                    khHDon = detail.InvoiceSerial.Substring(1);
                }

                int tctBao = detail.ErrorType;
                if (tctBao < 1 || tctBao > 3) tctBao = 3; // Mặc định là Giải trình
                listHDon.Add(new HDonTB04
                {
                    STT = stt++,
                    MCCQT = detail.InvoiceTaxCode,
                    KHMSHDon = khmsHDon,
                    KHHDon = khHDon,
                    SHDon = detail.InvoiceNumber, 
                    Ngay = detail.InvoiceDate.ToString("yyyy-MM-dd"),
                    LADHDDT = 1, // 1: Có mã theo NĐ123
                    TCTBao = tctBao,
                    LDo = detail.Reason
                });
            }
            var tBao = new TBao04
            {
                PBan = "2.1.0",
                MSo = "04/SS-HĐĐT",
                Ten = "Thông báo hóa đơn điện tử có sai sót",
                Loai = 1,
                MCQT = notification.TaxAuthorityCode, 
                TCQT = notification.TaxAuthorityName ?? "Cục Thuế TP. Hồ Chí Minh",    
                So = notification.NotificationNumber, 
                NTBCCQT = notification.ReportDate.ToString("yyyy-MM-dd"),
                DLTBao = new DLTBao
                {
                    MST = companyTaxCode,
                    MDVQHNSach = "",
                    DDanh = notification.Place, 
                    NTBao = notification.ReportDate.ToString("yyyy-MM-dd"),

                    DSHDon = new DSHDonWrapper
                    {
                        HDon = listHDon
                    }
                }
            };
            var tDiep = new TDiepTB04
            {
                TTChung = ttChung,
                DLieu = new DLieuTB04 { TBao = tBao }
            };
            var doc = new XmlDocument();
            using (var stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(TDiepTB04));
                using var writer = new StreamWriter(stream, new UTF8Encoding(false));

                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");

                serializer.Serialize(writer, tDiep, ns);
                stream.Position = 0;
                doc.Load(stream);
            }

            return (doc, messageId);
        }
        public async Task<PaymentStatementDTO> GetPaymentRequestXmlAsync(InvoiceStatement statementEntity)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(1);
            if (company == null) throw new Exception("Company not found");
            if (statementEntity == null) throw new Exception("Statement not found");
            var xmlDto = new PaymentStatementDTO
            {
                ProviderInfo = new ProviderInfoDTO
                {
                    Name = company.CompanyName,
                    Address = company.Address,
                    Phone = company.ContactPhone
                },

                // B. Thông tin Header
                HeaderInfo = new HeaderInfoDTO
                {
                    AccountantName = statementEntity.Creator.FullName,
                    StatementDate = statementEntity.StatementDate.ToString("dd/MM/yyyy"),
                    StatementMonth = statementEntity.StatementDate.ToString("MM/yyyy"),
                    CustomerName = statementEntity.Customer?.CustomerName ?? "Unknown Customer",
                    CustomerCode = $"KHACVT_{statementEntity.Customer?.CustomerID}",
                    ContactEmail = "support@einvoice.vn"
                },
                Items = new List<StatementItemDTO>()                           
            };
            var startOfStatementPeriod = new DateTime(statementEntity.StatementDate.Year, statementEntity.StatementDate.Month, 1);

            // Lấy danh sách chi tiết từ Entity
            var allDetails = statementEntity.StatementDetails.ToList();

            // --- PHẦN A: XỬ LÝ CÔNG NỢ CŨ (Các tháng trước) ---
            // Điều kiện: Ngày hóa đơn < Ngày đầu tháng này
            var oldDetails = allDetails
                .Where(d => (d.Invoice.IssuedDate ?? DateTime.MinValue) < startOfStatementPeriod)
                .ToList();

            if (oldDetails.Any())
            {
                var oldItemsSummary = oldDetails
                    .GroupBy(d => new {
                        Month = d.Invoice.IssuedDate?.Month ?? 0,
                        Year = d.Invoice.IssuedDate?.Year ?? 0
                    })
                    .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                    .Select(g => new StatementItemDTO
                    {
                        // Diễn giải gom nhóm: "Dư nợ quá hạn tháng 09/2025"
                        Description = $"Dư nợ quá hạn tháng {g.Key.Month.ToString("00")}/{g.Key.Year}",

                        // Thời gian
                        PeriodFrom = $"{g.Key.Month.ToString("00")}/{g.Key.Year}",
                        PeriodTo = $"{g.Key.Month.ToString("00")}/{g.Key.Year}",

                        IndicatorOld = "0",
                        IndicatorNew = "0",
                        Quantity = 1,

                        // --- LOGIC QUAN TRỌNG CHO NỢ CŨ ---
                        // Chỉ lấy OutstandingAmount (Số tiền còn lại phải trả)
                        // Không hiển thị VAT hay Doanh thu gốc vì đã kê khai ở tháng trước rồi

                        UnitPrice = g.Sum(x => x.OutstandingAmount),
                        Amount = g.Sum(x => x.OutstandingAmount),

                        VATRate = 0, // Để trống
                        VATAmount = 0, // Coi như bằng 0 để không cộng dồn thuế sai kỳ

                        TotalCurrent = 0, // Không phải phát sinh kỳ này
                        PreviousDebt = g.Sum(x => x.OutstandingAmount), // Đưa vào cột Nợ cũ (nếu template có cột này)

                        TotalPayable = g.Sum(x => x.OutstandingAmount) // Tổng phải trả = Tổng số dư nợ
                    });

                xmlDto.Items.AddRange(oldItemsSummary);
            }

            // --- PHẦN B: XỬ LÝ PHÁT SINH TRONG KỲ (Tháng hiện tại) ---
            // Điều kiện: Ngày hóa đơn >= Ngày đầu tháng này
            var currentDetails = allDetails
                .Where(d => (d.Invoice.IssuedDate ?? DateTime.MinValue) >= startOfStatementPeriod)
                .OrderBy(d => d.Invoice.IssuedDate)
                .ToList();

            var currentItemsMapped = currentDetails.Select(d =>
            {
                var invoice = d.Invoice;
                var issuedDate = invoice.IssuedDate ?? DateTime.UtcNow;
                int daysInMonth = DateTime.DaysInMonth(issuedDate.Year, issuedDate.Month);
                string lastDayOfMonth = new DateTime(issuedDate.Year, issuedDate.Month, daysInMonth).ToString("dd/MM/yyyy");

                return new StatementItemDTO
                {
                    Description = invoice != null
                        ? $"Thanh toán hóa đơn số {invoice.InvoiceNumber}"
                        : "Chi tiết công nợ",
                    PeriodFrom = issuedDate.ToString("dd/MM/yyyy"),
                    PeriodTo = lastDayOfMonth,
                    IndicatorOld = "0",
                    IndicatorNew = "0",
                    Quantity = 1,
                    UnitPrice = invoice?.TotalAmount ?? 0, 
                    Amount = invoice?.TotalAmount ?? 0, 
                    VATRate = invoice?.VATRate ?? 0,
                    VATAmount = (invoice?.TotalAmount ?? 0) * 0.1m, 
                    TotalCurrent = (invoice?.TotalAmount ?? 0) * 1.1m,
                    PreviousDebt = 0,
                    TotalPayable = d.OutstandingAmount
                };
            });

            xmlDto.Items.AddRange(currentItemsMapped);
            xmlDto.Summary = new StatementSummaryDTO
            {
                TotalAmount = xmlDto.Items.Sum(x => x.Amount),
                TotalVAT = xmlDto.Items.Sum(x => x.VATAmount),
                TotalCurrentPeriod = xmlDto.Items.Where(x => x.PreviousDebt == 0).Sum(x => x.TotalCurrent),
                TotalPreviousDebt = xmlDto.Items.Sum(x => x.PreviousDebt),
                GrandTotal = statementEntity.TotalAmount

            };
            return xmlDto;
        }
    }
}
