using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using EIMS.Infrastructure.Persistence;
using Humanizer;

namespace EIMS.Infrastructure.Repositories
{
    public class InvoiceStatusRepository : BaseRepository<InvoiceStatus>, IInvoiceStatusRepository
    {
        public InvoiceStatusRepository(ApplicationDbContext db) : base(db)
        {
        }
        
    }
}