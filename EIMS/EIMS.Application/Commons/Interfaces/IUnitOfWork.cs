using EIMS.Domain.Entities;
using EIMS.Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IInvoicesRepository InvoicesRepository { get; }
        IBaseRepository<Category> CategoryRepository { get; }
        Task SaveChanges();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
