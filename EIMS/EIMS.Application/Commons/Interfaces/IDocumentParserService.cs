using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IDocumentParserService
    {
        Task<string> ConvertPdfToXmlAsync(Stream pdfStream);
        Task<string> ConvertDocxToXmlAsync(Stream docxStream);
        string TransformXmlToHtml(string xmlContent, string xsltPath);
    }
}
