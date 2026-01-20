using EIMS.Application.Commons.Helpers;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Mails;
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
        private readonly IInvoiceXMLService _xMLService;
        private readonly IPdfService _pdfService;
        private const string XsltContent = @"... COPY NỘI DUNG XSL CỦA BẠN VÀO ĐÂY HOẶC LOAD TỪ FILE ...";

        public GetStatementPdfHandler(IUnitOfWork uow, IInvoiceXMLService xMLService, IPdfService pdfService)
        {
            _uow = uow;
            _xMLService = xMLService;
            _pdfService = pdfService;
        }

        public async Task<Result<FileAttachment>> Handle(GetStatementPdfQuery request, CancellationToken cancellationToken)
        {
            var entity = await _uow.InvoiceStatementRepository.GetAllQueryable()
                .AsNoTracking()
                .Include(s => s.Customer)
                .Include(s => s.Creator)
                .Include(s => s.StatementDetails)
                    .ThenInclude(d => d.Invoice)
                .FirstOrDefaultAsync(s => s.StatementID == request.StatementId, cancellationToken);

            if (entity == null)
                return Result.Fail("Bảng kê không tồn tại.");

            try
            {
                var paymentDto = await _xMLService.GetPaymentRequestXmlAsync(entity);
                string xmlString = XmlHelpers.Serialize(paymentDto);
                string fullPath = Path.Combine(request.RootPath, "Templates", "PaymentTemplate.xsl");
                string htmlContent = _pdfService.TransformXmlToHtml(xmlString, fullPath);
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
