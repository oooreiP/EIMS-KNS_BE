using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.TaxAPIDTO;
using EIMS.Application.Features.TaxLogs.Commands;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
         IRequestHandler<DeleteTaxLogCommand, Result>
    {
        private readonly IUnitOfWork _uow;

        public TaxLogHandlers(IUnitOfWork uow)
        {
            _uow = uow;
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
                Timestamp = x.Timestamp,
                TaxApiStatusName = x.TaxApiStatus?.StatusName ?? "Unknown",
                MTDiep = x.MTDiep,
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
                InvoiceNumber = x.Invoice != null ? x.Invoice.InvoiceNumber.ToString("D7") : "N/A",
                Timestamp = x.Timestamp,
                MTDiep = x.MTDiep,
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
    }
}
