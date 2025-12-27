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
        private readonly IPdfService _pdfService; 
        
        public InvoiceViewHandlers(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        public async Task<Result<string>> Handle(GetInvoiceHtmlViewQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Gọi hàm tạo HTML chúng ta vừa tách ra
                string html = await _pdfService.GenerateInvoiceHtmlAsync(request.InvoiceId, request.RootPath);

                return Result.Ok(html);
            }
            catch (Exception ex)
            {
                return Result.Fail($"Lỗi hiển thị hóa đơn: {ex.Message}");
            }
        }
    }
}