
using System.Xml;
using System.Xml.Xsl;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.InvoiceTemplate;
using EIMS.Domain.Entities;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using EIMS.Application.Commons.UnitOfWork;
using EIMS.Application.Commons;
using EIMS.Application.Features.Invoices.Commands.CreateInvoice;
using System.Xml.Serialization;
using EIMS.Application.Features.Invoices.Commands;
using EIMS.Application.Commons.Mapping;
using EIMS.Application.DTOs.XMLModels;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using System.Text;
using System.Globalization;
using iText.Kernel.Pdf;
using iText.Signatures;
using iText.Commons.Bouncycastle.Cert;
using Org.BouncyCastle.X509;
using EIMS.Infrastructure.Security;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using Rectangle = iText.Kernel.Geom.Rectangle;
using System.Net.Http;
using iText.Kernel.Colors;
using iText.Layout.Borders;
using iText.Layout.Properties;
using Org.BouncyCastle.Tls;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Pdf.Xobject;
using iText.Kernel.Pdf.Canvas;
namespace EIMS.Infrastructure.Service
{
    public class PdfService : IPdfService
    {
        private readonly IUnitOfWork _uow;
        private readonly IInvoiceXMLService _xmlService;
        private readonly IQrCodeService _qrService;
        private readonly IHttpClientFactory _httpClientFactory;
        public PdfService(
        IUnitOfWork uow,
        IInvoiceXMLService xmlService,
        IQrCodeService qrService,
        IHttpClientFactory httpClientFactory)
        {
            _uow = uow;
            _xmlService = xmlService;
            _qrService = qrService;
            _httpClientFactory = httpClientFactory;
        }
        private async Task<byte[]> GeneratePdfBytesAsync(string htmlContent)
        {
            string executablePath = null;

            // Check if running on Linux (Docker container)
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                executablePath = "/usr/bin/chromium";
            }
            // On Windows (Localhost), executablePath stays null, 
            // so Puppeteer looks for the local revision you downloaded.
            var launchOptions = new LaunchOptions
            {
                Headless = true,
                ExecutablePath = executablePath,
                DumpIO = true, // Để xem log lỗi nếu có
                Args = new[]
        {
            "--no-sandbox",
            "--disable-setuid-sandbox",
            "--disable-dev-shm-usage", // QUAN TRỌNG: Chống tràn bộ nhớ /dev/shm
            "--disable-gpu"
        }
            };
            launchOptions.Env["HOME"] = "/tmp";
            await using var browser = await Puppeteer.LaunchAsync(launchOptions);
            await using var page = await browser.NewPageAsync();
            await page.SetContentAsync(htmlContent);
            await page.EvaluateExpressionAsync("document.fonts.ready");
            var pdfBytes = await page.PdfDataAsync(new PdfOptions
            {
                Format = PaperFormat.A4,
                PrintBackground = true, 
                MarginOptions = new MarginOptions
                {
                    Top = "10mm",
                    Bottom = "10mm",
                    Left = "10mm",
                    Right = "10mm"
                },
                DisplayHeaderFooter = true,
                HeaderTemplate = "<div></div>",
                FooterTemplate = @"<div style='font-size:9px; font-family:sans-serif; width:100%; text-align:right; margin-right:10mm;'>Trang <span class='pageNumber'></span>/<span class='totalPages'></span></div>",
            });

            return pdfBytes;
        }
        public async Task<string> GenerateInvoiceHtmlAsync(int invoiceId, string rootPath)
        {
            var invoice = await _uow.InvoicesRepository.GetAllQueryable()
                 .Include(x => x.Template)
                 .ThenInclude(t => t.TemplateFrame)
                 .FirstOrDefaultAsync(x => x.InvoiceID == invoiceId);
            if (invoice == null) throw new Exception("Không tìm thấy hóa đơn.");
            if (invoice.Template == null) throw new Exception("Hóa đơn chưa được gán mẫu in (Template).");
            string xmlContent;
            if (string.IsNullOrEmpty(invoice.XMLPath))
            {
                throw new Exception("Hóa đơn chưa có dữ liệu XML.");
            }
            else
            {
                xmlContent = await _xmlService.DownloadStringAsync(invoice.XMLPath);
            }
            var config = JsonSerializer.Deserialize<TemplateConfig>(
                invoice.Template.LayoutDefinition ?? "{}"
            ) ?? new TemplateConfig();
                var xsltArgs = PrepareXsltArguments(config, invoice);
                string xsltPath = System.IO.Path.Combine(rootPath, "Templates", "InvoiceTemplate.xsl");

                if (!System.IO.File.Exists(xsltPath))
                {
                    throw new Exception("Không tìm thấy file mẫu XSLT cũ: " + xsltPath);
                }

                // Gọi hàm Transform XSLT cũ của bạn
                return TransformXmlToHtml(xmlContent, xsltPath, xsltArgs);
            //}
        }

