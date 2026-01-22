using EIMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using EIMS.Application.Commons.Interfaces;
using EIMS.Infrastructure.Security;
using EIMS.Infrastructure.Service;
using EIMS.Application.Commons.UnitOfWork;
using EIMS.Infrastructure.Interceptors;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using EIMS.Infrastructure.Service.BackgroundServices;

namespace EIMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //db context
            services.AddDbContext<ApplicationDbContext>(options =>
              options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IApplicationDBContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<AuditableEntityInterceptor>();
            //register security services
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<ILookupCodeGenerator, LookupCodeGenerator>();
            services.AddHttpContextAccessor();
            services.AddHostedService<DebtReminderWorker>();
            services.AddScoped<IAuthCookieService, AuthCookieService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IDocumentParserService, DocumentParserService>();
            services.AddScoped<IFileStorageService, FileStorageService>();
            services.AddScoped<IExternalCompanyLookupService, VietQrLookupService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IStatementService, StatementService>();
            services.AddHttpClient();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            // services.AddHttpClient<IEmailSenderService, MailerSendService>();
            services.AddScoped<IQrCodeService, QRCodeService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IActivityLogger, ActivityLogger>();
            services.AddScoped<ITaxApiClient, MockTaxApiClient>();
            services.AddScoped<IInvoiceXMLService, InvoiceXmlService>();
            services.AddScoped<IMinutesGenerator, MinutesGenerator>();
            services.AddScoped<IPdfService, PdfService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IInvoiceRealtimeService, InvoiceRealtimeService>();
            services.AddScoped<IUserRealtimeService, UserRealtimeService>();
            services.AddScoped<IDashboardRealtimeService, DashboardRealtimeService>();
            services.AddScoped<IEncryptionService, AesEncryptionService>();
            services.AddScoped<SendGridService>();
            // 3. Map IEmailSenderService (Used by Invoices) to SendGrid
            services.AddScoped<IEmailSenderService>(provider => provider.GetRequiredService<SendGridService>());
            return services;

        }
    }
}