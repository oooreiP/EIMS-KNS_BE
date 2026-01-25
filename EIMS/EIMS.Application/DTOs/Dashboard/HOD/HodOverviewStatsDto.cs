namespace EIMS.Application.DTOs.Dashboard.HOD
{
    public class HodOverviewStatsDto
    {
        public decimal TotalMonthlyRevenue { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalInvoiceRequests { get; set; }
        public int TotalProducts { get; set; }
        public int TotalInvoicesIssued { get; set; }
        public int TotalInvoicesPendingApproval { get; set; }
        public decimal TotalDebtAll { get; set; }
    }
}
