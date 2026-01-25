using EIMS.Application.Commons.Helpers;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Mails;
using EIMS.Application.Features.InvoiceStatements.Queries;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Minutes.Queries
{
    public class GetMinutePdfQueryHandler : IRequestHandler<GetMinutePdfQuery, Result<FileAttachment>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IInvoiceXMLService _invoiceXMLService;

        public GetMinutePdfQueryHandler(IUnitOfWork uow, IInvoiceXMLService invoiceXMLService)
        {
            _uow = uow;
            _invoiceXMLService = invoiceXMLService;

        }

        public async Task<Result<FileAttachment>> Handle(GetMinutePdfQuery request, CancellationToken cancellationToken)
        {
            var entity = await _uow.MinuteInvoiceRepository.GetByIdAsync(request.MinuteId);

            if (entity == null)
                return Result.Fail("Biên bản không tồn tại.");

            try
            {
                var pdfBytes = await _invoiceXMLService.DownloadBytesAsync(entity.FilePath);

                return Result.Ok(new FileAttachment
                {
                    FileContent = pdfBytes,
                    FileName = $"Bien_ban_{entity.MinuteCode}.pdf",
                });
            }
            catch (Exception ex)
            {
                return Result.Fail($"Lỗi tạo PDF: {ex.Message}");
            }
        }
    }
}
