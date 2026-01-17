
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
using Spire.Pdf.Security;
using Spire.Pdf;
using System.Drawing;
using FluentResults;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Cryptography.X509Certificates;
using Spire.Pdf.Graphics;
using Spire.Pdf.Interactive.DigitalSignatures;
using PdfSignature = Spire.Pdf.Security.PdfSignature;
using GraphicMode = Spire.Pdf.Security.GraphicMode;
using System.Drawing.Drawing2D;
using DocumentFormat.OpenXml.ExtendedProperties;
namespace EIMS.Infrastructure.Service
{
    public class PdfService : IPdfService
    {
        private readonly IUnitOfWork _uow;
        private readonly IInvoiceXMLService _xmlService;
        private readonly IQrCodeService _qrService;
        public PdfService(
        IUnitOfWork uow,
        IInvoiceXMLService xmlService,
        IQrCodeService qrService)
        {
            _uow = uow;
            _xmlService = xmlService;
            _qrService = qrService;
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
            return TransformXmlToHtml(xmlContent, xsltPath, xsltArgs);
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
        public byte[] SignPdfUsingSpire(byte[] pdfBytes, X509Certificate2 signingCert)
        {
            if (!signingCert.HasPrivateKey)
            {
                throw new Exception("Certificate không chứa Private Key. Không thể ký số.");
            }

            using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
            {
                PdfDocument doc = new PdfDocument(pdfStream);
                PdfCertificate cert = new PdfCertificate(signingCert);
                PdfPageBase page = doc.Pages[doc.Pages.Count - 1];
                PdfSignature signature = new PdfSignature(doc, page, cert, "Signature_HSM");
                float x = 350;
                float y = 600;
                float width = 180;
                float height = 100;
                signature.Bounds = new RectangleF(new PointF(x, y), new SizeF(width, height));
                using (Bitmap bitmap = new Bitmap((int)width, (int)height))
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    g.Clear(Color.Transparent); 
                    Pen borderPen = new Pen(Color.Red, 3); 
                    g.DrawRectangle(borderPen, 1, 1, width - 2, height - 2);
                    Font fontTitle = new Font("Arial", 10, FontStyle.Bold);
                    Brush brushTitle = Brushes.Red;
                    string title = "ĐÃ KÝ ĐIỆN TỬ BỞI";
                    SizeF titleSize = g.MeasureString(title, fontTitle);
                    float titleX = (width - titleSize.Width) / 2;
                    g.DrawString(title, fontTitle, brushTitle, titleX, 10);
                    string companyName = GetCommonName(cert.Subject) ?? "CÔNG TY CỔ PHẦN EIMS";
                    Font fontCompany = new Font("Arial", 9, FontStyle.Bold);
                    RectangleF rectCompany = new RectangleF(5, 35, width - 10, 40);
                    StringFormat format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    g.DrawString(companyName.ToUpper(), fontCompany, brushTitle, rectCompany, format);
                    string dateStr = $"Ngày ký: {DateTime.Now:dd/MM/yyyy}";
                    Font fontDate = new Font("Arial", 8, FontStyle.Regular);

                    SizeF dateSize = g.MeasureString(dateStr, fontDate);
                    float dateX = (width - dateSize.Width) / 2;
                    g.DrawString(dateStr, fontDate, brushTitle, dateX, 75);
                    using (MemoryStream msImg = new MemoryStream())
                    {
                        bitmap.Save(msImg, System.Drawing.Imaging.ImageFormat.Png);
                        msImg.Position = 0;
                        PdfImage pdfImage = PdfImage.FromStream(msImg);
                        signature.GraphicsMode = Spire.Pdf.Security.GraphicMode.SignImageOnly;
                        signature.SignImageSource = pdfImage;
                    }
                }
                using (MemoryStream outStream = new MemoryStream())
                {
                    doc.SaveToStream(outStream, FileFormat.PDF);
                    return outStream.ToArray();
                }
            }
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
        private string TransformXmlToHtml(string xmlContent, string xsltPath, XsltArgumentList args)
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
        private string SerializeInvoiceToXml(Invoice invoice)
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
        private string GetCommonName(string subject)
        {
            if (string.IsNullOrEmpty(subject)) return null;
            var parts = subject.Split(',');
            foreach (var part in parts)
            {
                if (part.Trim().StartsWith("CN="))
                    return part.Trim().Substring(3);
            }
            return subject;
        }
    }
}