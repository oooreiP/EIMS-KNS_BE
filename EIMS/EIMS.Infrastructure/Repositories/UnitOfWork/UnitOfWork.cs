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
        public IBaseRepository<InvoiceRequest> InvoiceRequestRepository { get; set; }
        public IBaseRepository<InvoiceRequestItem> InvoiceRequestItemRepository { get; set; }
        public IBaseRepository<InvoiceRequestStatus> InvoiceRequestStatusRepository { get; set; }
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
        public IBaseRepository<InvoiceItem> InvoiceItemRepository { get; set; }
        public IBaseRepository<SerialStatus> SerialStatusRepository { get; set; }
        public IInvoiceStatementRepository InvoiceStatementRepository { get; private set; }
        public IUserRepository UserRepository { get; set; }
        public ITemplateFrameRepository TemplateFrameRepository { get; set; }
        public ICompanyRepository CompanyRepository { get; set; }
        public IBaseRepository<InvoiceStatus> InvoiceStatusRepository { get; set; }
        public IInvoicePaymentRepository InvoicePaymentRepository { get; private set; }
        public IBaseRepository<EmailTemplate> EmailTemplateRepository { get; set; }
        public IBaseRepository<SystemActivityLog> SystemActivityLogRepository { get; set; }
        public IBaseRepository<AuditLog> AuditLogRepository { get; set; }
        public IBaseRepository<NotificationType> NotificationTypeRepository { get; set; }
        public IBaseRepository<Notification> NotificationRepository { get; set; }
        public IBaseRepository<NotificationStatus> NotificationStatusRepository { get; set; }
        public IBaseRepository<InvoiceLookupLog> InvoiceLookupLogRepository { get; set; }
        public IErrorNotificationRepository ErrorNotificationRepository { get; set; }
        public IBaseRepository<InvoiceErrorDetail> InvoiceErrorDetailRepository { get; set; }
        public UnitOfWork(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
            InvoiceRequestRepository = new BaseRepository<InvoiceRequest>(_db);
            InvoiceRequestItemRepository = new BaseRepository<InvoiceRequestItem>(_db);
            InvoiceRequestStatusRepository = new BaseRepository<InvoiceRequestStatus>(_db);
            ProductRepository = new ProductRepository(_db);
            InvoicesRepository = new InvoiceRepository(_db, _configuration);
            CustomerRepository = new CustomerRepository(_db);
            SerialRepository = new SerialRepository(_db);
            CategoryRepository = new BaseRepository<Category>(_db);
            InvoiceHistoryRepository = new BaseRepository<InvoiceHistory>(_db);
            TaxMessageCodeRepository = new BaseRepository<TaxMessageCode>(_db);
            TaxApiLogRepository = new BaseRepository<TaxApiLog>(_db);
            TaxApiStatusRepository = new BaseRepository<TaxApiStatus>(_db);
            InvoiceStatusRepository = new BaseRepository<InvoiceStatus>(_db);
            InvoiceTemplateRepository = new InvoiceTemplateRepository(_db);
            PrefixRepository = new BaseRepository<Prefix>(_db);
            InvoiceTypeRepository = new BaseRepository<InvoiceType>(_db);
            SerialStatusRepository = new BaseRepository<SerialStatus>(_db);
            InvoiceStatementRepository = new InvoiceStatementRepository(_db);
            UserRepository = new UserRepository(_db);
            TemplateFrameRepository = new TemplateFrameRepository(_db);
            CompanyRepository = new CompanyRepository(_db);
            InvoicePaymentRepository = new InvoicePaymentRepository(_db);
            InvoiceItemRepository = new BaseRepository<InvoiceItem>(_db);
            EmailTemplateRepository = new BaseRepository<EmailTemplate>(_db);
            SystemActivityLogRepository = new BaseRepository<SystemActivityLog>(_db);
            AuditLogRepository = new BaseRepository<AuditLog>(_db);
            NotificationTypeRepository = new BaseRepository<NotificationType>(_db);
            NotificationRepository = new BaseRepository<Notification>(_db);
            NotificationStatusRepository = new BaseRepository<NotificationStatus>(_db);
            InvoiceLookupLogRepository = new BaseRepository<InvoiceLookupLog>(_db);         
            ErrorNotificationRepository = new ErrorNotificationRepository(_db);
            InvoiceErrorDetailRepository = new BaseRepository<InvoiceErrorDetail>(_db);
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