        public async Task<string> PreviewInvoiceHtmlAsync(BaseInvoiceCommand request, string rootPath)
        {
            if (request.Items == null || !request.Items.Any())
                throw new Exception("Hóa đơn phải có ít nhất một mặt hàng.");

            if (request.TemplateID == null || request.TemplateID == 0)
                throw new Exception("Vui lòng chọn mẫu hóa đơn.");
            var template = await _uow.InvoiceTemplateRepository.GetAllQueryable()
                .Include(t => t.Serial).ThenInclude(s => s.Prefix)
                .Include(t => t.Serial).ThenInclude(s => s.SerialStatus)
                .Include(t => t.Serial).ThenInclude(s => s.InvoiceType)
                .Include(t => t.TemplateFrame)
                .FirstOrDefaultAsync(t => t.TemplateID == request.TemplateID.Value);
            if (template == null) throw new Exception("Mẫu hóa đơn không tồn tại.");
            var company = await _uow.CompanyRepository.GetByIdAsync(request.CompanyID);
            if (company == null) throw new Exception("Không tìm thấy thông tin đơn vị bán.");
            var status = await _uow.InvoiceStatusRepository.GetByIdAsync(request.InvoiceStatusID);
            Customer customer;
            if (request.CustomerID != null && request.CustomerID > 0)
            {
                customer = await _uow.CustomerRepository.GetByIdAsync(request.CustomerID.Value);
                if (customer == null) throw new Exception("Khách hàng không tồn tại.");
            }
            else
            {
                customer = new Customer
                {
                    CustomerName = request.CustomerName ?? "Khách hàng chưa đặt tên",
                    TaxCode = request.TaxCode ?? "",
                    Address = request.Address ?? "",
                    ContactEmail = request.ContactEmail,
                    ContactPerson = request.ContactPerson,
                    ContactPhone = request.ContactPhone
                };
            }
            var productIds = request.Items.Select(i => i.ProductId).Distinct().ToList();
            var products = await _uow.ProductRepository.GetAllQueryable()
                .Where(p => productIds.Contains(p.ProductID))
                .ToListAsync();
            var productDict = products.ToDictionary(p => p.ProductID);

            var processedItems = new List<InvoiceItem>();

            foreach (var itemReq in request.Items)
            {
                if (!productDict.TryGetValue(itemReq.ProductId, out var productInfo)) continue;

                decimal finalAmount = (itemReq.Amount ?? 0) > 0
                    ? itemReq.Amount!.Value
                    : productInfo.BasePrice * (decimal)itemReq.Quantity;

                decimal finalVatAmount;
                if ((itemReq.VATAmount ?? 0) > 0)
                {
                    finalVatAmount = itemReq.VATAmount!.Value;
                }
                else
                {
                    decimal vatRate = productInfo.VATRate ?? 0;
                    finalVatAmount = Math.Round(finalAmount * (vatRate / 100m), 2);
                }

                processedItems.Add(new InvoiceItem
                {
                    ProductID = itemReq.ProductId,
                    Product = productInfo, 
                    Quantity = itemReq.Quantity,
                    Amount = finalAmount,
                    VATAmount = finalVatAmount,
                    UnitPrice = productInfo.BasePrice
                });
            }
            decimal subtotal = processedItems.Sum(i => i.Amount);
            decimal vatAmount = processedItems.Sum(i => i.VATAmount);
            decimal totalAmount = (request.TotalAmount > 0) ? request.TotalAmount.Value : (subtotal + vatAmount);

            decimal invoiceVatRate = (subtotal > 0) ? Math.Round((vatAmount / subtotal) * 100, 2) : 0;
            var invoice = new Invoice
            {
                InvoiceID = 0, 
                CreatedAt = DateTime.UtcNow,
                CompanyId = company.CompanyID,
                CustomerID = customer.CustomerID,
                TemplateID = template.TemplateID,
                InvoiceStatus = status,
                Template = template,
                Customer = customer,
                Company = company,
                InvoiceItems = processedItems,
                SubtotalAmount = subtotal,
                VATAmount = vatAmount,
                VATRate = invoiceVatRate,
                TotalAmount = totalAmount,
                TotalAmountInWords = NumberToWordsConverter.ChuyenSoThanhChu(totalAmount),
                Notes = request.Notes,
                PaymentMethod = request.PaymentMethod ?? "TM/CK",
                InvoiceCustomerName = customer.CustomerName,
                InvoiceCustomerAddress = customer.Address,
                InvoiceCustomerTaxCode = customer.TaxCode,
                InvoiceStatusID = request.InvoiceStatusID,
                CreatedBy = request.PerformedBy
            };
            string xmlContent = SerializeInvoiceToXml(invoice);
            var config = JsonSerializer.Deserialize<TemplateConfig>(
                template.LayoutDefinition ?? "{}"
            ) ?? new TemplateConfig();
            var xsltArgs = PrepareXsltArguments(config, invoice);

            string xsltPath = System.IO.Path.Combine(rootPath, "Templates", "InvoiceTemplate.xsl");
            return TransformXmlToHtml(xmlContent, xsltPath, xsltArgs);
        }
        public async Task<byte[]> ConvertXmlToPdfAsync(int invoiceId, string rootPath)
        {
            string htmlContent = await GenerateInvoiceHtmlAsync(invoiceId, rootPath);
            return await GeneratePdfBytesAsync(htmlContent);
        }
        public async Task<byte[]> ConvertHtmlToPdfAsync(string htmlContent)
        {
            return await GeneratePdfBytesAsync(htmlContent);
        }
        public async Task<string> GenerateNotificationHtmlAsync(int notificationId, string rootPath)
        {
            var noti = await _uow.ErrorNotificationRepository.GetByIdAsync(notificationId);
            if (noti == null) throw new Exception("Không tìm thấy tờ khai thông báo sai sót.");

            if (string.IsNullOrEmpty(noti.XMLPath))
            {
                throw new Exception("Tờ khai chưa có dữ liệu XML.");
            }
            string xmlContent = await _xmlService.DownloadStringAsync(noti.XMLPath);
            // 3. Chuẩn bị tham số cho XSLT
            // Mẫu 04 đơn giản hơn Invoice, không cần config JSON phức tạp,
            // nhưng cần truyền Tên Công Ty vào vì XML gốc chỉ có MST.
            var xsltArgs = await PrepareNotificationXsltArguments(noti);

            // 4. Đường dẫn file Template (File Form04SS.xslt mà tôi gửi ở bài trước)
            string xsltPath = System.IO.Path.Combine(rootPath, "Templates", "Form04SS.xsl");

            // 5. Transform
            return TransformXmlToHtml(xmlContent, xsltPath, xsltArgs);
        }

