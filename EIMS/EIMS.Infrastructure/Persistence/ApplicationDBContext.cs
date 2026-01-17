using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using EIMS.Domain.Enums;
using EIMS.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDBContext
    {
        private readonly AuditableEntityInterceptor _auditableEntityInterceptor;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, AuditableEntityInterceptor auditableEntityInterceptor)
            : base(options)
        {
            _auditableEntityInterceptor = auditableEntityInterceptor;
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
        public DbSet<TaxMessageCode> TaxMessageCodes { get; set; }
        public DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public DbSet<TemplateFrame> TemplateFrames { get; set; }
        public DbSet<InvoiceStatementDetail> InvoiceStatementDetails { get; set; }
        public DbSet<SystemActivityLog> SystemActivityLogs { get; set; }
        public DbSet<InvoiceErrorNotification> InvoiceErrorNotifications { get; set; }
        public DbSet<InvoiceErrorDetail> InvoiceErrorDetails { get; set; }
        public DbSet<InvoiceLookupLog> InvoiceLookupLogs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntityInterceptor);
        }
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
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(c => c.TaxCode).IsUnique();
            });
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
            modelBuilder.Entity<TemplateFrame>().HasData(
                new TemplateFrame { FrameID = 1, FrameName = "Frame 1", ImageUrl = "https://res.cloudinary.com/djz86r9zd/image/upload/v1764156291/khunghoadon11_kqjill.png" },
                new TemplateFrame { FrameID = 2, FrameName = "Frame 2", ImageUrl = "https://res.cloudinary.com/djz86r9zd/image/upload/v1764156289/khunghoadon3_utka5u.png" },
                new TemplateFrame { FrameID = 3, FrameName = "Frame 3", ImageUrl = "https://res.cloudinary.com/djz86r9zd/image/upload/v1764156287/khunghoadon10_pjapiv.png" },
                new TemplateFrame { FrameID = 4, FrameName = "Frame 4", ImageUrl = "https://res.cloudinary.com/djz86r9zd/image/upload/v1764156286/khunghoadon7_shsqte.png" },
                new TemplateFrame { FrameID = 5, FrameName = "Frame 5", ImageUrl = "https://res.cloudinary.com/djz86r9zd/image/upload/v1764156286/khunghoadon4_o9xatr.png" },
                new TemplateFrame { FrameID = 6, FrameName = "Frame 6", ImageUrl = "https://res.cloudinary.com/djz86r9zd/image/upload/v1764156286/khunghoadon9_smq1lj.png" },
                new TemplateFrame { FrameID = 7, FrameName = "Frame 7", ImageUrl = "https://res.cloudinary.com/djz86r9zd/image/upload/v1764156285/khunghoadon5_tveg16.png" },
                new TemplateFrame { FrameID = 8, FrameName = "Frame 8", ImageUrl = "https://res.cloudinary.com/djz86r9zd/image/upload/v1764156285/khunghoadon6_mp5fh1.png" },
                new TemplateFrame { FrameID = 9, FrameName = "Frame 9", ImageUrl = "https://res.cloudinary.com/djz86r9zd/image/upload/v1764156285/khunghoadon8_d5ho2y.png" },
                new TemplateFrame { FrameID = 10, FrameName = "Frame 10", ImageUrl = "https://res.cloudinary.com/djz86r9zd/image/upload/v1764156284/khunghoadon2_vamivw.png" },
                new TemplateFrame { FrameID = 11, FrameName = "Frame 11", ImageUrl = "https://res.cloudinary.com/djz86r9zd/image/upload/v1764156219/khunghoadon1_urc2b5.png" }
                );
            modelBuilder.Entity<TaxMessageCode>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.MessageCode)
                    .HasMaxLength(5)
                    .IsRequired();

                entity.HasIndex(e => e.MessageCode)
                    .IsUnique();

                entity.Property(e => e.MessageName)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.Category)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.FlowType)
                    .IsRequired();
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
            Status = UserAccountStatus.Active,
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
            Status = UserAccountStatus.Active,
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
            Status = UserAccountStatus.Active,
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
            Status = UserAccountStatus.Active,
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
                Status = UserAccountStatus.Active,
                CreatedAt = DateTime.UtcNow
            });
            modelBuilder.Entity<TaxApiStatus>().HasData(
                // === Nhóm trạng thái logic ===
                new TaxApiStatus { TaxApiStatusID = 1, Code = "PENDING", StatusName = "Đang gửi CQT" },
                new TaxApiStatus { TaxApiStatusID = 2, Code = "RECEIVED", StatusName = "CQT đã tiếp nhận" },
                new TaxApiStatus { TaxApiStatusID = 3, Code = "REJECTED", StatusName = "CQT từ chối" },
                new TaxApiStatus { TaxApiStatusID = 4, Code = "APPROVED", StatusName = "CQT đã cấp mã" },
                new TaxApiStatus { TaxApiStatusID = 5, Code = "FAILED", StatusName = "Lỗi hệ thống" },
                new TaxApiStatus { TaxApiStatusID = 6, Code = "PROCESSING", StatusName = "Đang xử lý" },
                new TaxApiStatus { TaxApiStatusID = 7, Code = "NOT_FOUND", StatusName = "Không tìm thấy hóa đơn" },
                new TaxApiStatus { TaxApiStatusID = 8, Code = "NOT_SENT", StatusName = "Hoá đơn đang trong trạng thái nháp/chờ kí" },
                new TaxApiStatus { TaxApiStatusID = 9, Code = "TECHNICAL_ERROR", StatusName = "Lỗi kĩ thuật" },



                // === Các mã thông điệp tiếp nhận TBxx ===
                new TaxApiStatus { TaxApiStatusID = 10, Code = "TB01", StatusName = "Tiếp nhận hợp lệ" },
                new TaxApiStatus { TaxApiStatusID = 11, Code = "TB02", StatusName = "Từ chối: Sai định dạng XML/XSD" },
                new TaxApiStatus { TaxApiStatusID = 12, Code = "TB03", StatusName = "Từ chối: Chữ ký số không hợp lệ" },
                new TaxApiStatus { TaxApiStatusID = 13, Code = "TB04", StatusName = "Từ chối: MST không đúng" },
                new TaxApiStatus { TaxApiStatusID = 14, Code = "TB05", StatusName = "Từ chối: Thiếu thông tin bắt buộc" },
                new TaxApiStatus { TaxApiStatusID = 15, Code = "TB06", StatusName = "Từ chối: Sai định dạng dữ liệu" },
                new TaxApiStatus { TaxApiStatusID = 16, Code = "TB07", StatusName = "Từ chối: Trùng hóa đơn" },
                new TaxApiStatus { TaxApiStatusID = 17, Code = "TB08", StatusName = "Từ chối: Hóa đơn không được cấp mã" },
                new TaxApiStatus { TaxApiStatusID = 18, Code = "TB09", StatusName = "Từ chối: Không tìm thấy hóa đơn tham chiếu" },
                new TaxApiStatus { TaxApiStatusID = 19, Code = "TB10", StatusName = "Từ chối: Thông tin hàng hóa không hợp lệ" },
                new TaxApiStatus { TaxApiStatusID = 20, Code = "TB11", StatusName = "Từ chối: Bản thể hiện PDF sai cấu trúc" },
                new TaxApiStatus { TaxApiStatusID = 21, Code = "TB12", StatusName = "Lỗi kỹ thuật hệ thống thuế" },

                // === Mã kết quả xử lý KQxx ===
                new TaxApiStatus { TaxApiStatusID = 30, Code = "KQ01", StatusName = "Đã cấp mã CQT" },
                new TaxApiStatus { TaxApiStatusID = 31, Code = "KQ02", StatusName = "Bị từ chối khi cấp mã" },
                new TaxApiStatus { TaxApiStatusID = 32, Code = "KQ03", StatusName = "Chưa có kết quả xử lý" },
                new TaxApiStatus { TaxApiStatusID = 33, Code = "KQ04", StatusName = "Không tìm thấy hóa đơn" }
            );

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
            modelBuilder.Entity<InvoiceStatus>().HasData(
                new InvoiceStatus { InvoiceStatusID = 1, StatusName = "Draft" }, // Hóa đơn nháp
                new InvoiceStatus { InvoiceStatusID = 2, StatusName = "Issued" }, // Đã phát hành
                new InvoiceStatus { InvoiceStatusID = 3, StatusName = "Cancelled" }, // Đã hủy
                new InvoiceStatus { InvoiceStatusID = 4, StatusName = "Adjusted" }, // Bị điều chỉnh
                new InvoiceStatus { InvoiceStatusID = 5, StatusName = "Replaced" },  // Bị thay thế
                new InvoiceStatus { InvoiceStatusID = 6, StatusName = "Pending Approval" },  // Đã thanh toán
                new InvoiceStatus { InvoiceStatusID = 7, StatusName = "Pending Sign" },
                new InvoiceStatus { InvoiceStatusID = 8, StatusName = "Signed" },
                new InvoiceStatus { InvoiceStatusID = 9, StatusName = "Sent" },
                new InvoiceStatus { InvoiceStatusID = 10, StatusName = "AdjustmentInProcess" },
                new InvoiceStatus { InvoiceStatusID = 11, StatusName = "ReplacementInProcess" },
                new InvoiceStatus { InvoiceStatusID = 12, StatusName = "TaxAuthority Approved" },
                new InvoiceStatus { InvoiceStatusID = 13, StatusName = "TaxAuthority Rejected" },
                new InvoiceStatus { InvoiceStatusID = 14, StatusName = "Processing" },
                new InvoiceStatus { InvoiceStatusID = 15, StatusName = "Send Error" },
                new InvoiceStatus { InvoiceStatusID = 16, StatusName = "Rejected" }




            );

            modelBuilder.Entity<PaymentStatus>().HasData(
                new PaymentStatus { PaymentStatusID = 1, StatusName = "Unpaid" },
                new PaymentStatus { PaymentStatusID = 2, StatusName = "PartiallyPaid" },
                new PaymentStatus { PaymentStatusID = 3, StatusName = "Paid" },
                new PaymentStatus { PaymentStatusID = 4, StatusName = "Overdue" }
            );
            modelBuilder.Entity<StatementStatus>().HasData(
                new StatementStatus { StatusID = 1, StatusName = "Draft" },          // Editing phase, not visible to client
                new StatementStatus { StatusID = 2, StatusName = "Published" },      // Finalized/Approved, ready to send (Locked)
                new StatementStatus { StatusID = 3, StatusName = "Sent" },           // Emailed/Delivered to client
                new StatementStatus { StatusID = 4, StatusName = "Partially Paid" }, // Client paid 50%, 50% remaining
                new StatementStatus { StatusID = 5, StatusName = "Paid" },           // Fully settled
                new StatementStatus { StatusID = 6, StatusName = "Cancelled" },           // Cancelled (Mistake/Invalid) - Never delete!
                new StatementStatus { StatusID = 7, StatusName = "Refunded" }        // Money returned to client
                        );
            modelBuilder.Entity<NotificationStatus>().HasData(
                new NotificationStatus { StatusID = 1, StatusName = "Chưa đọc" },
                new NotificationStatus { StatusID = 2, StatusName = "Đã đọc" },
                new NotificationStatus { StatusID = 3, StatusName = "Đã lưu trữ" }
            );
            modelBuilder.Entity<NotificationType>().HasData(
        // Loại 1: Hệ thống (Icon: Info/Gear - Màu: Blue/Grey)
        // Dành cho: Admin, All Users
        new NotificationType
        {
            TypeID = 1,
            TypeName = "Hệ thống" // System info, Maintenance, Welcome
        },

        // Loại 2: Hóa đơn (Icon: FileInvoice - Màu: Blue)
        // Dành cho: Sales (Tạo), Accountant (Ký), Customer (Nhận)
        new NotificationType
        {
            TypeID = 2,
            TypeName = "Hóa đơn" // Issued, Signed, Cancelled, Adjusted
        },

        // Loại 3: Thanh toán (Icon: DollarSign/CreditCard - Màu: Green)
        // Dành cho: Accountant (Nhận tiền), Customer (Thanh toán xong), Sales (Theo dõi doanh số)
        new NotificationType
        {
            TypeID = 3,
            TypeName = "Thanh toán" // Payment Success, Refund
        },

        // Loại 4: Nhắc nhở & Cảnh báo (Icon: Bell/ExclamationTriangle - Màu: Yellow/Red)
        // Dành cho: Customer (Nợ quá hạn), Accountant (Deadline báo cáo)
        new NotificationType
        {
            TypeID = 4,
            TypeName = "Nhắc nhở" // Payment Overdue, Debt Reminder
        },
        // Loại 5: Quy trình Phê duyệt (Icon: ClipboardCheck - Màu: Purple)
        // Dành cho: Chief Accountant (Duyệt biên bản), Admin (Duyệt user)
        new NotificationType
        {
            TypeID = 5,
            TypeName = "Phê duyệt" // Minutes Approval, User Request Approval
        },

        // Loại 6: Báo cáo & Đối soát (Icon: ChartBar - Màu: Cyan)
        // Dành cho: Chief Accountant, Admin
        new NotificationType
        {
            TypeID = 6,
            TypeName = "Báo cáo" // Monthly Report Ready, Tax Report
        },

        // Loại 7: Bảo mật (Icon: ShieldLock - Màu: Red)
        // Dành cho: All Users (Login alert, Password changed)
        new NotificationType
        {
            TypeID = 7,
            TypeName = "Bảo mật"
        }
    );
            modelBuilder.Entity<Company>().HasData(
        new Company
        {
            CompanyID = 1,
            CompanyName = "CÔNG TY CỔ PHẦN GIẢI PHÁP TỔNG THỂ KỶ NGUYÊN SỐ",
            TaxCode = "0311357436",
            Address = "26 Nguyễn Đình Khơi, Phường Tân Sơn Nhất, TP Hồ Chí Minh, Việt Nam",
            ContactPhone = "02873000789", // Example phone
            AccountNumber = "1012148510", // Example Account Number
            BankName = "Vietcombank - CN Tan Son Nhat" // Example Bank
        }
    );
            modelBuilder.Entity<EmailTemplate>().HasData(
        new EmailTemplate
        {
            EmailTemplateID = 1,
            TemplateCode = "INVOICE_SEND",
            LanguageCode = "vi",
            Subject = "🔔 [Hóa đơn] #{{InvoiceNumber}} - Thông báo phát hành",
            Category = "invoice",
            IsSystemTemplate = true,
            Name = "Mẫu gửi hóa đơn mặc định",
            IsActive = true,
            BodyContent = @"<!DOCTYPE html>
<html>
<head>
    <style>
        .email-container { font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; background-color: #f4f4f7; padding: 40px 20px; }
        .email-content { max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); overflow: hidden; }
        .email-header { background-color: #007BFF; padding: 30px; text-align: center; color: #ffffff; }
        .email-header h1 { margin: 0; font-size: 24px; font-weight: bold; }
        .email-body { padding: 30px; color: #333333; line-height: 1.6; }
        .info-table { width: 100%; border-collapse: collapse; margin: 20px 0; }
        .info-table td { padding: 12px 0; border-bottom: 1px solid #eeeeee; }
        .info-label { font-weight: bold; color: #555555; width: 40%; }
        .info-value { text-align: right; color: #333333; }
        .highlight-amount { color: #D63384; font-weight: bold; font-size: 18px; }
        .lookup-box { background-color: #f8f9fa; border: 2px dashed #007BFF; border-radius: 6px; padding: 15px; text-align: center; margin: 25px 0; }
        .lookup-code { display: block; font-size: 24px; letter-spacing: 2px; font-weight: bold; color: #007BFF; margin-top: 5px; }
        .email-footer { background-color: #f4f4f7; padding: 20px; text-align: center; font-size: 12px; color: #888888; }
        .attachment-list { list-style: none; padding: 0; margin: 0; }
        .attachment-list li { margin-bottom: 8px; }
        .attachment-list a { color: #007BFF; text-decoration: none; font-weight: bold; }
    </style>
</head>
<body>
    <div class='email-container'>
        <div class='email-content'>
            <div class='email-header'>
                <h1>Hóa Đơn Điện Tử</h1>
            </div>

            <div class='email-body'>
                <p><strong>Xin chào {{CustomerName}},</strong></p>
                <p>{{Message}}</p>
                
                <p>Hệ thống xin thông báo hóa đơn của quý khách đã được phát hành với thông tin chi tiết như sau:</p>

                <table class='info-table'>
                    <tr>
                        <td class='info-label'>Số hóa đơn:</td>
                        <td class='info-value'>#{{InvoiceNumber}}</td>
                    </tr>
                    <tr>
                        <td class='info-label'>Ký hiệu (Serial):</td>
                        <td class='info-value'>{{Serial}}</td>
                    </tr>
                    <tr>
                        <td class='info-label'>Ngày phát hành:</td>
                        <td class='info-value'>{{IssuedDate}}</td>
                    </tr>
                    <tr>
                        <td class='info-label'>Tổng thanh toán:</td>
                        <td class='info-value highlight-amount'>{{TotalAmount}} VND</td>
                    </tr>
                </table>

                <div class='lookup-box'>
                    <span>Mã tra cứu hóa đơn</span>
                    <span class='lookup-code'>{{LookupCode}}</span>
                </div>

                <p>📂 <strong>Tài liệu đính kèm:</strong></p>
                <ul class='attachment-list'>
                    {{AttachmentList}}
                </ul>

                <p style='margin-top: 30px;'>Nếu quý khách có thắc mắc, vui lòng liên hệ bộ phận hỗ trợ.</p>
                <p>Trân trọng,<br><strong>Đội ngũ EIMS</strong></p>
            </div>

            <div class='email-footer'>
                <p>&copy; 2026 EIMS KNS Solutions. All rights reserved.</p>
                <p>Email này được gửi tự động, vui lòng không trả lời.</p>
            </div>
        </div>
    </div>
</body>
</html>"
        },
        new EmailTemplate
        {
            EmailTemplateID = 2,
            TemplateCode = "INVOICE_SEND",
            LanguageCode = "en",
            Subject = "🧾 [Invoice] #{{InvoiceNumber}} - Issued Notification",
            Category = "invoice",
            IsSystemTemplate = true,
            Name = "Standard Invoice Email (English)",
            IsActive = true,
            BodyContent = @"<div style='font-family:Arial, sans-serif; padding: 20px; border: 1px solid #ddd; max-width: 600px; margin: 0 auto;'>
                <h2 style='color:#007BFF;'>Hello {{CustomerName}},</h2>
                <p style='background:#f0f8ff; padding:10px; border-left:4px solid #007BFF; font-style:italic;'>{{Message}}</p>
                <p>We are pleased to inform you that your e-invoice has been issued:</p>
                <table style='width:100%; margin:15px 0;'>
                    <tr><td><strong>Invoice No:</strong></td><td>#{{InvoiceNumber}}</td></tr>
                    <tr><td><strong>Total Amount:</strong></td><td style='color:#D63384; font-weight:bold;'>{{TotalAmount}} VND</td></tr>
                </table>
                <p>📂 <strong>Attachments:</strong></p>
                <ul>{{AttachmentList}}</ul>
                <p style='color:#777; font-size:12px;'>Best Regards,<br>EIMS Team</p>
            </div>"
        },
        new EmailTemplate
        {
            EmailTemplateID = 3,
            TemplateCode = "PAYMENT_REMINDER",
            LanguageCode = "vi",
            Subject = "🔥 [NHẮC THANH TOÁN] Hóa đơn #{{InvoiceNumber}} quá hạn",
            Category = "payment",
            IsSystemTemplate = true,
            Name = "Mẫu nhắc nợ khẩn cấp",
            IsActive = true,
            BodyContent = @"<div style='font-family:Arial, sans-serif; border: 2px solid #dc3545; padding: 20px; max-width: 600px; margin: 0 auto;'>
                <h2 style='color:#dc3545;'>⚠️ Thông báo Nhắc thanh toán</h2>
                <p>Kính gửi {{CustomerName}},</p>
                <div style='background:#fff3cd; color:#856404; padding:10px; margin:10px 0;'>
                    <strong>Lời nhắn:</strong> {{Message}}
                </div>
                <p>Hóa đơn <strong>#{{InvoiceNumber}}</strong> ({{TotalAmount}} VND) hiện chưa được thanh toán.</p>
                <ul>{{AttachmentList}}</ul>
            </div>"
        },
        new EmailTemplate
        {
            EmailTemplateID = 4,
            TemplateCode = "INVOICE_SEND_2",
            LanguageCode = "vi",
            Subject = "🔔 [Hóa đơn] #{{InvoiceNumber}} - Thông báo phát hành",
            Category = "invoice",
            IsSystemTemplate = true,
            Name = "Mẫu gửi hóa đơn (Giao diện bảng chi tiết)",
            IsActive = true,
            BodyContent = @"<!DOCTYPE html>
<html>
<head>
    <style>
        .email-container { font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; background-color: #f4f4f7; padding: 40px 20px; }
        .email-content { max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); overflow: hidden; }
        .email-header { background-color: #007BFF; padding: 30px; text-align: center; color: #ffffff; }
        .email-header h1 { margin: 0; font-size: 24px; font-weight: bold; }
        .email-body { padding: 30px; color: #333333; line-height: 1.6; }
        .info-table { width: 100%; border-collapse: collapse; margin: 20px 0; }
        .info-table td { padding: 12px 0; border-bottom: 1px solid #eeeeee; }
        .info-label { font-weight: bold; color: #555555; width: 40%; }
        .info-value { text-align: right; color: #333333; }
        .highlight-amount { color: #D63384; font-weight: bold; font-size: 18px; }
        .lookup-box { background-color: #f8f9fa; border: 2px dashed #007BFF; border-radius: 6px; padding: 15px; text-align: center; margin: 25px 0; }
        .lookup-code { display: block; font-size: 24px; letter-spacing: 2px; font-weight: bold; color: #007BFF; margin-top: 5px; }
        .email-footer { background-color: #f4f4f7; padding: 20px; text-align: center; font-size: 12px; color: #888888; }
        .attachment-list { list-style: none; padding: 0; margin: 0; }
        .attachment-list li { margin-bottom: 8px; }
        .attachment-list a { color: #007BFF; text-decoration: none; font-weight: bold; }
    </style>
</head>
<body>
    <div class='email-container'>
        <div class='email-content'>
            <div class='email-header'>
                <h1>Hóa Đơn Điện Tử</h1>
            </div>

            <div class='email-body'>
                <p><strong>Xin chào {{CustomerName}},</strong></p>
                <p>{{Message}}</p>
                
                <p>Hệ thống xin thông báo hóa đơn của quý khách đã được phát hành với thông tin chi tiết như sau:</p>

                <table class='info-table'>
                    <tr>
                        <td class='info-label'>Số hóa đơn:</td>
                        <td class='info-value'>#{{InvoiceNumber}}</td>
                    </tr>
                    <tr>
                        <td class='info-label'>Ký hiệu (Serial):</td>
                        <td class='info-value'>{{Serial}}</td>
                    </tr>
                    <tr>
                        <td class='info-label'>Ngày phát hành:</td>
                        <td class='info-value'>{{IssuedDate}}</td>
                    </tr>
                    <tr>
                        <td class='info-label'>Tổng thanh toán:</td>
                        <td class='info-value highlight-amount'>{{TotalAmount}} VND</td>
                    </tr>
                </table>

                <div class='lookup-box'>
                    <span>Mã tra cứu hóa đơn</span>
                    <span class='lookup-code'>{{LookupCode}}</span>
                </div>

                <p>📂 <strong>Tài liệu đính kèm:</strong></p>
                <ul class='attachment-list'>
                    {{AttachmentList}}
                </ul>

                <p style='margin-top: 30px;'>Nếu quý khách có thắc mắc, vui lòng liên hệ bộ phận hỗ trợ.</p>
                <p>Trân trọng,<br><strong>Đội ngũ EIMS</strong></p>
            </div>

            <div class='email-footer'>
                <p>&copy; 2026 EIMS KNS Solutions. All rights reserved.</p>
                <p>Email này được gửi tự động, vui lòng không trả lời.</p>
            </div>
        </div>
    </div>
</body>
</html>"
        },
    new EmailTemplate
    {
        EmailTemplateID = 7,
        TemplateCode = "MINUTES_REPLACE",
        LanguageCode = "vi",
        Subject = "📝 [XÁC NHẬN] Biên bản thu hồi hóa đơn #{{InvoiceNumber}}",
        Category = "minutes",
        IsSystemTemplate = true,
        Name = "Mẫu biên bản thu hồi/thay thế",
        IsActive = true,
        BodyContent = @"<div style='font-family:Arial, sans-serif; padding: 20px; border: 1px solid #ddd; max-width: 600px; margin: 0 auto;'>
            <h2 style='color:#007BFF;'>Kính gửi {{CustomerName}},</h2>
            <p>Do có sự sai sót về thông tin trên hóa đơn số <strong>#{{InvoiceNumber}}</strong> (Ngày lập: {{IssuedDate}}), chúng tôi đã lập biên bản thu hồi/thay thế hóa đơn này.</p>
            
            <div style='background:#fff3cd; color:#856404; padding:10px; margin:15px 0;'>
                <strong>Lý do sai sót:</strong> {{Reason}}
            </div>

            <p>Kính đề nghị Quý khách xem xét, <strong>ký xác nhận</strong> vào biên bản đính kèm và phản hồi lại email này để chúng tôi tiến hành xuất hóa đơn thay thế mới.</p>
            
            <p>📂 <strong>Tài liệu đính kèm:</strong></p>
            <ul>{{AttachmentList}}</ul>
            
            <p style='color:#777; font-size:12px;'>Trân trọng,<br><strong>Đội ngũ Kế toán EIMS</strong></p>
        </div>"
    },

    // Mẫu 8: Biên bản Điều chỉnh
    new EmailTemplate
    {
        EmailTemplateID = 8,
        TemplateCode = "MINUTES_ADJUST",
        LanguageCode = "vi",
        Subject = "📝 [XÁC NHẬN] Biên bản điều chỉnh hóa đơn #{{InvoiceNumber}}",
        Category = "minutes",
        IsSystemTemplate = true,
        Name = "Mẫu biên bản điều chỉnh",
        IsActive = true,
        BodyContent = @"<div style='font-family:Arial, sans-serif; padding: 20px; border: 1px solid #ddd; max-width: 600px; margin: 0 auto;'>
            <h2 style='color:#007BFF;'>Kính gửi {{CustomerName}},</h2>
            <p>Chúng tôi gửi đến Quý khách biên bản thỏa thuận điều chỉnh cho hóa đơn số <strong>#{{InvoiceNumber}}</strong>.</p>
            
            <div style='background:#e2e3e5; color:#383d41; padding:10px; margin:15px 0;'>
                <strong>Nội dung điều chỉnh:</strong> {{Reason}}
            </div>

            <p>Quý khách vui lòng kiểm tra, <strong>ký số (hoặc ký tươi)</strong> vào biên bản đính kèm và gửi lại cho chúng tôi.</p>
            
            <ul>{{AttachmentList}}</ul>
            <p style='color:#777; font-size:12px;'>Trân trọng,<br><strong>Đội ngũ Kế toán EIMS</strong></p>
        </div>"
    }
  );
            modelBuilder.Entity<TaxMessageCode>().HasData(
    // ---- Đăng ký ----
    new TaxMessageCode { Id = 1, MessageCode = "100", MessageName = "Thông điệp gửi tờ khai đăng ký/thay đổi thông tin sử dụng hóa đơn điện tử", Category = "Đăng ký", FlowType = 1 },
    new TaxMessageCode { Id = 2, MessageCode = "101", MessageName = "Thông điệp gửi tờ khai đăng ký thay đổi thông tin đăng ký sử dụng HĐĐT khi ủy nhiệm/nhận ủy nhiệm lập hóa đơn", Category = "Đăng ký", FlowType = 1 },
    new TaxMessageCode { Id = 3, MessageCode = "102", MessageName = "Thông điệp tiếp nhận/không tiếp nhận tờ khai đăng ký HĐĐT", Category = "Đăng ký", FlowType = 2 },
    new TaxMessageCode { Id = 4, MessageCode = "103", MessageName = "Thông điệp chấp nhận/không chấp nhận đăng ký HĐĐT", Category = "Đăng ký", FlowType = 2 },
    new TaxMessageCode { Id = 5, MessageCode = "104", MessageName = "Thông điệp chấp nhận/không chấp nhận thay đổi thông tin đăng ký HĐĐT khi ủy nhiệm", Category = "Đăng ký", FlowType = 2 },
    new TaxMessageCode { Id = 6, MessageCode = "105", MessageName = "Thông điệp hết thời gian sử dụng HĐĐT có mã", Category = "Đăng ký", FlowType = 2 },
    new TaxMessageCode { Id = 7, MessageCode = "106", MessageName = "Thông điệp gửi Đơn đề nghị cấp HĐĐT có mã theo từng lần", Category = "Đăng ký", FlowType = 1 },
    new TaxMessageCode { Id = 8, MessageCode = "107", MessageName = "Thông điệp yêu cầu giải trình/bổ sung tài liệu", Category = "Đăng ký", FlowType = 2 },
    new TaxMessageCode { Id = 9, MessageCode = "108", MessageName = "Thông điệp ngừng sử dụng hóa đơn điện tử", Category = "Đăng ký", FlowType = 2 },
    new TaxMessageCode { Id = 10, MessageCode = "109", MessageName = "Thông điệp gửi tờ khai đăng ký/thay đổi thông tin sử dụng chứng từ điện tử", Category = "Đăng ký", FlowType = 1 },
    new TaxMessageCode { Id = 11, MessageCode = "110", MessageName = "Thông điệp gửi tờ khai đăng ký thay đổi thông tin CTĐT khi ủy nhiệm", Category = "Đăng ký", FlowType = 1 },
    new TaxMessageCode { Id = 12, MessageCode = "111", MessageName = "Thông điệp tiếp nhận/không tiếp nhận tờ khai CTĐT", Category = "Đăng ký", FlowType = 2 },
    new TaxMessageCode { Id = 13, MessageCode = "112", MessageName = "Thông điệp chấp nhận/không chấp nhận đăng ký CTĐT", Category = "Đăng ký", FlowType = 2 },
    new TaxMessageCode { Id = 14, MessageCode = "113", MessageName = "Thông điệp chấp nhận/không chấp nhận đăng ký CTĐT khi ủy nhiệm", Category = "Đăng ký", FlowType = 2 },
    new TaxMessageCode { Id = 15, MessageCode = "114", MessageName = "Thông điệp thông báo về việc NNT hủy tờ khai hoặc thông báo", Category = "Đăng ký", FlowType = 2 },

    // ---- Tài khoản Cổng TCT ----
    new TaxMessageCode { Id = 16, MessageCode = "150", MessageName = "Thông điệp gửi tờ khai đăng ký mới/thay đổi thông tin/Chấm dứt tài khoản Cổng TCT", Category = "Tài khoản Cổng TCT", FlowType = 1 },
    new TaxMessageCode { Id = 17, MessageCode = "151", MessageName = "Thông điệp tiếp nhận/không tiếp nhận tài khoản Cổng TCT", Category = "Tài khoản Cổng TCT", FlowType = 2 },
    new TaxMessageCode { Id = 18, MessageCode = "152", MessageName = "Thông điệp chấp nhận/không chấp nhận tài khoản Cổng TCT", Category = "Tài khoản Cổng TCT", FlowType = 2 },

    // ---- Hóa đơn cấp mã ----
    new TaxMessageCode { Id = 19, MessageCode = "200", MessageName = "Thông điệp gửi hóa đơn điện tử tới cơ quan thuế để cấp mã", Category = "Hóa đơn Cấp mã", FlowType = 1 },
    new TaxMessageCode { Id = 20, MessageCode = "201", MessageName = "Thông điệp gửi hóa đơn điện tử theo từng lần phát sinh", Category = "Hóa đơn Cấp mã", FlowType = 1 },
    new TaxMessageCode { Id = 21, MessageCode = "202", MessageName = "Thông điệp kết quả cấp mã hóa đơn điện tử", Category = "Hóa đơn Cấp mã", FlowType = 2 },
    new TaxMessageCode { Id = 22, MessageCode = "203", MessageName = "Thông điệp gửi dữ liệu hóa đơn không mã", Category = "Hóa đơn Không mã", FlowType = 1 },
    new TaxMessageCode { Id = 23, MessageCode = "204", MessageName = "Kết quả kiểm tra dữ liệu hóa đơn điện tử (TB01/KTDL)", Category = "Hóa đơn Kiểm tra", FlowType = 2 },
    new TaxMessageCode { Id = 24, MessageCode = "205", MessageName = "Phản hồi hồ sơ đề nghị cấp mã theo từng lần", Category = "Hóa đơn Cấp mã", FlowType = 2 },
    new TaxMessageCode { Id = 25, MessageCode = "206", MessageName = "Thông điệp gửi hóa đơn từ máy tính tiền", Category = "HĐ Máy tính tiền", FlowType = 1 },
    new TaxMessageCode { Id = 26, MessageCode = "207", MessageName = "Thông điệp gửi hóa đơn Casino", Category = "HĐ Đặc thù", FlowType = 1 },
    new TaxMessageCode { Id = 27, MessageCode = "208", MessageName = "Thông điệp gửi hóa đơn điều chỉnh/thay thế nhiều", Category = "HĐ Điều chỉnh", FlowType = 1 },
    new TaxMessageCode { Id = 28, MessageCode = "209", MessageName = "Thông điệp gửi hóa đơn tích hợp biên lai", Category = "HĐ Đặc thù", FlowType = 1 },

    // ---- Hóa đơn sai sót ----
    new TaxMessageCode { Id = 29, MessageCode = "300", MessageName = "Thông điệp thông báo hóa đơn có sai sót", Category = "HĐ Sai sót", FlowType = 1 },
    new TaxMessageCode { Id = 30, MessageCode = "301", MessageName = "Tiếp nhận và kết quả xử lý hóa đơn sai sót", Category = "HĐ Sai sót", FlowType = 2 },
    new TaxMessageCode { Id = 31, MessageCode = "302", MessageName = "Thông điệp thông báo hóa đơn cần rà soát", Category = "HĐ Sai sót", FlowType = 2 },
    new TaxMessageCode { Id = 32, MessageCode = "303", MessageName = "Thông điệp thông báo hóa đơn từ máy tính tiền có sai sót", Category = "HĐ Sai sót", FlowType = 1 },

    // ---- Bảng tổng hợp ----
    new TaxMessageCode { Id = 33, MessageCode = "400", MessageName = "Thông điệp chuyển bảng tổng hợp dữ liệu hóa đơn điện tử", Category = "Bảng Tổng hợp", FlowType = 1 },
    new TaxMessageCode { Id = 34, MessageCode = "401", MessageName = "Chuyển bảng tổng hợp + bảng kê điều chỉnh/thay thế", Category = "Bảng Tổng hợp", FlowType = 1 },

    // ---- TCTN Ủy quyền ----
    new TaxMessageCode { Id = 35, MessageCode = "500", MessageName = "Chuyển dữ liệu HĐĐT TCTN ủy quyền cấp mã", Category = "TCTN Ủy quyền", FlowType = 1 },
    new TaxMessageCode { Id = 36, MessageCode = "503", MessageName = "Chuyển dữ liệu hóa đơn không đủ điều kiện cấp mã", Category = "TCTN Ủy quyền", FlowType = 1 },
    new TaxMessageCode { Id = 37, MessageCode = "504", MessageName = "Chuyển dữ liệu TB01/KTDL của TCUQ gửi NNT", Category = "TCTN Ủy quyền", FlowType = 2 },
    new TaxMessageCode { Id = 38, MessageCode = "505", MessageName = "Cung cấp MST thay đổi thông tin trong ngày", Category = "TCTN Cung cấp", FlowType = 1 },
    new TaxMessageCode { Id = 39, MessageCode = "506", MessageName = "Cung cấp quyết định ngừng/tiếp tục sử dụng hóa đơn", Category = "TCTN Cung cấp", FlowType = 1 },
    new TaxMessageCode { Id = 40, MessageCode = "507", MessageName = "Cung cấp thông tin đăng ký sử dụng hóa đơn điện tử", Category = "TCTN Cung cấp", FlowType = 1 },

    // ---- Ký số ủy quyền ----
    new TaxMessageCode { Id = 41, MessageCode = "600", MessageName = "Đề nghị ký số hóa đơn cấp mã", Category = "Ký số Ủy quyền", FlowType = 1 },
    new TaxMessageCode { Id = 42, MessageCode = "601", MessageName = "Tổng cục Thuế ký số hóa đơn đã cấp mã", Category = "Ký số Ủy quyền", FlowType = 2 },
    new TaxMessageCode { Id = 43, MessageCode = "602", MessageName = "Đề nghị ký số lên thông báo", Category = "Ký số Ủy quyền", FlowType = 1 },
    new TaxMessageCode { Id = 44, MessageCode = "603", MessageName = "Tổng cục Thuế ký số thông báo", Category = "Ký số Ủy quyền", FlowType = 2 },

    // ---- Chứng từ sai sót ----
    new TaxMessageCode { Id = 45, MessageCode = "700", MessageName = "Gửi thông báo chứng từ điện tử sai", Category = "Chứng từ Sai sót", FlowType = 1 },
    new TaxMessageCode { Id = 46, MessageCode = "702", MessageName = "Tiếp nhận & xử lý CTĐT sai", Category = "Chứng từ Sai sót", FlowType = 2 },
    new TaxMessageCode { Id = 47, MessageCode = "703", MessageName = "Thông báo CTĐT cần rà soát", Category = "Chứng từ Sai sót", FlowType = 2 },
    new TaxMessageCode { Id = 48, MessageCode = "704", MessageName = "Thông báo kết quả kiểm tra CTĐT", Category = "Chứng từ Sai sót", FlowType = 2 },

    // ---- Phản hồi kỹ thuật & khác ----
    new TaxMessageCode { Id = 49, MessageCode = "999", MessageName = "Thông điệp phản hồi kỹ thuật", Category = "Phản hồi Khác", FlowType = 2 },
    new TaxMessageCode { Id = 50, MessageCode = "901", MessageName = "Báo cáo đối soát hàng ngày", Category = "Báo cáo Đối soát", FlowType = 1 },
    new TaxMessageCode { Id = 51, MessageCode = "902", MessageName = "Báo cáo đối soát TCTN", Category = "Báo cáo Đối soát", FlowType = 1 },
    new TaxMessageCode { Id = 52, MessageCode = "903", MessageName = "Phản hồi kỹ thuật CTĐT", Category = "Phản hồi Khác", FlowType = 2 },
    new TaxMessageCode { Id = 53, MessageCode = "-1", MessageName = "Thông điệp phản hồi sai định dạng XML", Category = "Phản hồi Khác", FlowType = 2 },
    new TaxMessageCode { Id = 54, MessageCode = "-2", MessageName = "Dữ liệu đề nghị ký số bị lỗi", Category = "Phản hồi Khác", FlowType = 2 }
);
            modelBuilder.Entity<InvoiceTemplate>()
        .HasOne(t => t.Serial)
        .WithMany(s => s.InvoiceTemplates)
        .HasForeignKey(t => t.SerialID);
            modelBuilder.Entity<InvoiceErrorNotification>(entity =>
            {
                // Khai báo rõ ràng Khóa chính là NotificationID
                entity.HasKey(e => e.InvoiceErrorNotificationID);

                // Cấu hình quan hệ 1-N với Detail
                entity.HasMany(e => e.Details)
                      .WithOne(d => d.Notification)
                      .HasForeignKey(d => d.NotificationID) // Chỉ định rõ FK
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}