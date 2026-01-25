using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard.Sale
{
    public class SalesDashboardDto
    {
        public SalesCurrentUserDto CurrentUser { get; set; } = new();
        public SalesKpiDto SalesKPIs { get; set; } = new();
        public InvoiceRequestStatsDto InvoiceRequestStats { get; set; } = new();
        public List<SalesTrendDto> SalesTrend { get; set; } = new();
        public List<DebtWatchlistDto> DebtWatchlist { get; set; } = new();
        public TotalCustomerDebtDto TotalCustomerDebt { get; set; } = new();
    }
}