using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard.HOD
{
    public class HodDashboardDto
    {
        public HodFinancialMetricsDto Financials { get; set; } = new();
        public List<CashFlowDto> CashFlow { get; set; } = new();
        public DebtAgingReportDto DebtAging { get; set; } = new();
        public List<PendingInvoiceDto> PendingInvoices { get; set; } = new();
        public DateTime GeneratedAt { get; set; }
        public string FiscalMonth { get; set; }
    }
}