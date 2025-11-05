using EIMS.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IInvoicesRepository InvoicesRepository { get; }
        ISerialRepository SerialRepository { get; }
        IBaseRepository<Category> CategoryRepository { get; }
        IBaseRepository<InvoiceTemplate> InvoiceTemplateRepository { get; }

        Task SaveChanges();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
