using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Queries
{
    public class InvoiceViewHandlers :
    IRequestHandler<GetInvoiceHtmlViewQuery, Result<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IInvoiceXMLService _invoiceXmlService; // Service tải file/đọc file
        private readonly IDocumentParserService _documentParserService; // Service convert XML->HTML
        public InvoiceViewHandlers(
            IUnitOfWork uow,
            IInvoiceXMLService invoiceXmlService,
            IDocumentParserService documentParserService)
        {
            _uow = uow;
            _invoiceXmlService = invoiceXmlService;
            _documentParserService = documentParserService;
        }

        public async Task<Result<string>> Handle(GetInvoiceHtmlViewQuery request, CancellationToken cancellationToken)
        {
            // 1. Lấy thông tin hóa đơn từ DB
            var invoice = await _uow.InvoicesRepository.GetByIdAsync(request.InvoiceId);
            if (invoice == null)
                return Result.Fail("Không tìm thấy hóa đơn.");

            if (string.IsNullOrEmpty(invoice.XMLPath))
                return Result.Fail("Hóa đơn chưa có file XML.");

            try
            {
                // 2. Tải nội dung XML (Dùng hàm DownloadStringAsync bạn đã viết ở bài trước)
                string xmlContent = await _invoiceXmlService.DownloadStringAsync(invoice.XMLPath);

                // 3. Convert sang HTML
                string html = ConvertToHtml(xmlContent,request.RootPath);
                return Result.Ok(html);
            }
            catch (Exception ex)
            {
                return Result.Fail($"Lỗi khi đọc file XML: {ex.Message}");
            }
        }
        private string ConvertToHtml(string xmlContent, string rootPath)
        {
            string xsltPath = Path.Combine(rootPath, "Templates", "InvoiceTemplate.xsl");
            return _documentParserService.TransformXmlToHtml(xmlContent, xsltPath);
        }
    }
}