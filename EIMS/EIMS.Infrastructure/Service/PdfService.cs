
using System.Xml;
using System.Xml.Xsl;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.InvoiceTemplate;
using EIMS.Domain.Entities;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Infrastructure.Service
{
    public class PdfService : IPdfService
    {
        private readonly IUnitOfWork _uow;
        private readonly IInvoiceXMLService _xmlService; 
        public PdfService(
        IUnitOfWork uow,
        IInvoiceXMLService xmlService)
        {
            _uow = uow;
            _xmlService = xmlService;
        }

        private async Task<byte[]> GeneratePdfBytesAsync(string htmlContent)
        {
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Args = new[] { "--no-sandbox" }
            });
            await using var page = await browser.NewPageAsync();
            await page.SetContentAsync(htmlContent);
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
                // Cấu hình Footer (Puppeteer dùng HTML template cho footer)
                DisplayHeaderFooter = true,
                // Header để rỗng
                HeaderTemplate = "<div></div>",
                // Footer: class 'pageNumber' và 'totalPages' là biến của Puppeteer
                FooterTemplate = @"
                <div style='font-size: 9px; font-family: sans-serif; width: 100%; text-align: right; margin-right: 10mm;'>
                    Trang <span class='pageNumber'></span> / <span class='totalPages'></span>
                </div>",
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
            var style = config.Style ?? new StyleSettings();
            args.AddParam("ColorTheme", "", style.ColorTheme ?? "#0056b3");
            args.AddParam("FontFamily", "", style.FontFamily ?? "Times New Roman");
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