using EIMS.Application.Commons.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceTemplate.Queries
{
    public class GetInvoiceTemplatePreviewHandler : IRequestHandler<GetInvoiceTemplatePreviewQuery, string>
    {
        private readonly IPdfService _invoiceService;
        public GetInvoiceTemplatePreviewHandler(IPdfService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public async Task<string> Handle(GetInvoiceTemplatePreviewQuery request, CancellationToken cancellationToken)
        {
            return await _invoiceService.GetBlankInvoicePreviewAsync(
                request.TemplateID,
                request.CompanyID ?? 1,
                request.RootPath
            );
        }
    }
}
