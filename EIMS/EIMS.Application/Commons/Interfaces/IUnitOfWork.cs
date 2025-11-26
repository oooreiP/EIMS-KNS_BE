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
        IInvoiceTemplateRepository InvoiceTemplateRepository { get; }
        IBaseRepository<Prefix> PrefixRepository { get; }
        IBaseRepository<InvoiceType> InvoiceTypeRepository { get; }
        IBaseRepository<SerialStatus> SerialStatusRepository { get; }
        IInvoiceStatementRepository InvoiceStatementRepository { get; }
        IUserRepository UserRepository { get; }
        ITemplateFrameRepository TemplateFrameRepository { get; }


        Task SaveChanges();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
