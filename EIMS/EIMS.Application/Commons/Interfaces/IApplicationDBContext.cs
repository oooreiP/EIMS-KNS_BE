using EIMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IApplicationDBContext
    {
        DbSet<AuditLog> AuditLogs { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Company> Companies { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Invoice> Invoices { get; set; }
        DbSet<InvoiceHistory> InvoiceHistories { get; set; }
        DbSet<InvoiceItem> InvoiceItems { get; set; }
        DbSet<InvoiceStatement> InvoiceStatements { get; set; }
        DbSet<InvoiceStatus> InvoiceStatuses { get; set; }
        DbSet<InvoiceTemplate> InvoiceTemplates { get; set; }
        DbSet<InvoiceType> InvoiceTypes { get; set; }
        DbSet<Notification> Notifications { get; set; }
        DbSet<NotificationStatus> NotificationStatuses { get; set; }
        DbSet<NotificationType> NotificationTypes { get; set; }
        DbSet<Prefix> Prefixes { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Serial> Serials { get; set; }
        DbSet<SerialStatus> SerialStatuses { get; set; }
        DbSet<StatementStatus> StatementStatuses { get; set; }
        DbSet<TaxApiLog> TaxApiLogs { get; set; }
        DbSet<TaxApiStatus> TaxApiStatuses { get; set; }
        DbSet<TemplateType> TemplateTypes { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}