using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Files.Commands
{
    public class ConvertHtmlToPdfHandler : IRequestHandler<ConvertHtmlToPdfCommand, Result<byte[]>>
    {
        private readonly IPdfService _pdfService;

        public ConvertHtmlToPdfHandler(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        public async Task<Result<byte[]>> Handle(ConvertHtmlToPdfCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.HtmlContent))
                    return Result.Fail<byte[]>("Nội dung HTML không được để trống.");
                var pdfBytes = await _pdfService.ConvertHtmlToPdfAsync(request.HtmlContent);
                return Result.Ok(pdfBytes);
            }
            catch (Exception ex)
            {
                return Result.Fail<byte[]>($"Lỗi tạo PDF: {ex.Message}");
            }
        }
    }
}
