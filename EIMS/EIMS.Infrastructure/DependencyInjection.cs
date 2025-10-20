using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EIMS.Application.Common.Interfaces;
using EIMS.Infrastructure.Persistence;

namespace EIMS.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    //register efcore with Npgsql
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(connectionString));

    //bridge interface to concreate dbcontext
    services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

    //register other infra services(email, file storage, etc,..)
    //services.AddScoped<IEmailService, EmailService>();
    return services;
  }
}