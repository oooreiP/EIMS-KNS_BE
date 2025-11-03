using EIMS.Domain.Entities;
using EIMS.Infrastructure.Persistence;
using EIMS.Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Infrastructure.Repositories
{
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoicesRepository
    {
        private readonly ApplicationDbContext _context;
        public InvoiceRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        //public async Task<string> GenerateInvoiceNumberAsync()
        //{
        //    var latestInvoice = await _context.Invoices
        //        .OrderByDescending(i => i.InvoiceID)
        //        .FirstOrDefaultAsync();

        //    int nextNumber = 1;
        //    if (latestInvoice != null && int.TryParse(latestInvoice.InvoiceNumber.Replace("INV-", ""), out var lastNum))
        //        nextNumber = lastNum + 1;

        //    return $"INV-{nextNumber:D6}";
        //}
        public async Task<Invoice> CreateInvoiceAsync(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);

            if (invoice.InvoiceItems != null && invoice.InvoiceItems.Any())
            {
                await _context.InvoiceItems.AddRangeAsync(invoice.InvoiceItems);
            }

            await _context.SaveChangesAsync();
            return invoice;
        }
    }
}
