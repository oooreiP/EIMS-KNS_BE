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
        public DbSet<Product> Products { get; set; }
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
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");

                entity.HasKey(p => p.ProductID);

                entity.Property(p => p.Code)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(p => p.Name)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(p => p.Unit)
                    .HasMaxLength(100);

                entity.Property(p => p.BasePrice)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(p => p.VATRate)
                    .HasColumnType("decimal(5,2)");

                entity.Property(p => p.Description)
                    .HasMaxLength(500);

                entity.Property(p => p.IsActive)
                    .HasDefaultValue(true);

                entity.Property(p => p.CreatedDate)
                    .HasDefaultValueSql("NOW()");

                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryID)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Category>().HasData(
       new Category
       {
           CategoryID = 1,
           Code = "HH",
           Name = "Hàng hóa chịu thuế 10%",
           Description = "Mặt hàng vật lý chịu thuế GTGT 10%",
           VATRate = 10,
           CategoryType = "Goods",
           IsTaxable = true,
           IsActive = true,
           CreatedDate = DateTime.UtcNow
       },
       new Category
       {
           CategoryID = 2,
           Code = "DV",
           Name = "Dịch vụ chịu thuế 8%",
           Description = "Dịch vụ lưu trữ, cho thuê máy chủ",
           VATRate = 8,
           CategoryType = "Service",
           IsTaxable = true,
           IsActive = true,
           CreatedDate = DateTime.UtcNow
       },
       new Category
       {
           CategoryID = 3,
           Code = "SW",
           Name = "Phần mềm không chịu thuế",
           Description = "Sản phẩm phần mềm và bản quyền",
           VATRate = 0,
           CategoryType = "Software",
           IsTaxable = false,
           IsActive = true,
           CreatedDate = DateTime.UtcNow
       }
   );
            modelBuilder.Entity<Product>().HasData(
        new Product
        {
            ProductID = 1,
            Code = "HH0001",
            Name = "Xăng RON95",
            CategoryID = 1,
            Unit = "Lít",
            BasePrice = 23000,
            VATRate = 10,
            Description = "Xăng RON95 chịu thuế GTGT 10%",
            IsActive = true,
            CreatedDate = DateTime.UtcNow
        },
    new Product
    {
        ProductID = 2,
        Code = "DV001",
        Name = "Dịch vụ cho thuê máy chủ (Hosting)",
        CategoryID = 2,
        Unit = "Tháng",
        BasePrice = 500000,
        VATRate = 8,
        Description = "Dịch vụ hosting thuế suất 8%",
        IsActive = true,
        CreatedDate = DateTime.UtcNow
    },
    new Product
    {
        ProductID = 3,
        Code = "SW001",
        Name = "Phần mềm kế toán bản quyền",
        CategoryID = 3,
        Unit = "Gói",
        BasePrice = 10000000,
        VATRate = 0,
        Description = "Phần mềm không chịu thuế GTGT",
        IsActive = true,
        CreatedDate = DateTime.UtcNow
    }
);
            modelBuilder.Entity<User>().HasData(
        new User
        {
            UserID = 1, // Start with ID 1 for Admin
            FullName = "Admin User",
            Email = "admin@eims.local",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("P3ssword!"),
            PhoneNumber = "0101010101",
            RoleID = 1, // Admin Role
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        },
        new User
        {
            UserID = 2,
            FullName = "Accountant User",
            Email = "accountant@eims.local",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("P3ssword!"),
            PhoneNumber = "0202020202",
            RoleID = 2, // Accountant Role
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        },
        new User
        {
            UserID = 3,
            FullName = "Sales User",
            Email = "sale@eims.local",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("P3ssword!"),
            PhoneNumber = "0303030303",
            RoleID = 3, // Sale Role
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        },
        new User
        {
            UserID = 4,
            FullName = "Head Dept User",
            Email = "hod@eims.local",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("P3ssword!"),
            PhoneNumber = "0404040404",
            RoleID = 4, // HOD Role
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        },
            new User
            {
                UserID = 5,
                FullName = "Customer User",
                Email = "customer@eims.local",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("P3ssword!"),
                PhoneNumber = "0505050505",
                RoleID = 5, // Customer Role
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            });
            modelBuilder.Entity<Prefix>().HasData(
                new Prefix { PrefixID = 1, PrefixName = "Hóa đơn điện tử giá trị gia tăng" },
                new Prefix { PrefixID = 2, PrefixName = "Hóa đơn điện tử bán hàng" },
                new Prefix { PrefixID = 3, PrefixName = "Hóa đơn điện tử bán tài sản công" },
                new Prefix { PrefixID = 4, PrefixName = "Hóa đơn điện tử bán hàng dự trữ quốc gia" },
                new Prefix { PrefixID = 5, PrefixName = "Hóa đơn điện tử khác là tem điện tử, vé điện tử, thẻ điện tử, phiếu thu điện tử hoặc các chứng từ điện tử có tên gọi khác nhưng có nội dung của hóa đơn điện tử theo quy định tại Nghị định số 123/2020/NĐ-CP" },
                new Prefix { PrefixID = 6, PrefixName = "Chứng từ điện tử được sử dụng và quản lý như hóa đơn gồm phiếu xuất kho kiêm vận chuyển nội bộ điện tử, phiếu xuất kho hàng gửi bán đại lý điện tử" },
                new Prefix { PrefixID = 7, PrefixName = "Hóa đơn thương mại điện tử" },
                new Prefix { PrefixID = 8, PrefixName = "Hóa đơn giá trị gia tăng tích hợp biên lai thu thuế, phí, lệ phí" },
                new Prefix { PrefixID = 9, PrefixName = "Hóa đơn bán hàng tích hợp biên lai thu thuế, phí, lệ phí" }
            );
            modelBuilder.Entity<InvoiceType>().HasData(
                new InvoiceType { InvoiceTypeID = 1, Symbol = "T", TypeName = "Hóa đơn Doanh nghiệp, tổ chức, hộ, cá nhân kinh doanh đăng ký sử dụng" },
                new InvoiceType { InvoiceTypeID = 2, Symbol = "D", TypeName = "Hóa đơn tài sản công và hóa đơn bán hàng dự trữ quốc gia hoặc hóa đơn điện tử đặc thù không nhất thiết phải có một số tiêu thức do các doanh nghiệp, tổ chức đăng ký sử dụng" },
                new InvoiceType { InvoiceTypeID = 3, Symbol = "L", TypeName = "Hóa đơn Cơ quan thuế cấp theo từng lần phát sinh" },
                new InvoiceType { InvoiceTypeID = 4, Symbol = "M", TypeName = "Hóa đơn khởi tạo từ máy tính tiền" },
                new InvoiceType { InvoiceTypeID = 5, Symbol = "N", TypeName = "Phiếu xuất kho kiêm vận chuyển nội bộ" },
                new InvoiceType { InvoiceTypeID = 6, Symbol = "B", TypeName = "Phiếu xuất kho gửi bán đại lý điện" },
                new InvoiceType { InvoiceTypeID = 7, Symbol = "G", TypeName = "Tem, vé, thẻ điện tử là hóa đơn GTGT" },
                new InvoiceType { InvoiceTypeID = 8, Symbol = "H", TypeName = "Tem, vé, thẻ điện tử là hóa đơn bán hàng" },
                new InvoiceType { InvoiceTypeID = 9, Symbol = "X", TypeName = "Hóa đơn thương mại điện tử" }
            );
            modelBuilder.Entity<SerialStatus>().HasData(
                new SerialStatus { SerialStatusID = 1, Symbol = "C", StatusName = "Hóa đơn có mã của cơ quan thuế" },
                new SerialStatus { SerialStatusID = 2, Symbol = "K", StatusName = "Hóa đơn không có mã của cơ quan thuế" }
            );
            modelBuilder.Entity<TemplateType>().HasData(
                new TemplateType { TemplateTypeID = 1, TypeName = "Hóa đơn mới", TypeCategory = "New" },
                new TemplateType { TemplateTypeID = 2, TypeName = "Hóa đơn điều chỉnh", TypeCategory = "Adjustment" },
                new TemplateType { TemplateTypeID = 3, TypeName = "Hóa đơn thay thế", TypeCategory = "Replacement" }
            );
            modelBuilder.Entity<InvoiceTemplate>()
        .HasOne(t => t.Serial)
        .WithMany(s => s.InvoiceTemplates)
        .HasForeignKey(t => t.SerialID);
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}