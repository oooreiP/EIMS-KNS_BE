using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using EIMS.Infrastructure.Persistence;

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
