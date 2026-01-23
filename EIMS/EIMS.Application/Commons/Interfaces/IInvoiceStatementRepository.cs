using EIMS.Domain.Entities;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IInvoiceStatementRepository : IBaseRepository<InvoiceStatement>
    {
        Task<InvoiceStatement?> GetByIdWithInvoicesAsync(int id);
        Task<List<InvoiceStatement>> GetStatementsContainingInvoiceAsync(int invoiceId);
        Task<List<InvoiceStatement>> GetUnpaidStatementsInMonthAsync(int month, int year);
        Task<bool> IsDuplicated(int customerId, int month, int year);
    }
}