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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}