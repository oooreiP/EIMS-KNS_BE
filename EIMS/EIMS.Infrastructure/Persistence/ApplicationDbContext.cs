using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Common.Interfaces;
using EIMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace EIMS.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    //Add entities as DbSet

    public DbSet<Invoices> Invoices => Set<Invoices>();
    public DbSet<Customers> Customers => Set<Customers>();
    public DbSet<Users> Users => Set<Users>();
    public DbSet<UserRefreshToken> UserRefreshTokens => throw new NotImplementedException();

    public Task<int> SaveChangeAsync(CancellationToken cancellationToken)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserRefreshToken>()
            .HasOne(rt => rt.User)
            .WithMany()
            .HasForeignKey(rt => rt.UserId);
    }
}