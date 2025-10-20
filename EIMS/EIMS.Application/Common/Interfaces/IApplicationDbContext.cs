using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Invoices> Invoices { get; }
    DbSet<Customers> Customers { get; }
    DbSet<Users> Users { get; }
}