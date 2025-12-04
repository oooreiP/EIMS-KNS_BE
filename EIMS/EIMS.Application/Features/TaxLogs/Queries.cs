using EIMS.Application.DTOs.TaxAPIDTO;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.TaxLogs
{
    public class GetTaxLogsByInvoiceQuery : IRequest<Result<List<TaxApiLogSummaryDto>>>
    {
        public int InvoiceId { get; set; }
    }
    public class GetTaxLogByIdQuery : IRequest<Result<TaxApiLogDetailDto>>
    {
        public int Id { get; set; }
    }
    public class GetAllTaxLogsQuery : IRequest<Result<List<TaxApiLogSummaryDto>>>
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
