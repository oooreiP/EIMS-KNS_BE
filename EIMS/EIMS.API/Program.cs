using System.Text;
using EIMS.API.Middleware;
using EIMS.Application;
using EIMS.Application.Common.Mapping;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.Mails;
using EIMS.Infrastructure;
using EIMS.Infrastructure.Persistence;
using EIMS.Infrastructure.Security;
using EIMS.Infrastructure.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PuppeteerSharp;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

var corsPolicyName = "AllowSpecificOrigins";
var allowedOrigins = config.GetSection("AllowedOrigins").Get<string[]>() ?? new string[0];

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName,
                      policy =>
                      {
                          policy.WithOrigins(allowedOrigins) // Read URLs from appsettings
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials(); // Allow cookies (for refresh token)
                      });
});

//add services from other layers
builder.Services.AddApplicationServices()
    .AddInfrastructureServices(config);

// Add services to the container.

builder.Services.AddControllers();

//configure jwt settings
var jwtSettings = config.GetSection(JwtSettings.SectionName).Get<JwtSettings>();

//configure authentication
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            ClockSkew = TimeSpan.Zero
        };
    });

//configure UseAuthorization
builder.Services.AddAuthorization();

//configure swagger to use jwt
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.Configure<EmailSMTPSettings>(builder.Configuration.GetSection("EmailSMTPSettings"));
builder.Services.Configure<FileSettings>(builder.Configuration.GetSection("FileSettings"));
// builder.Services.AddHttpClient<IEmailService, EmailService>();
builder.Services.AddHttpClient<IExternalCompanyLookupService, VietQrLookupService>();
builder.Services.AddScoped<ICaptchaService, CaptchaService>();
builder.Services.AddMemoryCache();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "AddAuthorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    options.CustomSchemaIds(type => type.ToString());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();
var forwardOptions = new ForwardedHeadersOptions
{
    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | 
                       Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
};

forwardOptions.KnownNetworks.Clear(); 
forwardOptions.KnownProxies.Clear();
app.UseForwardedHeaders(forwardOptions);
using (var scope = app.Services.CreateScope())
{
    Console.WriteLine("Đang kiểm tra và tải trình duyệt Chromium...");
    var browserFetcher = new BrowserFetcher();
    await browserFetcher.DownloadAsync();
    Console.WriteLine("Đã tải xong trình duyệt!");
}
try
{
    using (var scope = app.Services.CreateScope())
    {
        //get the dbContext from services
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        //apply pending migrations 
        dbContext.Database.Migrate();
    }
}
catch (Exception ex)
{
    //log the error if fail
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred while migrating the database.");
}
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Production");
    options.RoutePrefix = "swagger";
});
app.UseCors(corsPolicyName);
// app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<NotificationHub>("/hubs/notifications");
app.MapControllers();

app.Run();
