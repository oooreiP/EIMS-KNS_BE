using EIMS.Infrastructure.Persistence;
using EIMS.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;

namespace EIMS.Application.Commons.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        public IProductRepository ProductRepository { get; set; }
        public IInvoicesRepository InvoicesRepository { get; set; }
        public ICustomerRepository CustomerRepository { get; set; }
        public ISerialRepository SerialRepository { get; set; }
        public IBaseRepository<Category> CategoryRepository { get; set; }
        public IBaseRepository<InvoiceHistory> InvoiceHistoryRepository { get; set; }
        public IBaseRepository<TaxMessageCode> TaxMessageCodeRepository { get; set; }
        public IBaseRepository<TaxApiLog> TaxApiLogRepository { get; set; }
        public IBaseRepository<TaxApiStatus> TaxApiStatusRepository { get; set; }
        public IInvoiceTemplateRepository InvoiceTemplateRepository { get; set; }
        public IBaseRepository<Prefix> PrefixRepository { get; set; }
        public IBaseRepository<InvoiceType> InvoiceTypeRepository { get; set; }
        public IBaseRepository<SerialStatus> SerialStatusRepository { get; set; }
        public IInvoiceStatementRepository InvoiceStatementRepository { get; private set; }
        public IUserRepository UserRepository { get; set; }
        public ITemplateFrameRepository TemplateFrameRepository { get; set; }

        public UnitOfWork(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
            ProductRepository = new ProductRepository(_db);
            InvoicesRepository = new InvoiceRepository(_db);
            CustomerRepository = new CustomerRepository(_db);
            SerialRepository = new SerialRepository(_db);
            CategoryRepository = new BaseRepository<Category>(_db);
            InvoiceHistoryRepository = new BaseRepository<InvoiceHistory>(_db);
            TaxMessageCodeRepository = new BaseRepository<TaxMessageCode>(_db);
            TaxApiLogRepository = new BaseRepository<TaxApiLog>(_db);
            TaxApiStatusRepository = new BaseRepository<TaxApiStatus>(_db);
            InvoiceTemplateRepository = new InvoiceTemplateRepository(_db);
            PrefixRepository = new BaseRepository<Prefix>(_db);
            InvoiceTypeRepository = new BaseRepository<InvoiceType>(_db);
            SerialStatusRepository = new BaseRepository<SerialStatus>(_db);
            InvoiceStatementRepository = new InvoiceStatementRepository(_db);
            UserRepository = new UserRepository(_db);
            TemplateFrameRepository = new TemplateFrameRepository(_db);
        }
        public async Task SaveChanges()
        {
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _db.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _db.Database.CurrentTransaction?.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await _db.Database.CurrentTransaction?.RollbackAsync();
        }
    }
}
