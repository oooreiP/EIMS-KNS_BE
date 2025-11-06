using System.Text;
using EIMS.Application;
using EIMS.Application.Common.Mapping;
using EIMS.Infrastructure;
using EIMS.Infrastructure.Persistence;
using EIMS.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();
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
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
