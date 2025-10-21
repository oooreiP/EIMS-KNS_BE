using EIMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Invoices> Invoices { get; }
    DbSet<Customers> Customers { get; }
    DbSet<Users> Users { get; }
    DbSet<UserRefreshToken> UserRefreshTokens { get; }
    Task<int> SaveChangeAsync(CancellationToken cancellationToken);
}