        public async Task<byte[]> ConvertNotificationToPdfAsync(int notificationId, string rootPath)
        {
            // 1. Tạo HTML
            string htmlContent = await GenerateNotificationHtmlAsync(notificationId, rootPath);

            // 2. Gọi lại hàm GeneratePdfBytesAsync (Puppeteer) có sẵn của bạn
            return await GeneratePdfBytesAsync(htmlContent);
        }

        public byte[] SignPdfAtText(byte[] pdfBytes, X509Certificate2 signingCert, string searchText)
        {
            var location = FindText(pdfBytes, searchText);
            if (location == null) throw new Exception($"Không tìm thấy dòng chữ '{searchText}'");

            using (var msInput = new MemoryStream(pdfBytes))
            using (var msOutput = new MemoryStream())
            {
                PdfReader reader = new PdfReader(msInput);
                PdfSigner signer = new PdfSigner(reader, msOutput, new StampingProperties());
                float width = 260;
                float height = 90;
                Rectangle signatureRect = new Rectangle(
                    location.Rect.GetLeft(),
                    location.Rect.GetBottom() - height - 35,
                    width,
                    height);


                signer.SetPageNumber(location.PageNumber);
                signer.SetPageRect(signatureRect);
                PdfSignatureAppearance appearance = signer.GetSignatureAppearance();
                appearance
                    .SetReuseAppearance(false)
                    .SetPageRect(signatureRect)
                    .SetPageNumber(location.PageNumber);

                var pdfDoc = signer.GetDocument();
                PdfFormXObject layer = appearance.GetLayer2();
                Canvas canvas = new Canvas(layer, pdfDoc);
                Color themeColor = new DeviceRgb(0, 102, 204); // xanh giống XSL
                string sellerName = signingCert.GetNameInfo(X509NameType.SimpleName, false);
                // Border
                canvas.Add(new Div()
                    .SetBorder(new SolidBorder(themeColor, 2))
                    .SetPadding(6)
                    .SetWidth(UnitValue.CreatePercentValue(100))
                    .SetHeight(UnitValue.CreatePercentValue(100))
                    .Add(new Paragraph("ĐÃ KÝ ĐIỆN TỬ BỞI")
                        .SetFontSize(10)
                        .SetBold()
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontColor(themeColor))
                    .Add(new Paragraph(sellerName.ToUpper())
                        .SetFontSize(11)
                        .SetBold()
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetMarginTop(3))
                    .Add(new Paragraph($"Ngày ký: {DateTime.UtcNow:dd/MM/yyyy HH:mm:ss}")
                        .SetFontSize(9)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetMarginTop(3))
                    .Add(new Paragraph("✔")
                        .SetFontSize(14)
                        .SetFontColor(ColorConstants.GREEN)
                        .SetTextAlignment(TextAlignment.CENTER))
                );

                canvas.Close();
                X509CertificateParser parser = new X509CertificateParser();
                var bcCert = parser.ReadCertificate(signingCert.RawData);
                var chain = new IX509Certificate[] {
            new iText.Bouncycastle.X509.X509CertificateBC(bcCert)
        };

                IExternalSignature externalSignature = new X509Certificate2Signature(signingCert, "SHA-256");

                signer.SignDetached(externalSignature, chain, null, null, null, 0, PdfSigner.CryptoStandard.CMS);

                return msOutput.ToArray();
            }
        }
        public async Task<byte[]> DownloadFileBytesAsync(string url)
        {
            var client = _httpClientFactory.CreateClient();
            return await client.GetByteArrayAsync(url);
        }
        public async Task<string> GetBlankInvoicePreviewAsync(int templateId, int companyId, string rootPath)
        {
            var template = await _uow.InvoiceTemplateRepository.GetTemplateDetailsAsync(templateId);
            if (template == null) throw new Exception("Mẫu hóa đơn không tồn tại.");

            var company = await _uow.CompanyRepository.GetByIdAsync(companyId);
            if (company == null) throw new Exception("Không tìm thấy thông tin đơn vị bán.");
            var emptyCustomer = new Customer
            {
                CustomerName = "",
                TaxCode = "",
                Address = "",
                ContactPerson = "",
                ContactEmail = "",
                ContactPhone = "" 
            };
            var emptyItems = new List<InvoiceItem>
    {
        new InvoiceItem
        {
            Product = new Product
            {
                Name = "", 
                Unit = "",
                Code = ""
            },
            Quantity = 0,     
            UnitPrice = 0,
            Amount = 0,
            VATAmount = 0
        }
    };
            var invoice = new Invoice
            {
                InvoiceID = 0,
                CreatedAt = DateTime.UtcNow,
                IssuedDate = DateTime.UtcNow,

                InvoiceNumber = null, 

                CompanyId = company.CompanyID,
                Company = company, 

                TemplateID = template.TemplateID,
                Template = template,

                Customer = emptyCustomer,
                InvoiceCustomerName = "",
                InvoiceCustomerAddress = "",
                InvoiceCustomerTaxCode = "",

                InvoiceItems = emptyItems, // Quan trọng: List có 1 item rỗng

                SubtotalAmount = 0,
                VATAmount = 0,
                TotalAmount = 0,
                TotalAmountInWords = "",

                PaymentMethod = "",
                Notes = "" 
            };
            string xmlContent = SerializeInvoiceToXml(invoice);
            var config = JsonSerializer.Deserialize<TemplateConfig>(
                template.LayoutDefinition ?? "{}"
            ) ?? new TemplateConfig();
            var xsltArgs = PrepareXsltArguments(config, invoice);

            string xsltPath = System.IO.Path.Combine(rootPath, "Templates", "InvoiceTemplate.xsl");

            return TransformXmlToHtml(xmlContent, xsltPath, xsltArgs);
        }
        private XsltArgumentList PrepareXsltArguments(TemplateConfig config, Invoice invoice)
        {
            var args = new XsltArgumentList();
            string bgUrl = "";
            if (invoice.Template.TemplateFrame != null && !string.IsNullOrEmpty(invoice.Template.TemplateFrame.ImageUrl))
            {
                bgUrl = invoice.Template.TemplateFrame.ImageUrl; // Lấy từ DB
            }
            else
            {
                bgUrl = config.BackgroundUrl ?? "";
            }
            string logoUrl = !string.IsNullOrEmpty(invoice.Template.LogoUrl)
                ? invoice.Template.LogoUrl
                : "";
            string qrContent = "";
            string mccqt = invoice.TaxAuthorityCode ?? "";
            if (!string.IsNullOrEmpty(invoice.QRCodeData))
            {
                qrContent = $"http://159.223.64.31/swagger/view?code={invoice.QRCodeData}";
            }
            else
            {
                // Fallback: Thông tin cơ bản
                qrContent = $"{invoice.InvoiceNumber}|{invoice.TotalAmount}";
            }
            string qrBase64 = _qrService.GenerateQrImageBase64(qrContent);
            string refText = invoice.ReferenceNote ?? "";
            if (refText.Length > 0)
            {
                args.AddParam("ReferenceText", "", refText);
            }
            args.AddParam("QrCodeData", "", qrBase64);
            var style = config.Style ?? new StyleSettings();
            args.AddParam("ColorTheme", "", style.ColorTheme ?? "#0056b3");
            args.AddParam("FontFamily", "", style.FontFamily ?? "Liberation Serif");
            args.AddParam("LogoUrl", "", logoUrl);
            args.AddParam("BackgroundUrl", "", bgUrl);
            var disp = config.DisplaySettings;
            args.AddParam("IsBilingual", "", disp.IsBilingual ? "true" : "false");
            args.AddParam("ShowQrCode", "", disp.ShowQrCode ? "true" : "false");
            args.AddParam("ShowSignature", "", disp.ShowSignature ? "true" : "false");
            args.AddParam("ShowLogo", "", disp.ShowLogo ? "true" : "false");
            args.AddParam("ShowCompanyName", "", disp.ShowCompanyName ? "true" : "false");
            args.AddParam("ShowCompanyTaxCode", "", disp.ShowTaxCode ? "true" : "false");
            args.AddParam("ShowCompanyAddress", "", disp.ShowAddress ? "true" : "false");
            args.AddParam("ShowCompanyPhone", "", disp.ShowPhone ? "true" : "false");
            args.AddParam("ShowCompanyBankAccount", "", disp.ShowBankAccount ? "true" : "false");
            var cust = config.CustomerSettings;
            args.AddParam("ShowCusName", "", cust.ShowName ? "true" : "false");
            args.AddParam("ShowCusTaxCode", "", cust.ShowTaxCode ? "true" : "false");
            args.AddParam("ShowCusAddress", "", cust.ShowAddress ? "true" : "false");
            args.AddParam("ShowCusPhone", "", cust.ShowPhone ? "true" : "false");
            args.AddParam("ShowCusEmail", "", cust.ShowEmail ? "true" : "false");
            args.AddParam("ShowPaymentMethod", "", cust.ShowPaymentMethod ? "true" : "false");
            bool isDraft = invoice.InvoiceStatusID == 1;
            args.AddParam("IsDraft", "", isDraft ? "true" : "false");

            return args;
        }
        private async Task<XsltArgumentList> PrepareNotificationXsltArguments(InvoiceErrorNotification noti)
        {
            var args = new XsltArgumentList();
            var company = await _uow.CompanyRepository.GetByIdAsync(1);
            string companyName = noti.TaxpayerName ?? company?.CompanyName ?? "TÊN CÔNG TY CHƯA CẬP NHẬT";
            string place = noti.Place;
            string providerName = "Hệ thống EIMS";
            bool isApproved = noti.Status == 4;
            args.AddParam("IsApproved", "", isApproved ? "true" : "false");
            args.AddParam("ProviderName", "", providerName);
            args.AddParam("Place", "", place);
            args.AddParam("CompanyName", "", companyName);
            bool isDraft = noti.Status == 1;
            args.AddParam("IsDraft", "", isDraft ? "true" : "false");
            return args;
        }
        private TextLocation FindText(byte[] pdfBytes, string searchText)
        {
            using (var ms = new MemoryStream(pdfBytes))
            using (var reader = new PdfReader(ms))
            using (var pdfDoc = new iText.Kernel.Pdf.PdfDocument(reader))
            {
                for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
                {
                    var strategy = new RegexBasedLocationExtractionStrategy(searchText);
                    PdfCanvasProcessor processor = new PdfCanvasProcessor(strategy);
                    processor.ProcessPageContent(pdfDoc.GetPage(i));

                    var occurrences = strategy.GetResultantLocations();
                    if (occurrences.Count > 0)
                    {
                        return new TextLocation
                        {
                            PageNumber = i,
                            Rect = occurrences.First().GetRectangle()
                        };
                    }
                }
            }
            return null;
        }
        public string TransformXmlToHtml(string xmlContent, string xsltPath, XsltArgumentList? args = null)
        {
            if (!File.Exists(xsltPath))
                throw new FileNotFoundException($"Không tìm thấy file mẫu in tại: {xsltPath}");

            var transform = new XslCompiledTransform();
            transform.Load(xsltPath); // Load file .xsl

            using (var stringReader = new StringReader(xmlContent))
            using (var xmlReader = XmlReader.Create(stringReader))
            using (var stringWriter = new StringWriter())
            {
                // Transform có truyền Argument List
                transform.Transform(xmlReader, args, stringWriter);
                return stringWriter.ToString();
            }
        }
        public string SerializeInvoiceToXml(Invoice invoice)
        {
            var xmlModel = InvoiceXmlMapper.MapInvoiceToXmlModel(invoice);
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "http://tempuri.org/HDonSchema.xsd");
            var serializer = new XmlSerializer(typeof(HDon));
            using (var stringWriter = new StringWriter())
            {
                var settings = new XmlWriterSettings
                {
                    Indent = true,
                    OmitXmlDeclaration = true
                };

                using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
                {
                    serializer.Serialize(xmlWriter, xmlModel, ns);
                    return stringWriter.ToString();
                }
            }
        }
        public string MergeXmlToHtml(string htmlTemplate, string xmlContent)
        {
            // 1. Parse XML (Bỏ qua Namespace để dễ query)
            var xDoc = XDocument.Parse(xmlContent);

            // Helper: Lấy giá trị của thẻ bất kỳ theo tên (bỏ qua namespace)
            string GetVal(string tag) => xDoc.Descendants()
                                             .FirstOrDefault(x => x.Name.LocalName == tag)?.Value ?? "";

            // Helper: Lấy giá trị bên trong một node cha cụ thể
            string GetChildVal(XElement parent, string tag) => parent?.Elements()
                                             .FirstOrDefault(x => x.Name.LocalName == tag)?.Value ?? "";

            StringBuilder sb = new StringBuilder(htmlTemplate);

            // --- A. THÔNG TIN CHUNG (HEADER) ---
            sb.Replace("{{invoiceNumber}}", GetVal("SHDon"));
            sb.Replace("{{invoiceSymbol}}", GetVal("KHHDon"));
            sb.Replace("{{templateCode}}", GetVal("KHMSHDon"));

            // Format ngày: 2026-01-19 -> 19/01/2026
            string dateRaw = GetVal("NLap");
            if (DateTime.TryParse(dateRaw, out DateTime dateVal))
                sb.Replace("{{createdDate}}", dateVal.ToString("dd/MM/yyyy"));
            else
                sb.Replace("{{createdDate}}", dateRaw);

            sb.Replace("{{currency}}", GetVal("DVTTe"));
            // Tỷ giá (nếu có)
            string tyGia = GetVal("TGia");
            sb.Replace("{{exchangeRate}}", !string.IsNullOrEmpty(tyGia) ? $"Tỷ giá: {tyGia}" : "");

            // --- B. NGƯỜI BÁN (NBan) ---
            var nBan = xDoc.Descendants().FirstOrDefault(x => x.Name.LocalName == "NBan");
            sb.Replace("{{providerName}}", GetChildVal(nBan, "Ten"));
            sb.Replace("{{providerTaxCode}}", GetChildVal(nBan, "MST"));
            sb.Replace("{{providerAddress}}", GetChildVal(nBan, "DChi"));
            sb.Replace("{{providerPhone}}", GetChildVal(nBan, "SDThoai"));
            sb.Replace("{{providerEmail}}", GetChildVal(nBan, "DCTDTu"));
            sb.Replace("{{providerBankAccount}}", GetChildVal(nBan, "STKNHang"));
            sb.Replace("{{providerBankName}}", GetChildVal(nBan, "TNHang"));

            // --- C. NGƯỜI MUA (NMua) ---
            var nMua = xDoc.Descendants().FirstOrDefault(x => x.Name.LocalName == "NMua");
            sb.Replace("{{customerName}}", GetChildVal(nMua, "Ten"));
            sb.Replace("{{customerTaxCode}}", GetChildVal(nMua, "MST"));
            sb.Replace("{{customerAddress}}", GetChildVal(nMua, "DChi"));
            sb.Replace("{{customerPhone}}", GetChildVal(nMua, "SDThoai"));
            sb.Replace("{{paymentMethod}}", GetVal("HTTToan")); // Hình thức thanh toán

            // --- D. DANH SÁCH HÀNG HÓA (QUAN TRỌNG) ---
            // Tìm tất cả thẻ HHDVu
            var productNodes = xDoc.Descendants().Where(x => x.Name.LocalName == "HHDVu");
            string productRowsHtml = GenerateProductRowsFromXml(productNodes);
            sb.Replace("{{productRows}}", productRowsHtml);

            // --- E. TỔNG TIỀN (TToan) ---
            var tToan = xDoc.Descendants().FirstOrDefault(x => x.Name.LocalName == "TToan");

            // Format tiền tệ (VD: 1000000 -> 1.000.000)
            string FmtMoney(string val)
            {
                if (decimal.TryParse(val, out decimal d))
                    return d.ToString("#,##0", new CultureInfo("vi-VN"));
                return val;
            }

            sb.Replace("{{totalBeforeTax}}", FmtMoney(GetChildVal(tToan, "TgTCThue"))); // Tổng tiền chưa thuế
            sb.Replace("{{totalVat}}", FmtMoney(GetChildVal(tToan, "TgTThue")));       // Tổng tiền thuế
            sb.Replace("{{totalAmount}}", FmtMoney(GetChildVal(tToan, "TgTTTBSo")));   // Tổng tiền thanh toán
            sb.Replace("{{amountInWords}}", GetChildVal(tToan, "TgTTTBChu"));          // Số tiền bằng chữ

            // --- F. CHỮ KÝ SỐ ---
            // Kiểm tra xem đã có chữ ký chưa (thẻ SignatureValue)
            bool isSigned = xDoc.Descendants().Any(x => x.Name.LocalName == "SignatureValue");
            string signatureHtml = "";

            if (isSigned)
            {
                string signerName = GetChildVal(nBan, "Ten"); // Người ký là người bán
                string signDateStr = GetVal("NLap"); // Mặc định lấy ngày lập, hoặc parse SigningTime nếu muốn chuẩn hơn

                // Nếu muốn lấy SigningTime từ Signature (nếu có)
                var signingTime = xDoc.Descendants().FirstOrDefault(x => x.Name.LocalName == "SigningTime")?.Value;
                if (!string.IsNullOrEmpty(signingTime) && DateTime.TryParse(signingTime, out DateTime sTime))
                {
                    signDateStr = sTime.ToString("dd/MM/yyyy");
                }

                // HTML khung chữ ký (Khớp style với template static)
                signatureHtml = $@"
            <div class='signature-box' style='margin-top: 10px; border: 2px solid #2979ff; color: #2979ff; padding: 10px; border-radius: 4px; display: inline-block; text-align: left;'>
                <div style='font-size: 11px; font-weight: bold; text-transform: uppercase;'>Signature Valid</div>
                <div style='font-size: 11px;'>Ký bởi: <b>{signerName}</b></div>
                <div style='font-size: 11px;'>Ngày ký: {signDateStr}</div>
            </div>";
            }

            sb.Replace("{{sellerSignature}}", signatureHtml);

            return sb.ToString();
        }
        private string GenerateProductRowsFromXml(IEnumerable<XElement> items)
        {
            if (items == null || !items.Any()) return "";

            StringBuilder rows = new StringBuilder();

            // Helper lấy giá trị trong thẻ Item
            string GetItemVal(XElement item, string tag) =>
                item.Elements().FirstOrDefault(e => e.Name.LocalName == tag)?.Value ?? "";

            // Helper format số lượng/tiền
            string Fmt(string val)
            {
                if (decimal.TryParse(val, out decimal d)) return d.ToString("#,##0", new CultureInfo("vi-VN"));
                return val;
            }

            foreach (var item in items)
            {
                // 1. Lấy dữ liệu thô từ XML
                string stt = GetItemVal(item, "STT");
                string name = GetItemVal(item, "THHDVu"); 
                string unit = GetItemVal(item, "DVTinh"); 
                string qty = GetItemVal(item, "SLuong");
                string price = GetItemVal(item, "DGia");
                string amount = GetItemVal(item, "ThTien"); 
                string vatRate = GetItemVal(item, "TSuat");
                string vatAmount = GetItemVal(item, "TGTGT"); 

                // Tính tổng sau thuế (nếu XML không có sẵn trong từng dòng thì cộng tay)
                // Trong mẫu XML của bạn, thường dòng hàng chỉ có Thành tiền chưa thuế.
                // Cột cuối cùng trong template là "Thành tiền" (Total) hay "Tổng cộng"? 
                // Dựa vào template: Cột cuối là "Thành tiền (Amount)" thường là tiền chưa thuế, 
                // nhưng nếu template yêu cầu "Tổng sau thuế" thì:
                decimal.TryParse(amount, out decimal amtDec);
                decimal.TryParse(vatAmount, out decimal vatDec);
                string totalRow = (amtDec + vatDec).ToString("#,##0", new CultureInfo("vi-VN"));
                string vatDisplay = vatRate.StartsWith("K") ? vatRate : vatRate + "%";
                string row = $@"
        <tr class='table-row'>
            <td class='table-cell text-center'>{stt}</td>
            <td class='table-cell text-left' style='font-weight: 500;'>{name}</td>
            <td class='table-cell text-center'>{unit}</td>
            <td class='table-cell text-right'>{Fmt(qty)}</td>
            <td class='table-cell text-right'>{Fmt(price)}</td>
            <td class='table-cell text-right'>{Fmt(amount)}</td>
            <td class='table-cell text-center'>{vatDisplay}</td>
            <td class='table-cell text-right'>{Fmt(vatAmount)}</td>
            <td class='table-cell text-right' style='font-weight: bold;'>{totalRow}</td>
        </tr>";

                rows.Append(row);
            }

            return rows.ToString();
        }
        // Helper format tiền
        private string FormatMoney(string val)
        {
            if (decimal.TryParse(val, out decimal d)) return d.ToString("N0");
            return val;
        }
    }
}