using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Files.Commands
{
    public class SignPdfCommandHandler : IRequestHandler<SignPdfCommand, Result<byte[]>>
    {
        private readonly IPdfService  _pdfService;
        private readonly IInvoiceXMLService _invoiceXmlService;

        public SignPdfCommandHandler(IPdfService pdfService, IInvoiceXMLService invoiceXmlService)
        {
            _pdfService = pdfService;
            _invoiceXmlService = invoiceXmlService;
        }

        public async Task<Result<byte[]>> Handle(SignPdfCommand request, CancellationToken cancellationToken)
        {
            var certResult = await _invoiceXmlService.GetCertificateAsync(1);
            if (certResult.IsFailed)
                return Result.Fail(certResult.Errors);
            var signingCert = certResult.Value;
            var result = _pdfService.SignPdfAtText(request.PdfBytes, signingCert, request.SearchText);

            return await Task.FromResult(result);
        }
    }
}
