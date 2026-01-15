using UglyToad.PdfPig;
using DocumentFormat.OpenXml.Packaging;
using System.Xml.Linq;
using EIMS.Application.Commons.Interfaces;
using System.Xml.Xsl;
using System.Xml;
using Spire.Doc;

namespace EIMS.Infrastructure.Service
{
    public class DocumentParserService : IDocumentParserService
    {
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
            return await Task.Run(() =>
            {
                try
                {
                    using (var inputStream = new MemoryStream(docxBytes))
                    {
                        Document document = new Document();
                        document.LoadFromStream(inputStream, FileFormat.Docx);
                        using (var outputStream = new MemoryStream())
                        {
                            document.SaveToStream(outputStream, FileFormat.PDF);
                            return outputStream.ToArray();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi convert DOCX sang PDF: {ex.Message}");
                }
            });
        }
    }
}
