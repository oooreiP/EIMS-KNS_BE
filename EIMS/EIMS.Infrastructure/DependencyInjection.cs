using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using EIMS.Application.Commons.Interfaces;
using EIMS.Infrastructure.Security;
using EIMS.Infrastructure.Service;
using EIMS.Infrastructure.Repositories.Interface;
using EIMS.Infrastructure.Repositories;
using EIMS.Domain.Entities;
using EIMS.Application.Commons.UnitOfWork;

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

            //register security services
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddHttpContextAccessor();
            services.AddScoped<IAuthCookieService, AuthCookieService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;

        }
    }
}