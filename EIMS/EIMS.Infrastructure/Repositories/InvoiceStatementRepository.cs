using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Helpers;
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
        public async Task<List<InvoiceStatement>> GetStatementsContainingInvoiceAsync(int invoiceId)
        {
            return await _db.InvoiceStatements
                .Where(statement => statement.StatementDetails
                    .Any(detail => detail.InvoiceID == invoiceId)
                )
                .ToListAsync();
        }
        public async Task<List<InvoiceStatement>> GetUnpaidStatementsInMonthAsync(int month, int year)
        {
            var startDate = new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            endDate.EndOfDay();
            return await dbSet
                .Include(s => s.Customer) 
                .Where(s =>
                    s.DueDate >= startDate &&
                    s.DueDate <= endDate &&
                    s.PaidAmount < s.TotalAmount && 
                    s.StatusID != 6 
                )
                .ToListAsync();
        }       
    }
}