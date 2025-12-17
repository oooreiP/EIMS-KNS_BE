using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.TaxLogs
{
    public class GetLogHtmlViewQuery : IRequest<Result<string>>
    {
        /// <summary>
        /// ID của dòng Log trong database
        /// </summary>
        public int LogId { get; set; }

        /// <summary>
        /// Loại dữ liệu muốn xem: "request" (Gửi đi) hoặc "response" (Nhận về)
        /// </summary>
        public string ViewType { get; set; }

        /// <summary>
        /// true: Xem dạng giao diện hóa đơn (HTML)
        /// false: Xem dạng code XML gốc (Raw Data)
        /// </summary>
        public bool ViewByHtml { get; set; }
        public string RootPath { get; set; }
        public GetLogHtmlViewQuery(int logId, string viewType, bool viewByHtml, string rootPath)
        {
            LogId = logId;
            ViewType = viewType?.ToLower() ?? "request";
            ViewByHtml = viewByHtml;
            RootPath = rootPath;
        }
    }
}
