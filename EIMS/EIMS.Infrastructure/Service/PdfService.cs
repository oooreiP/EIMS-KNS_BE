using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace EIMS.Infrastructure.Service
{
    public class PdfService : IPdfService
    {
        public async Task<byte[]> ConvertHtmlToPdfAsync(string htmlContent)
        {
            // --- FIX: Remove 'using' here ---
            var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync(); // Just call the method

            // Browser DOES implement IAsyncDisposable, so 'await using' is correct here
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Args = new[] { "--no-sandbox" } // Recommended for Docker environments
            });

            await using var page = await browser.NewPageAsync();
            await page.SetContentAsync(htmlContent);

            // Generate PDF with A4 settings
            var pdfData = await page.PdfDataAsync(new PdfOptions
            {
                Format = PaperFormat.A4,
                PrintBackground = true,
                MarginOptions = new MarginOptions
                {
                    Top = "10mm",
                    Bottom = "10mm",
                    Left = "10mm",
                    Right = "10mm"
                }
            });

            return pdfData;
        }
    }
}