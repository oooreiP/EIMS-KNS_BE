using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using EIMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Infrastructure.Repositories
{
    public class InvoiceStatementRepository : BaseRepository<InvoiceStatement>, IInvoiceStatementRepository
    {
        public InvoiceStatementRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<InvoiceStatement?> GetByIdWithInvoicesAsync(int id)
        {
            return await _db.InvoiceStatements
            .Include(s => s.Customer)
            .Include(s => s.StatementStatus)
            .Include(s => s.StatementDetails)
                .ThenInclude(sd => sd.Invoice)
            .FirstOrDefaultAsync(s => s.StatementID == id);
        }
    }
}