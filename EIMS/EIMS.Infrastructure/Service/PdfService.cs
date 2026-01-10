
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
            string xsltPath = Path.Combine(rootPath, "Templates", "InvoiceTemplate.xsl");
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
            string xsltPath = Path.Combine(rootPath, "Templates", "Form04SS.xsl");

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
            string companyName = company?.CompanyName ?? "TÊN CÔNG TY CHƯA CẬP NHẬT";
            args.AddParam("CompanyName", "", companyName);
            bool isDraft = noti.Status == 1;
            args.AddParam("IsDraft", "", isDraft ? "true" : "false");
            // 2. Có thể truyền thêm trạng thái Draft nếu muốn hiển thị watermark "NHÁP"
            // (Tùy chỉnh file XSLT để hứng tham số này)
            // bool isDraft = noti.Status == 0;
            // args.AddParam("IsDraft", "", isDraft ? "true" : "false");

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
    }
}