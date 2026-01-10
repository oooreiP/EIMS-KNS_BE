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
    public class GetErrorNotificationPreviewHandler : IRequestHandler<GetErrorNotificationPreviewQuery, Result<string>>
    {
        private readonly IPdfService _pdfService;

        public GetErrorNotificationPreviewHandler(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        public async Task<Result<string>> Handle(GetErrorNotificationPreviewQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. Xác định đường dẫn gốc (Root Path) để tìm file Template .xslt
                // AppDomain.CurrentDomain.BaseDirectory trỏ đến thư mục bin/Debug/netX.0/
                string rootPath = AppDomain.CurrentDomain.BaseDirectory;

                // 2. Gọi Service để sinh HTML từ XML + XSLT
                string htmlContent = await _pdfService.GenerateNotificationHtmlAsync(request.NotificationId, rootPath);

                // 3. Trả về kết quả
                return Result.Ok(htmlContent);
            }
            catch (FileNotFoundException ex)
            {
                return Result.Fail($"Không tìm thấy file mẫu in (Template): {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                return Result.Fail($"Lỗi khi tạo bản xem trước: {ex.Message}");
            }
        }
    }
}
