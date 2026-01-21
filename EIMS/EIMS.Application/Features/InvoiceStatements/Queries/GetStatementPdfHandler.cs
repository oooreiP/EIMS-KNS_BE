using EIMS.Application.Commons.Helpers;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Mails;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceStatements.Queries
{
    public class GetStatementPdfHandler : IRequestHandler<GetStatementPdfQuery, Result<FileAttachment>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IStatementService _statementService;
        private readonly IInvoiceXMLService _invoiceXMLService;
        private readonly IPdfService _pdfService;
        private const string XsltContent = @"... COPY NỘI DUNG XSL CỦA BẠN VÀO ĐÂY HOẶC LOAD TỪ FILE ...";

        public GetStatementPdfHandler(IUnitOfWork uow, IStatementService statementService, IPdfService pdfService, IInvoiceXMLService invoiceXMLService)
        {
            _uow = uow;
            _statementService = statementService;
            _pdfService = pdfService;
            _invoiceXMLService = invoiceXMLService;
        }

        public async Task<Result<FileAttachment>> Handle(GetStatementPdfQuery request, CancellationToken cancellationToken)
        {
            var entity = await _uow.InvoiceStatementRepository.GetAllQueryable()
                .AsNoTracking()
                .Include(s => s.Customer)
                .Include(s => s.Creator)
                .Include(s => s.StatementDetails)
                    .ThenInclude(d => d.Invoice)
                        .ThenInclude(i => i.InvoiceItems) // 2. Vào tiếp danh sách Hàng hóa (InvoiceItems)
                         .ThenInclude(it => it.Product)
                .FirstOrDefaultAsync(s => s.StatementID == request.StatementId, cancellationToken);

            if (entity == null)
                return Result.Fail("Bảng kê không tồn tại.");

            try
            {
                var certResult = await _invoiceXMLService.GetCertificateAsync(1);
                if (certResult.IsFailed)
                    return Result.Fail(certResult.Errors);
                var signingCert = certResult.Value;
                var paymentDto = await _statementService.GetPaymentRequestXmlAsync(entity);
                string unsignedXmlString = XmlHelpers.Serialize(paymentDto);
                var signedResult = XmlHelpers.SignElectronicDocument(unsignedXmlString, signingCert, true);
                string xmlString = XmlHelpers.Serialize(paymentDto);
                string signedXmlContent = signedResult.SignedXml;
                string fullPath = Path.Combine(request.RootPath, "Templates", "PaymentTemplate.xsl");
                string htmlContent = _pdfService.TransformXmlToHtml(signedXmlContent, fullPath);
                byte[] pdfBytes = await _pdfService.ConvertHtmlToPdfAsync(htmlContent);

                return Result.Ok(new FileAttachment
                {
                    FileContent = pdfBytes,
                    FileName = $"Bang_ke_{entity.StatementCode}.pdf",
                });
            }
            catch (Exception ex)
            {
                return Result.Fail($"Lỗi tạo PDF: {ex.Message}");
            }
        }
    }
}
