using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDBContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceHistory> InvoiceHistories { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<InvoiceStatement> InvoiceStatements { get; set; }
        public DbSet<InvoiceStatus> InvoiceStatuses { get; set; }
        public DbSet<InvoiceTemplate> InvoiceTemplates { get; set; }
        public DbSet<InvoiceType> InvoiceTypes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationStatus> NotificationStatuses { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<Prefix> Prefixes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Serial> Serials { get; set; }
        public DbSet<SerialStatus> SerialStatuses { get; set; }
        public DbSet<StatementStatus> StatementStatuses { get; set; }
        public DbSet<TaxApiLog> TaxApiLogs { get; set; }
        public DbSet<TaxApiStatus> TaxApiStatuses { get; set; }
        public DbSet<TemplateType> TemplateTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleID = 1, RoleName = "Admin" },
                new Role { RoleID = 2, RoleName = "Accountant" },
                new Role { RoleID = 3, RoleName = "Sale" },
                new Role { RoleID = 4, RoleName = "HOD" }, // Head of Department
                new Role { RoleID = 5, RoleName = "Customer" }
            );
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}