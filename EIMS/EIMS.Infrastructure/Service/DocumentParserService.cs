using UglyToad.PdfPig;
using DocumentFormat.OpenXml.Packaging;
using System.Xml.Linq;
using EIMS.Application.Commons.Interfaces;

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
    }
}
