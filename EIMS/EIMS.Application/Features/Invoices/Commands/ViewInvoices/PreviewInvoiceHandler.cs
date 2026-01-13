using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Commands.ViewInvoices
{
    public class PreviewInvoiceHandler : IRequestHandler<PreviewInvoiceCommand, Result<string>>
    {
        private readonly IPdfService _pdfService;

        public PreviewInvoiceHandler(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        public async Task<Result<string>> Handle(PreviewInvoiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string htmlContent = await _pdfService.PreviewInvoiceHtmlAsync(request, request.RootPath);
                return Result.Ok(htmlContent);
            }
            catch (Exception ex)
            {
                return Result.Fail(new Error($"Lỗi tạo bản xem trước: {ex.Message}")
                                   .WithMetadata("ErrorCode", "Invoice.Preview.Failed"));
            }
        }
    }
}
