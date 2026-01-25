using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.Customer;
using EIMS.Application.DTOs.TaxAPIDTO;
using EIMS.Application.Features.TaxLogs.Commands;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EIMS.Application.Features.TaxLogs
{
    public class TaxLogHandlers :
         IRequestHandler<GetTaxLogsByInvoiceQuery, Result<List<TaxApiLogSummaryDto>>>,
         IRequestHandler<GetTaxLogByIdQuery, Result<TaxApiLogDetailDto>>,
         IRequestHandler<CreateTaxLogCommand, Result<int>>,
         IRequestHandler<DeleteTaxLogCommand, Result>,
         IRequestHandler<GetLogHtmlViewQuery, Result<string>>,
         IRequestHandler<GetTaxApiLogListQuery, Result<PaginatedList<TaxApiLogSummaryDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IDocumentParserService _documentParserService;
        public TaxLogHandlers(IUnitOfWork uow, IDocumentParserService documentParserService)
        {
            _uow = uow;
            _documentParserService = documentParserService;
        }
        public async Task<Result<PaginatedList<TaxApiLogSummaryDto>>> Handle(GetTaxApiLogListQuery request, CancellationToken cancellationToken)
        {
            var query = _uow.TaxApiLogRepository.GetAllQueryable().AsNoTracking();
            query = query         
                .Include(l => l.TaxApiStatus)
                .Include(l => l.Invoice);
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                string term = request.SearchTerm.ToLower().Trim();
                query = query.Where(c =>
                    c.SoTBao.ToLower().Contains(term) ||
                    c.MTDiep.ToLower().Contains(term)
                );
            }

            query = query.OrderByDescending(l => l.Timestamp);
            var dtoList = query.Select(l => new TaxApiLogSummaryDto
            {
                TaxLogID = l.TaxLogID,
                InvoiceID = l.InvoiceID,
                InvoiceNumber = l.Invoice.InvoiceNumber,
                TaxApiStatusID = l.TaxApiStatusID,
                TaxApiStatusName = l.TaxApiStatus.StatusName,
                MTDiep = l.MTDiep,
                MTDiepPhanHoi = l.MTDiepPhanHoi,
                MCCQT = l.MCCQT,
                SoTBao = l.SoTBao,
                Timestamp = l.Timestamp
            });
            var paginatedList = await PaginatedList<TaxApiLogSummaryDto>.CreateAsync(
               dtoList,
               request.PageNumber,
               request.PageSize
           );
            return Result.Ok(paginatedList);
        }
        public async Task<Result<List<TaxApiLogSummaryDto>>> Handle(GetTaxLogsByInvoiceQuery request, CancellationToken cancellationToken)
        {
            var logs = await _uow.TaxApiLogRepository.GetAllAsync(
                filter: x => x.InvoiceID == request.InvoiceId,
                includeProperties: "TaxApiStatus"
            );

            var dtos = logs.Select(x => new TaxApiLogSummaryDto
            {
                TaxLogID = x.TaxLogID,
                InvoiceID = x.InvoiceID,
                TaxApiStatusID = x.TaxApiStatusID,
                Timestamp = x.Timestamp,
                TaxApiStatusName = x.TaxApiStatus?.StatusName ?? "Unknown",
                MTDiep = x.MTDiep,
                MTDiepPhanHoi = x.MTDiepPhanHoi,
                MCCQT = x.MCCQT,
                SoTBao = x.SoTBao
            }).ToList();

            return Result.Ok(dtos);
        }
        public async Task<Result<TaxApiLogDetailDto>> Handle(GetTaxLogByIdQuery request, CancellationToken cancellationToken)
        {
            var logs = await _uow.TaxApiLogRepository.GetAllAsync(
         filter: x => x.TaxLogID == request.TaxLogID,
         includeProperties: "TaxApiStatus");
            var log = logs.FirstOrDefault();
            if (log == null) return Result.Fail("Log not found");
            // Format XML cho đẹp trước khi trả về
            string formattedRequest = TryFormatXml(log.RequestPayload);
            string formattedResponse = TryFormatXml(log.ResponsePayload);

            var dto = new TaxApiLogDetailDto
            {
                TaxLogID = log.TaxLogID,
                Timestamp = log.Timestamp,
                TaxApiStatusName = log.TaxApiStatus?.StatusName ?? "Unknown",
                MTDiep = log.MTDiep,
                MTDiepPhanHoi = log.MTDiepPhanHoi,
                MCCQT = log.MCCQT,
                SoTBao = log.SoTBao,
                RequestPayload = formattedRequest,
                ResponsePayload = formattedResponse
            };

            return Result.Ok(dto);
        }
        public async Task<Result<int>> Handle(CreateTaxLogCommand request, CancellationToken cancellationToken)
        {
            var log = new TaxApiLog
            {
                InvoiceID = request.InvoiceID,
                RequestPayload = request.RequestPayload,
                ResponsePayload = request.ResponsePayload,
                TaxApiStatusID = request.TaxApiStatusID,
                Timestamp = DateTime.UtcNow               
            };

            await _uow.TaxApiLogRepository.CreateAsync(log);
            await _uow.SaveChanges();
            return Result.Ok(log.TaxLogID);
        }
        public async Task<Result<List<TaxApiLogSummaryDto>>> Handle(GetAllTaxLogsQuery request, CancellationToken cancellationToken)
        {

            var logs = await _uow.TaxApiLogRepository.GetAllAsync(
                filter: x => (!request.FromDate.HasValue || x.Timestamp >= request.FromDate) &&
                             (!request.ToDate.HasValue || x.Timestamp <= request.ToDate),
                includeProperties: "Invoice,TaxApiStatus",
                orderBy: q => q.OrderByDescending(x => x.Timestamp)
            );

            // 2. Map sang DTO
            var dtos = logs.Select(x => new TaxApiLogSummaryDto
            {
                TaxLogID = x.TaxLogID,
                InvoiceID = x.InvoiceID,
                InvoiceNumber = x.Invoice != null ? x.Invoice.InvoiceNumber : 0,
                Timestamp = x.Timestamp,
                MTDiep = x.MTDiep,
                MTDiepPhanHoi = x.MTDiepPhanHoi,
                SoTBao = x.SoTBao,
                TaxApiStatusID = x.TaxApiStatusID,
                TaxApiStatusName = x.TaxApiStatus?.StatusName ?? "Unknown",
            }).ToList();

            return Result.Ok(dtos);
        }
        // --- 4. DELETE ---
        public async Task<Result> Handle(DeleteTaxLogCommand request, CancellationToken cancellationToken)
        {
            var log = await _uow.TaxApiLogRepository.GetByIdAsync(request.Id);
            if (log == null) return Result.Fail("Log not found");

            await _uow.TaxApiLogRepository.DeleteAsync(log);
            await _uow.SaveChanges();
            return Result.Ok();
        }
        public async Task<Result<string>> Handle(GetLogHtmlViewQuery request, CancellationToken cancellationToken)

        {

            // 1. LẤY DỮ LIỆU LOG TỪ DB

            var log = await _uow.TaxApiLogRepository.GetByIdAsync(request.LogId);

            if (log == null)

                return Result.Fail("Không tìm thấy lịch sử truyền nhận (Log) này.");



            // 2. CHỌN NỘI DUNG (Gửi đi hay Nhận về)

            string content = (request.ViewType == "response") ? log.ResponsePayload : log.RequestPayload;

            string title = (request.ViewType == "response") ? "Phản hồi từ CQT (Response)" : "Dữ liệu gửi đi (Request)";



            if (string.IsNullOrWhiteSpace(content))

                return Result.Fail("Log này không có dữ liệu.");

            if (!request.ViewByHtml)

            {

                return Result.Ok(content);

            }

            if (!content.TrimStart().StartsWith("<"))

            {

                return Result.Ok(GenerateRawViewHtml(title, "Dữ liệu không phải XML (JSON/Text)", content));

            }



            try

            {

                // Bước 4: Tự động phát hiện loại dữ liệu để chọn XSLT phù hợp

                string xsltFileName = DetectTemplate(content);



                // Nếu không tìm thấy template phù hợp (XML lạ), hiển thị Raw

                if (string.IsNullOrEmpty(xsltFileName))

                {

                    return Result.Ok(GenerateRawViewHtml(title, "Dữ liệu XML chưa được hỗ trợ giao diện", content));

                }



                // Bước 5: Lấy đường dẫn file XSLT

                string xsltPath = Path.Combine(request.RootPath, "Templates", xsltFileName);



                if (!File.Exists(xsltPath))

                    return Result.Ok(GenerateErrorHtml(title, $"Không tìm thấy file mẫu tại: {xsltPath}", content));

                // Bước 6: Transform XML -> HTML

                string htmlOutput = _documentParserService.TransformXmlToHtml(content, xsltPath);

                return Result.Ok(htmlOutput);

            }

            catch (Exception ex)

            {

                // Nếu transform lỗi (do XML sai cấu trúc, thiếu thẻ đóng...), hiển thị XML gốc để debug

                return Result.Ok(GenerateErrorHtml(title, $"Lỗi hiển thị: {ex.Message}", content));

            }

        }
        // --- HELPER: Format XML ---
        private string TryFormatXml(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml)) return "";
            try
            {
                var doc = XDocument.Parse(xml);
                return doc.ToString(); 
            }
            catch
            {
                return xml; 
            }
        }
        // --- Hàm phụ trợ: Tự động chọn file XSLT ---
        private string DetectTemplate(string xmlContent)
        {
            if (xmlContent.Contains("DLHDon") || xmlContent.Contains("HDon"))
            {
                return "InvoiceTemplate.xsl"; // Xem hóa đơn
            }
            if (xmlContent.Contains("TBao") || xmlContent.Contains("DiepToBa"))
            {
                return "TaxNotificationTemplate.xsl"; 
            }
            return "InvoiceTemplate.xsl";
        }

        // --- Hàm phụ trợ: HTML hiển thị lỗi/text thô ---
        private string GenerateRawViewHtml(string title, string status, string content)
        {
            return $@"
                <html>
                <body style='font-family: Segoe UI, sans-serif; padding: 20px; background: #f9f9f9;'>
                    <div style='background: white; padding: 20px; border-radius: 8px; box-shadow: 0 2px 10px rgba(0,0,0,0.1);'>
                        <h2 style='color: #0056b3; margin-top: 0;'>{title}</h2>
                        <div style='background: #fff3cd; color: #856404; padding: 10px; border-radius: 4px; margin-bottom: 15px; border: 1px solid #ffeeba;'>
                            <strong>Trạng thái:</strong> {status}
                        </div>
                        <h4 style='margin-bottom: 5px; color: #555;'>Nội dung gốc:</h4>
                        <pre style='background: #2d2d2d; color: #f8f8f2; padding: 15px; border-radius: 5px; overflow-x: auto; white-space: pre-wrap; font-family: Consolas, monospace;'>{System.Net.WebUtility.HtmlEncode(content)}</pre>
                    </div>
                </body>
                </html>";
        }

        private string GenerateErrorHtml(string title, string error, string rawXml)
        {
            return $@"
                <html>
                <body style='font-family: Segoe UI, sans-serif; padding: 20px; background: #fff5f5;'>
                    <div style='background: white; padding: 20px; border-radius: 8px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); border-left: 5px solid #dc3545;'>
                        <h2 style='color: #dc3545; margin-top: 0;'>⚠️ {title}</h2>
                        <p style='color: #721c24; font-weight: bold;'>Lỗi: {error}</p>
                        <hr style='border: 0; border-top: 1px solid #eee; margin: 20px 0;'/>
                        <h4 style='margin-bottom: 5px;'>Dữ liệu XML gốc:</h4>
                        <textarea style='width: 100%; height: 400px; font-family: Consolas, monospace; border: 1px solid #ccc; padding: 10px;'>{rawXml}</textarea>
                    </div>
                </body>
                </html>";
        }
    }
}
