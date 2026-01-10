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
        IBaseRepository<InvoiceItem> InvoiceItemRepository { get; }
        IBaseRepository<SerialStatus> SerialStatusRepository { get; }
        IBaseRepository<EmailTemplate> EmailTemplateRepository { get; }
        IBaseRepository<AuditLog> AuditLogRepository { get; }
        IInvoiceStatementRepository InvoiceStatementRepository { get; }
        IUserRepository UserRepository { get; }
        ITemplateFrameRepository TemplateFrameRepository { get; }
        ICompanyRepository CompanyRepository { get; }
        IInvoicePaymentRepository InvoicePaymentRepository { get; } 

        IBaseRepository<InvoiceHistory> InvoiceHistoryRepository { get; }
        IBaseRepository<SystemActivityLog> SystemActivityLogRepository { get; }
        IBaseRepository<TaxMessageCode> TaxMessageCodeRepository { get; }
        IBaseRepository<TaxApiLog> TaxApiLogRepository { get; }
        IBaseRepository<TaxApiStatus> TaxApiStatusRepository { get; }
        IBaseRepository<InvoiceStatus> InvoiceStatusRepository { get; }
        IBaseRepository<Notification> NotificationRepository { get; }
        IBaseRepository<NotificationStatus> NotificationStatusRepository { get; }
        IBaseRepository<NotificationType> NotificationTypeRepository { get; }
        IBaseRepository<InvoiceErrorNotification> ErrorNotificationRepository { get; }
        IBaseRepository<InvoiceErrorDetail> InvoiceErrorDetailRepository { get; }
        Task SaveChanges();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
