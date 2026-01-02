using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Dashboard;
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
        public async Task<CustomerDashboardDto> GetCustomerDashboardStatsAsync(int customerId)
        {
            var now = DateTime.UtcNow;
            
            // Base Query: Filter by Customer
            var query = _context.Invoices.AsNoTracking().Where(i => i.CustomerID == customerId);

            // 1. Calculate Aggregates (Single DB Query)
            // We use the specific PaymentStatusID values you provided:
            // 1=Unpaid, 2=Partially Paid, 3=Paid, 4=Overdue
            var stats = await query
                .GroupBy(x => 1) // Fake grouping to aggregate all rows
                .Select(g => new
                {
                    TotalCount = g.Count(),
                    
                    // Count by IDs
                    PaidCount = g.Count(i => i.PaymentStatusID == 3),
                    UnpaidCount = g.Count(i => i.PaymentStatusID == 1),
                    PartialCount = g.Count(i => i.PaymentStatusID == 2),
                    
                    // Overdue: Either explicitly marked as '4' OR not paid and due date passed
                    OverdueCount = g.Count(i => i.PaymentStatusID == 4 || (i.PaymentStatusID != 3 && i.PaymentDueDate < now)),

                    // Money Sums (Using your specific columns)
                    TotalInvoiced = g.Sum(i => i.TotalAmount),
                    TotalPaid = g.Sum(i => i.PaidAmount),
                    TotalPending = g.Sum(i => i.RemainingAmount)
                })
                .FirstOrDefaultAsync();

            // 2. Fetch Recent 5 Invoices (Includes Status Name for display)
            var recent = await query
                .OrderByDescending(i => i.CreatedAt)
                .Take(5)
                .Select(i => new SimpleInvoiceDto
                {
                    InvoiceId = i.InvoiceID,
                    InvoiceNumber = i.InvoiceNumber,
                    CreatedAt = i.CreatedAt,
                    DueDate = i.PaymentDueDate,
                    TotalAmount = i.TotalAmount,
                    RemainingAmount = i.RemainingAmount,
                    StatusId = i.PaymentStatusID,
                    StatusName = i.PaymentStatus.StatusName // Assuming PaymentStatus table has 'StatusName'
                })
                .ToListAsync();

            // 3. Map to DTO
            var dashboard = new CustomerDashboardDto
            {
                RecentInvoices = recent
            };

            if (stats != null)
            {
                dashboard.TotalInvoices = stats.TotalCount;
                dashboard.PaidInvoicesCount = stats.PaidCount;
                dashboard.UnpaidInvoicesCount = stats.UnpaidCount;
                dashboard.PartiallyPaidInvoicesCount = stats.PartialCount;
                dashboard.OverdueInvoicesCount = stats.OverdueCount;

                dashboard.TotalInvoicedAmount = stats.TotalInvoiced;
                dashboard.TotalAmountPaid = stats.TotalPaid;
                dashboard.TotalAmountPending = stats.TotalPending;
            }

            return dashboard;
        }
    }
}
