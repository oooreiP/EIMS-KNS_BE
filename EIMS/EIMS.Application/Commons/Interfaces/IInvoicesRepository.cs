using EIMS.Application.DTOs.Dashboard;
using EIMS.Application.DTOs.Dashboard.Accountant;
using EIMS.Application.DTOs.Dashboard.HOD;
using EIMS.Application.DTOs.Dashboard.Sale;
using EIMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IInvoicesRepository : IBaseRepository<Invoice>
    {
        Task<Invoice> CreateInvoiceAsync(Invoice invoice);
        public IQueryable<Invoice> ApplySorting(IQueryable<Invoice> query, string? column, string? direction);
        Task<CustomerDashboardDto> GetCustomerDashboardStatsAsync(int customerId);
        Task<AdminDashboardDto> GetAdminDashboardStatsAsync(string? period, CancellationToken cancellationToken);
        Task<SalesDashboardDto> GetSalesDashboardStatsAsync(int salesPersonId, CancellationToken cancellationToken);
        Task<HodDashboardDto> GetHodDashboardStatsAsync(CancellationToken cancellationToken);
        Task<AccountantDashboardDto> GetAccountantDashboardAsync(int userId, CancellationToken cancellationToken);
    }
}
