using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Dashboard;
using EIMS.Application.DTOs.Dashboard.Admin;
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
        // ... imports

        public async Task<AdminDashboardDto> GetAdminDashboardStatsAsync(CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var startOfMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var sixMonthsAgo = now.AddMonths(-6);

            // 1. Base Query (NoTracking for speed)
            var invoices = _context.Invoices.AsNoTracking();

            // 2. Calculate Financials & Counts (Aggregated)
            var stats = await invoices
                .GroupBy(x => 1)
                .Select(g => new
                {
                    // --- All Time ---
                    AllTime_Total = g.Sum(i => i.TotalAmount),
                    AllTime_Subtotal = g.Sum(i => i.SubtotalAmount),
                    AllTime_VAT = g.Sum(i => i.VATAmount),
                    AllTime_Paid = g.Sum(i => i.PaidAmount),
                    AllTime_Pending = g.Sum(i => i.RemainingAmount),

                    // Overdue is Status 4 OR (Not Paid(3) AND DueDate Passed)
                    AllTime_OverdueMoney = g.Where(i => i.PaymentStatusID == 4 || (i.PaymentStatusID != 3 && i.PaymentDueDate < now))
                                            .Sum(i => i.RemainingAmount),

                    // --- Current Month (Filtered by IssuedDate) ---
                    Month_Total = g.Where(i => i.IssuedDate >= startOfMonth).Sum(i => i.TotalAmount),
                    Month_Subtotal = g.Where(i => i.IssuedDate >= startOfMonth).Sum(i => i.SubtotalAmount),
                    Month_VAT = g.Where(i => i.IssuedDate >= startOfMonth).Sum(i => i.VATAmount),
                    Month_Paid = g.Where(i => i.IssuedDate >= startOfMonth).Sum(i => i.PaidAmount),

                    // --- Counts ---
                    Count_Total = g.Count(),
                    Count_Paid = g.Count(i => i.PaymentStatusID == 3),
                    Count_Unpaid = g.Count(i => i.PaymentStatusID == 1),
                    Count_Overdue = g.Count(i => i.PaymentStatusID == 4 || (i.PaymentStatusID != 3 && i.PaymentDueDate < now)),
                })
                .FirstOrDefaultAsync(cancellationToken);

            // 3. Revenue Trend (Group by Month for last 6 months)
            var trend = await invoices
                .Where(i => i.IssuedDate >= sixMonthsAgo)
                .GroupBy(i => new { i.IssuedDate.Value.Year, i.IssuedDate.Value.Month })
                .Select(g => new RevenueTrendDto
                {
                    Year = g.Key.Year,
                    MonthNumber = g.Key.Month,
                    Revenue = g.Sum(i => i.TotalAmount)
                })
                .OrderBy(x => x.Year).ThenBy(x => x.MonthNumber)
                .ToListAsync(cancellationToken);

            // 4. Top 5 Customers
            var topCustomers = await invoices
                .GroupBy(i => i.Customer.CustomerName)
                .Select(g => new TopCustomerDto
                {
                    CustomerName = g.Key ?? "Unknown",
                    InvoiceCount = g.Count(),
                    TotalSpent = g.Sum(i => i.TotalAmount)
                })
                .OrderByDescending(x => x.TotalSpent)
                .Take(5)
                .ToListAsync(cancellationToken);

            // 5. User Stats (Using the Context directly here for simplicity, or inject IUserRepository)
            var userStats = await _context.Users
                .GroupBy(x => 1)
                .Select(g => new UserStatsDto
                {
                    TotalUsers = g.Count(),
                    TotalCustomers = g.Count(u => u.CustomerID != null),
                    NewUsersThisMonth = g.Count(u => u.CreatedAt >= startOfMonth)
                })
                .FirstOrDefaultAsync(cancellationToken);

            // 6. Assemble Result
            var result = new AdminDashboardDto
            {
                CurrentMonthStats = new FinancialStatsDto(),
                AllTimeStats = new FinancialStatsDto(),
                InvoiceCounts = new InvoiceCountDto(),
                UserStats = userStats ?? new UserStatsDto(),
                RevenueTrend = trend,
                TopCustomers = topCustomers
            };

            if (stats != null)
            {
                // Map Monthly
                result.CurrentMonthStats.TotalRevenue = stats.Month_Total;
                result.CurrentMonthStats.NetProfit = stats.Month_Subtotal; // Real Profit (Pre-tax)
                result.CurrentMonthStats.TaxLiability = stats.Month_VAT;
                result.CurrentMonthStats.CollectedAmount = stats.Month_Paid;

                // Map All Time
                result.AllTimeStats.TotalRevenue = stats.AllTime_Total;
                result.AllTimeStats.NetProfit = stats.AllTime_Subtotal;
                result.AllTimeStats.TaxLiability = stats.AllTime_VAT;
                result.AllTimeStats.CollectedAmount = stats.AllTime_Paid;
                result.AllTimeStats.OutstandingAmount = stats.AllTime_Pending;
                result.AllTimeStats.OverdueAmount = stats.AllTime_OverdueMoney;

                // Map Counts
                result.InvoiceCounts.Total = stats.Count_Total;
                result.InvoiceCounts.Paid = stats.Count_Paid;
                result.InvoiceCounts.Unpaid = stats.Count_Unpaid;
                result.InvoiceCounts.Overdue = stats.Count_Overdue;
            }

            // Format Month Names for the Chart
            foreach (var item in result.RevenueTrend)
            {
                item.Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.MonthNumber) + " " + item.Year;
            }

            return result;
        }
    }
}
