using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using EIMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Infrastructure.Repositories
{
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoicesRepository
    {
        private readonly ApplicationDbContext _context;
        public InvoiceRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
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
        public IQueryable<Invoice> ApplySorting(IQueryable<Invoice> query, string? column, string? direction)
        {
            bool isAsc = !string.IsNullOrEmpty(direction) && direction.ToLower() == "asc";
            string col = column?.ToLower()?.Trim() ?? "";

            return col switch
            {
                "date" => isAsc ? query.OrderBy(x => x.CreatedAt) : query.OrderByDescending(x => x.CreatedAt),
                
                "amount" => isAsc ? query.OrderBy(x => x.TotalAmount) : query.OrderByDescending(x => x.TotalAmount),
                
                "number" => isAsc ? query.OrderBy(x => x.InvoiceNumber) : query.OrderByDescending(x => x.InvoiceNumber),
                
                "status" => isAsc ? query.OrderBy(x => x.InvoiceStatusID) : query.OrderByDescending(x => x.InvoiceStatusID),
                
                // Sort by Snapshot Name, fallback to Master Name
                "customer" => isAsc 
                    ? query.OrderBy(x => x.InvoiceCustomerName ?? x.Customer.CustomerName) 
                    : query.OrderByDescending(x => x.InvoiceCustomerName ?? x.Customer.CustomerName),

                // Default: Newest Created At first
                _ => query.OrderByDescending(x => x.CreatedAt) 
            };
        }
    }
}
