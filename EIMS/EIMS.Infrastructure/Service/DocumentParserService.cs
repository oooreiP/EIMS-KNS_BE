using UglyToad.PdfPig;
using DocumentFormat.OpenXml.Packaging;
using System.Xml.Linq;
using EIMS.Application.Commons.Interfaces;
using System.Xml.Xsl;
using System.Xml;
using Spire.Doc;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace EIMS.Infrastructure.Service
{
    public class DocumentParserService : IDocumentParserService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;
        public DocumentParserService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public async Task<string> ConvertPdfToXmlAsync(Stream pdfStream)
        {
            using var mem = new MemoryStream();
            await pdfStream.CopyToAsync(mem);
            mem.Position = 0;

            var doc = new XElement("Document");
            using (var pdf = PdfDocument.Open(mem))
            {
                foreach (var page in pdf.GetPages())
                {
                    var pageElement = new XElement("Page",
                        new XAttribute("Number", page.Number),
                        new XElement("Text", page.Text));
                    doc.Add(pageElement);
                }
            }

            var path = Path.Combine(Path.GetTempPath(), $"pdf_{Guid.NewGuid():N}.xml");
            new XDocument(doc).Save(path);
            return path;
        }
        public async Task<string> ConvertDocxToXmlAsync(Stream docxStream)
        {
            using var mem = new MemoryStream();
            await docxStream.CopyToAsync(mem);
            mem.Position = 0;

            var doc = new XElement("Document");
            using (var wordDoc = WordprocessingDocument.Open(mem, false))
            {
                var body = wordDoc.MainDocumentPart?.Document.Body;
                if (body != null)
                {
                    var paras = body.Elements<DocumentFormat.OpenXml.Wordprocessing.Paragraph>()
                                    .Select(p => new XElement("Paragraph", p.InnerText));
                    doc.Add(new XElement("Paragraphs", paras));
                }
            }

            var path = Path.Combine(Path.GetTempPath(), $"docx_{Guid.NewGuid():N}.xml");
            new XDocument(doc).Save(path);
            return path;
        }
        public string TransformXmlToHtml(string xmlContent, string xsltPath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(xmlContent)) return "<h3>Không có dữ liệu XML</h3>";
                if (!File.Exists(xsltPath)) throw new FileNotFoundException("Không tìm thấy file mẫu XSLT");

                // 1. Load XSLT
                var transform = new XslCompiledTransform();
                transform.Load(xsltPath);

                // 2. Chuẩn bị XML Reader
                using (var stringReader = new StringReader(xmlContent))
                using (var xmlReader = XmlReader.Create(stringReader))
                using (var stringWriter = new StringWriter())
                {
                    // 3. Transform
                    transform.Transform(xmlReader, null, stringWriter);
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                return $"<div style='color:red'><h3>Lỗi hiển thị hóa đơn:</h3><pre>{ex.Message}</pre></div>";
            }
        }
        public async Task<byte[]> ConvertDocxToPdfAsync(byte[] docxBytes)
        {
            string baseUrl = _configuration["Gotenberg:Url"] ?? "http://localhost:3000";
            string apiUrl = $"{baseUrl}/forms/libreoffice/convert";

            using (var client = _clientFactory.CreateClient())
            using (var content = new MultipartFormDataContent())
            using (var fileStream = new MemoryStream(docxBytes))
            {
                var fileContent = new StreamContent(fileStream);
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/vnd.openxmlformats-officedocument.wordprocessingml.document");

                content.Add(fileContent, "files", "document.docx");

                // Gọi API
                var response = await client.PostAsync(apiUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Lỗi convert Gotenberg: {response.StatusCode}");
                }

                return await response.Content.ReadAsByteArrayAsync();
            }
        }
    }
}
