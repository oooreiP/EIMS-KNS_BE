using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.CQT.Queries
{
    public class GetErrorNotificationPdfHandler : IRequestHandler<GetErrorNotificationPdfQuery, Result<byte[]>>
    {
        private readonly IPdfService _pdfService;

        public GetErrorNotificationPdfHandler(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        public async Task<Result<byte[]>> Handle(GetErrorNotificationPdfQuery request, CancellationToken cancellationToken)
        {
            try
            {
                string rootPath = AppDomain.CurrentDomain.BaseDirectory;
                byte[] pdfBytes = await _pdfService.ConvertNotificationToPdfAsync(request.NotificationId, rootPath);

                return Result.Ok(pdfBytes);
            }
            catch (Exception ex)
            {
                return Result.Fail($"Lỗi khi tạo PDF: {ex.Message}");
            }
        }
    }
}

