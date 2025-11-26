using EIMS.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IInvoicesRepository InvoicesRepository { get; }
        IBaseRepository<Category> CategoryRepository { get; }
        IBaseRepository<InvoiceHistory> InvoiceHistoryRepository { get; }
        IBaseRepository<TaxMessageCode> TaxMessageCodeRepository { get; }
        IBaseRepository<TaxApiLog> TaxApiLogRepository { get; }
        IBaseRepository<TaxApiStatus> TaxApiStatusRepository { get; }
        Task SaveChanges();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
