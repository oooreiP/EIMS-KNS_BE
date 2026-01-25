using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Dashboard;
using EIMS.Application.DTOs.Dashboard.Accountant;
using EIMS.Application.DTOs.Dashboard.Admin;
using EIMS.Application.DTOs.Dashboard.HOD;
using EIMS.Application.DTOs.Dashboard.Sale;
using EIMS.Application.DTOs.Invoices;
using EIMS.Domain.Entities;
using EIMS.Domain.Enums;
using EIMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EIMS.Infrastructure.Repositories
{
    public class InvoiceRepository : BaseRepository<Invoice>, IInvoicesRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        public InvoiceRepository(ApplicationDbContext context, IConfiguration config) : base(context)
        {
            _context = context;
            _config = config;
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
        public async Task<InvoiceSymbolDto?> GetInvoiceSymbolAsync(int invoiceId)
        {

            var result = await _context.Invoices
                .AsNoTracking()
                .Where(x => x.InvoiceID == invoiceId)
                .Select(x => new InvoiceSymbolDto
                {
                    MauSo = x.Template.Serial.Prefix.PrefixID.ToString(),
                    KyHieu = x.Template.Serial.SerialStatus.Symbol
                           + x.Template.Serial.Year
                           + x.Template.Serial.InvoiceType.Symbol
                           + x.Template.Serial.Tail
                })
                .FirstOrDefaultAsync();
            return result;
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
                "payment" => isAsc ? query.OrderBy(x => x.PaymentStatusID) : query.OrderByDescending(x => x.PaymentStatusID),

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

        public async Task<AdminDashboardDto> GetAdminDashboardStatsAsync(string? period, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var targetDate = now;
            if (!string.IsNullOrEmpty(period) && period.ToLower() == "last_month")
            {
                targetDate = now.AddMonths(-1);
            }
            var startOfMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var startOfLastMonth = startOfMonth.AddMonths(-1);
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
                    //Last Month
                    LastMonth_Total = g.Where(i => i.IssuedDate >= startOfLastMonth && i.IssuedDate < startOfMonth)
                               .Sum(i => i.TotalAmount),
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
            userStats.UsersByRole = await _context.Users
            .GroupBy(u => u.Role.RoleName)
            .Select(g => new UserRoleStatDto
            {
                Role = g.Key,
                Count = g.Count()
            })
            .ToListAsync(cancellationToken);
            var recentInvoices = await invoices
                    .Include(i => i.Customer)
                    .Include(i => i.InvoiceStatus)
                    .Include(i => i.PaymentStatus)
                    .OrderByDescending(i => i.CreatedAt)
                    .Take(7) // Top 7 most recent
                    .Select(i => new RecentInvoiceDto
                    {
                        InvoiceId = i.InvoiceID,
                        InvoiceNumber = i.InvoiceNumber,
                        CustomerName = i.Customer.CustomerName,
                        CreatedAt = i.CreatedAt,
                        Amount = i.TotalAmount,
                        StatusName = i.InvoiceStatus.StatusName,
                        PaymentStatus = i.PaymentStatus.StatusName,
                        DueDate = i.PaymentDueDate,
                        IsOverdue = i.PaymentStatusID != 3
                        && i.PaymentDueDate != null
                        && i.PaymentDueDate < now
                    })
                .ToListAsync(cancellationToken);
            // 6. Assemble Result
            var result = new AdminDashboardDto
            {
                CurrentMonthStats = new FinancialStatsDto(),
                AllTimeStats = new FinancialStatsDto(),
                InvoiceCounts = new InvoiceCountDto(),
                UserStats = userStats ?? new UserStatsDto(),
                RevenueTrend = trend,
                TopCustomers = topCustomers,
                RecentInvoices = recentInvoices
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
                // --- Calculate Growth Percentage ---
                double growth = 0;
                if (stats.LastMonth_Total > 0)
                {
                    growth = (double)((stats.Month_Total - stats.LastMonth_Total) / stats.LastMonth_Total) * 100;
                }
                else if (stats.Month_Total > 0)
                {
                    growth = 100;
                }
                result.RevenueGrowthPercentage = Math.Round(growth, 2);
            }

            // Format Month Names for the Chart
            foreach (var item in result.RevenueTrend)
            {
                item.Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(item.MonthNumber) + " " + item.Year;
            }

            return result;
        }
        public async Task<SalesDashboardDto> GetSalesDashboardStatsAsync(int salesPersonId, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var startOfMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var startOfNextMonth = startOfMonth.AddMonths(1);
            var startOfLastMonth = startOfMonth.AddMonths(-1);
            var sixMonthsAgo = startOfMonth.AddMonths(-5);

            const int paymentStatusUnpaid = 1;

            var user = await _context.Users
                .AsNoTracking()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserID == salesPersonId, cancellationToken);

            var query = _context.Invoices
                .AsNoTracking()
                .Where(i => i.SalesID == salesPersonId);

            var currentRevenue = await query
                .Where(i => i.CreatedAt >= startOfMonth && i.CreatedAt < startOfNextMonth)
                .SumAsync(i => i.TotalAmount, cancellationToken);

            var lastMonthRevenue = await query
                .Where(i => i.CreatedAt >= startOfLastMonth && i.CreatedAt < startOfMonth)
                .SumAsync(i => i.TotalAmount, cancellationToken);

            double growthPercent = 0;
            if (lastMonthRevenue > 0)
            {
                growthPercent = (double)((currentRevenue - lastMonthRevenue) / lastMonthRevenue) * 100;
            }
            else if (currentRevenue > 0)
            {
                growthPercent = 100;
            }

            var totalCustomers = await query
                .Select(i => i.CustomerID)
                .Distinct()
                .CountAsync(cancellationToken);

            var rawTrend = await query
                .Where(i => i.CreatedAt >= sixMonthsAgo)
                .GroupBy(i => new { i.CreatedAt.Year, i.CreatedAt.Month })
                .Select(g => new
                {
                    g.Key.Year,
                    g.Key.Month,
                    Revenue = g.Sum(i => i.TotalAmount),
                    Count = g.Count()
                })
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .ToListAsync(cancellationToken);

            var salesTrend = rawTrend
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .TakeLast(6)
                .Select(t => new SalesTrendDto
                {
                    Year = t.Year,
                    MonthNumber = t.Month,
                    Month = $"T{t.Month:D2}/{t.Year}",
                    Revenue = t.Revenue,
                    InvoiceCount = t.Count,
                    CommissionEarned = 0
                })
                .ToList();

            var debtWatchlist = await query
                .Include(i => i.Customer)
                .Where(i => i.PaymentStatusID == paymentStatusUnpaid && i.PaymentDueDate != null && i.PaymentDueDate < now)
                .Select(i => new
                {
                    i.InvoiceID,
                    InvoiceNumber = i.InvoiceNumber != null ? i.InvoiceNumber.ToString() : string.Empty,
                    i.Customer.CustomerName,
                    i.Customer.ContactPhone,
                    i.Customer.ContactEmail,
                    i.RemainingAmount,
                    DueDate = i.PaymentDueDate.Value
                })
                .ToListAsync(cancellationToken);

            var debtWatchlistMapped = debtWatchlist
                .Select(i =>
                {
                    int overdueDays = (now - i.DueDate).Days;
                    int urgencyOrder = overdueDays >= 30 ? 3 : (overdueDays >= 15 ? 2 : 1);
                    string urgency = overdueDays >= 30 ? "Critical" : (overdueDays >= 15 ? "High" : "Medium");

                    return new
                    {
                        Dto = new DebtWatchlistDto
                        {
                            InvoiceId = i.InvoiceID,
                            InvoiceNumber = i.InvoiceNumber,
                            CustomerName = i.CustomerName,
                            Phone = i.ContactPhone ?? "",
                            Email = i.ContactEmail ?? "",
                            AmountPending = i.RemainingAmount,
                            OverdueDays = overdueDays,
                            UrgencyLevel = urgency
                        },
                        UrgencyOrder = urgencyOrder,
                        OverdueDays = overdueDays
                    };
                })
                .OrderByDescending(x => x.UrgencyOrder)
                .ThenByDescending(x => x.OverdueDays)
                .Take(15)
                .Select(x => x.Dto)
                .ToList();

            var unpaidQuery = query
                .Where(i => i.PaymentStatusID != 3 && i.RemainingAmount > 0);

            var totalUnpaidAmount = await unpaidQuery
                .SumAsync(i => i.RemainingAmount, cancellationToken);

            var totalDebtors = await unpaidQuery
                .Select(i => i.CustomerID)
                .Distinct()
                .CountAsync(cancellationToken);

            var totalOverdueAmount = await unpaidQuery
                .Where(i => i.PaymentDueDate != null && i.PaymentDueDate < now)
                .SumAsync(i => i.RemainingAmount, cancellationToken);

            var overdueCustomerCount = await unpaidQuery
                .Where(i => i.PaymentDueDate != null && i.PaymentDueDate < now)
                .Select(i => i.CustomerID)
                .Distinct()
                .CountAsync(cancellationToken);

            var lastMonthTotalDebt = await unpaidQuery
                .Where(i => i.CreatedAt >= startOfLastMonth && i.CreatedAt < startOfMonth)
                .SumAsync(i => i.RemainingAmount, cancellationToken);

            double debtGrowthPercent = 0;
            if (lastMonthTotalDebt > 0)
            {
                debtGrowthPercent = (double)((totalUnpaidAmount - lastMonthTotalDebt) / lastMonthTotalDebt) * 100;
            }
            else if (totalUnpaidAmount > 0)
            {
                debtGrowthPercent = 100;
            }

            var averageDebtPerCustomer = totalDebtors > 0
                ? totalUnpaidAmount / totalDebtors
                : 0;

            var overdueCriticalDate = now.AddDays(-30);
            var overdueHighDate = now.AddDays(-15);

            var debtByUrgency = await unpaidQuery
                .GroupBy(x => 1)
                .Select(g => new
                {
                    Critical = g.Where(i => i.PaymentDueDate != null
                                            && i.PaymentDueDate.Value <= overdueCriticalDate)
                        .Sum(i => i.RemainingAmount),
                    High = g.Where(i => i.PaymentDueDate != null
                                        && i.PaymentDueDate.Value <= overdueHighDate
                                        && i.PaymentDueDate.Value > overdueCriticalDate)
                        .Sum(i => i.RemainingAmount),
                    Medium = g.Where(i => i.PaymentDueDate == null
                                          || i.PaymentDueDate.Value > overdueHighDate)
                        .Sum(i => i.RemainingAmount)
                })
                .FirstOrDefaultAsync(cancellationToken);

            var requestQuery = _context.InvoiceRequests
                .AsNoTracking()
                .Where(r => r.SaleID == salesPersonId);

            var pendingCount = await requestQuery
                .CountAsync(r => r.RequestStatusID == (int)EInvoiceRequestStatus.Pending, cancellationToken);

            var approvedCount = await requestQuery
                .CountAsync(r => r.RequestStatusID == (int)EInvoiceRequestStatus.Approved
                                 && r.CreatedAt >= startOfMonth
                                 && r.CreatedAt < startOfNextMonth, cancellationToken);

            var rejectedCount = await requestQuery
                .CountAsync(r => r.RequestStatusID == (int)EInvoiceRequestStatus.Rejected
                                 && r.CreatedAt >= startOfMonth
                                 && r.CreatedAt < startOfNextMonth, cancellationToken);
            var issuedCount = await requestQuery
.CountAsync(r => r.RequestStatusID == (int)EInvoiceRequestStatus.Completed
            && r.CreatedAt >= startOfMonth
            && r.CreatedAt < startOfNextMonth, cancellationToken);

            var totalThisMonth = await requestQuery
                .CountAsync(r => r.CreatedAt >= startOfMonth && r.CreatedAt < startOfNextMonth, cancellationToken);

            var recentRequestsRaw = await requestQuery
                .Include(r => r.Customer)
                .Include(r => r.RequestStatus)
                .OrderByDescending(r => r.CreatedAt)
                .Take(5)
                .Select(r => new
                {
                    r.RequestID,
                    CustomerName = r.InvoiceCustomerName ?? r.Customer.CustomerName,
                    r.TotalAmount,
                    StatusName = r.RequestStatus.StatusName,
                    r.RequestStatusID,
                    r.CreatedAt
                })
                .ToListAsync(cancellationToken);

            var recentRequests = recentRequestsRaw
                .Select(r => new InvoiceRequestRecentDto
                {
                    RequestId = r.RequestID,
                    CustomerName = r.CustomerName ?? "Unknown",
                    Amount = r.TotalAmount,
                    Status = !string.IsNullOrWhiteSpace(r.StatusName)
                        ? r.StatusName
                        : Enum.GetName(typeof(EInvoiceRequestStatus), r.RequestStatusID) ?? "Unknown",
                    CreatedDate = r.CreatedAt
                })
                .ToList();

            return new SalesDashboardDto
            {
                CurrentUser = new SalesCurrentUserDto
                {
                    UserId = user?.UserID ?? 0,
                    UserName = user?.FullName ?? user?.Email ?? "Unknown",
                    FullName = user?.FullName ?? "",
                    Role = user?.Role?.RoleName ?? string.Empty,
                    Email = user?.Email ?? string.Empty,
                    SalesId = salesPersonId
                },
                SalesKPIs = new SalesKpiDto
                {
                    CurrentRevenue = currentRevenue,
                    LastMonthRevenue = lastMonthRevenue,
                    RevenueGrowthPercent = Math.Round(growthPercent, 2),
                    TotalCustomers = totalCustomers
                },
                InvoiceRequestStats = new InvoiceRequestStatsDto
                {
                    PendingCount = pendingCount,
                    ApprovedCount = approvedCount,
                    RejectedCount = rejectedCount,
                    IssuedCount = issuedCount,
                    TotalThisMonth = totalThisMonth,
                    RecentRequests = recentRequests
                },
                SalesTrend = salesTrend,
                DebtWatchlist = debtWatchlistMapped,
                TotalCustomerDebt = new TotalCustomerDebtDto
                {
                    TotalDebtors = totalDebtors,
                    TotalUnpaidAmount = totalUnpaidAmount,
                    TotalOverdueAmount = totalOverdueAmount,
                    OverdueCustomerCount = overdueCustomerCount,
                    LastMonthTotalDebt = lastMonthTotalDebt,
                    DebtGrowthPercent = Math.Round(debtGrowthPercent, 2),
                    AverageDebtPerCustomer = averageDebtPerCustomer,
                    DebtByUrgency = new DebtByUrgencyDto
                    {
                        Critical = debtByUrgency?.Critical ?? 0,
                        High = debtByUrgency?.High ?? 0,
                        Medium = debtByUrgency?.Medium ?? 0
                    }
                }
            };
        }
        public async Task<HodDashboardDto> GetHodDashboardStatsAsync(CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var startOfMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var startOfNextMonth = startOfMonth.AddMonths(1);
            var sixMonthsAgo = now.AddMonths(-6);

            const int statusDraft = 1;
            const int statusIssued = 2;
            const int statusCancelled = 3;
            const int statusSent = 9;
            const int statusPendingApproval = 6;

            // A.Tháng hiện tại
            var monthlyData = await _context.Invoices
                .AsNoTracking()
                .Where(i => i.CreatedAt >= startOfMonth)
                .GroupBy(x => 1)
                .Select(g => new
                {
                    // Trừ hóa đơn đã hủy
                    NetRevenue = g.Where(i => i.InvoiceStatusID != statusCancelled && i.InvoiceStatusID != statusDraft).Sum(i => i.TotalAmount),
                    CashCollected = g.Sum(i => i.PaidAmount)
                })
                .FirstOrDefaultAsync(cancellationToken);

            // Tìm nợ xấu (> 90 ngày)
            var criticalDate = now.AddDays(-90);
            var criticalDebts = await _context.Invoices
                .AsNoTracking()
                .Where(i => i.PaymentStatusID != 3 && i.PaymentDueDate != null && i.PaymentDueDate < criticalDate)
                .GroupBy(x => 1)
                .Select(g => new
                {
                    TotalDebt = g.Sum(i => i.RemainingAmount),
                    CustomerCount = g.Select(i => i.CustomerID).Distinct().Count()
                })
                .FirstOrDefaultAsync(cancellationToken);
            var totalDebtAll = await _context.Invoices
                .AsNoTracking()
                .Where(i => i.PaymentStatusID != 3) // != Paid
                .SumAsync(i => i.RemainingAmount, cancellationToken);

            var totalMonthlyRevenue = await _context.Invoices
                .AsNoTracking()
                .Where(i => i.IssuedDate != null
                            && i.IssuedDate >= startOfMonth
                            && i.IssuedDate < startOfNextMonth
                            && (i.InvoiceStatusID == statusIssued
                                || i.InvoiceStatusID == statusSent
                                || i.PaymentStatusID == 3))
                .SumAsync(i => i.TotalAmount, cancellationToken);

            var totalCustomers = await _context.Customers
                .AsNoTracking()
                .CountAsync(cancellationToken);

            var totalInvoiceRequests = await _context.InvoiceRequests
                .AsNoTracking()
                .CountAsync(cancellationToken);

            var totalProducts = await _context.Products
                .AsNoTracking()
                .CountAsync(cancellationToken);

            var totalInvoicesIssued = await _context.Invoices
                .AsNoTracking()
                .CountAsync(i => i.IssuedDate != null
                                 && (i.InvoiceStatusID == statusIssued), cancellationToken);

            var totalInvoicesPendingApproval = await _context.Invoices
                .AsNoTracking()
                .CountAsync(i => i.InvoiceStatusID == statusPendingApproval, cancellationToken);
            var metrics = new HodFinancialMetricsDto
            {
                NetRevenue = monthlyData?.NetRevenue ?? 0,
                CashCollected = monthlyData?.CashCollected ?? 0,
                EstimatedVAT = (monthlyData?.NetRevenue ?? 0) * 0.1m, // 10%
                CriticalDebt = criticalDebts?.TotalDebt ?? 0,
                CriticalDebtCount = criticalDebts?.CustomerCount ?? 0,
                VatRate = 10,
                TotalDebt = totalDebtAll,
            };
            metrics.Outstanding = metrics.NetRevenue - metrics.CashCollected;
            if (metrics.NetRevenue > 0)
                metrics.CollectionRate = Math.Round((double)metrics.CashCollected / (double)metrics.NetRevenue * 100, 2);
            metrics.OutstandingRate = Math.Round((double)metrics.Outstanding / (double)metrics.NetRevenue * 100, 2);

            // B. CASH FLOW (6 Tháng gần nhất)
            var cashFlowRaw = await _context.Invoices
                .AsNoTracking()
                .Where(i => i.IssuedDate >= sixMonthsAgo)
                .GroupBy(i => new { i.IssuedDate.Value.Year, i.IssuedDate.Value.Month })
                .Select(g => new
                {
                    g.Key.Year,
                    g.Key.Month,
                    Invoiced = g.Sum(i => i.TotalAmount),
                    Collected = g.Sum(i => i.PaidAmount)
                })
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .ToListAsync(cancellationToken);

            var cashFlowList = cashFlowRaw.Select(x =>
            {
                decimal outstanding = x.Invoiced - x.Collected;
                double rate = x.Invoiced > 0
                    ? Math.Round(((double)x.Collected / (double)x.Invoiced) * 100, 2)
                    : 0;
                return new CashFlowDto
                {
                    MonthNumber = x.Month,
                    Year = x.Year,
                    Month = $"T{x.Month:D2}/{x.Year}",
                    Invoiced = x.Invoiced,
                    Collected = x.Collected,
                    Outstanding = outstanding,
                    CollectionRate = Math.Round(rate, 2)
                };
            }).ToList();

            // C. DEBT AGING (Phân tích tuổi nợ)
            // Lấy tất cả hóa đơn chưa thanh toán hết để xử lý
            var unpaidInvoices = await _context.Invoices
                .AsNoTracking()
                .Where(i => i.PaymentStatusID != 3) // != Paid
                .Select(i => new { i.RemainingAmount, i.PaymentDueDate, i.CustomerID })
                .ToListAsync(cancellationToken);

            var debtAging = new DebtAgingReportDto
            {
                WithinDue = new DebtBucketDto { Label = "Trong hạn" },
                Overdue1To30 = new DebtBucketDto { Label = "1-30 ngày" },
                Overdue31To60 = new DebtBucketDto { Label = "31-60 ngày" },
                CriticalOverdue60Plus = new DebtBucketDto { Label = "60+ ngày" }
            };

            var customersWithin = new HashSet<int>();
            var customers1_30 = new HashSet<int>();
            var customers31_60 = new HashSet<int>();
            var customers60Plus = new HashSet<int>();

            foreach (var inv in unpaidInvoices)
            {
                if (inv.PaymentDueDate == null || inv.PaymentDueDate >= now)
                {
                    debtAging.WithinDue.Amount += inv.RemainingAmount;
                    customersWithin.Add(inv.CustomerID);
                }
                else
                {
                    int daysOverdue = (now - inv.PaymentDueDate.Value).Days;
                    if (daysOverdue <= 30)
                    {
                        debtAging.Overdue1To30.Amount += inv.RemainingAmount;
                        customers1_30.Add(inv.CustomerID);
                    }
                    else if (daysOverdue <= 60)
                    {
                        debtAging.Overdue31To60.Amount += inv.RemainingAmount;
                        customers31_60.Add(inv.CustomerID);
                    }
                    else
                    {
                        debtAging.CriticalOverdue60Plus.Amount += inv.RemainingAmount;
                        customers60Plus.Add(inv.CustomerID);
                    }
                }
            }

            debtAging.WithinDue.Count = customersWithin.Count;
            debtAging.Overdue1To30.Count = customers1_30.Count;
            debtAging.Overdue31To60.Count = customers31_60.Count;
            debtAging.CriticalOverdue60Plus.Count = customers60Plus.Count;
            metrics.TotalDebtCount = debtAging.WithinDue.Count +
                             debtAging.Overdue1To30.Count +
                             debtAging.Overdue31To60.Count +
                             debtAging.CriticalOverdue60Plus.Count;
            decimal totalDebtValue = unpaidInvoices.Sum(x => x.RemainingAmount);
            if (totalDebtValue > 0)
            {
                debtAging.WithinDue.Percentage = Math.Round(((double)debtAging.WithinDue.Amount / (double)totalDebtValue) * 100, 2);
                debtAging.Overdue1To30.Percentage = Math.Round(((double)debtAging.Overdue1To30.Amount / (double)totalDebtValue) * 100, 2);
                debtAging.Overdue31To60.Percentage = Math.Round(((double)debtAging.Overdue31To60.Amount / (double)totalDebtValue) * 100, 2);
                debtAging.CriticalOverdue60Plus.Percentage = Math.Round(((double)debtAging.CriticalOverdue60Plus.Amount / (double)totalDebtValue) * 100, 2);
            }
            var pendingStatusIds = new[] { 6 };

            var pendingRaw = await _context.Invoices
                .AsNoTracking()
                .Include(i => i.Customer)
                .Include(i => i.OriginalInvoice)
                .Where(i => pendingStatusIds.Contains(i.InvoiceStatusID))
                .OrderBy(i => i.CreatedAt)
                .Take(50)
                .Select(i => new
                {
                    i.InvoiceID,
                    i.InvoiceNumber,
                    CustomerName = i.Customer.CustomerName,
                    i.TotalAmount,
                    i.CreatedAt,
                    TypeId = i.InvoiceType,
                    i.ReferenceNote,
                    RefNumber = i.OriginalInvoice != null ? i.OriginalInvoice.InvoiceNumber : (int?)null
                })
                .ToListAsync(cancellationToken);

            var pendingList = pendingRaw.Select(x =>
            {
                double hours = (now - x.CreatedAt).TotalHours;
                string priority;
                int priorityOrder; // Để sắp xếp

                if (hours > 480) // > 20 ngày
                {
                    priority = "Critical";
                    priorityOrder = 4;
                }
                else if (hours > 240) // > 10 ngày
                {
                    priority = "High";
                    priorityOrder = 3;
                }
                else if (hours > 120) // > 5 ngày
                {
                    priority = "Medium";
                    priorityOrder = 2;
                }
                else
                {
                    priority = "Normal";
                    priorityOrder = 1;
                }
                string GetTypeName(int id) => id switch
                {
                    1 => "Gốc",
                    2 => "Điều chỉnh",
                    3 => "Thay thế",
                    4 => "Hủy",
                    5 => "Giải trình",
                    _ => "Khác"
                };
                string GetTypeBackgroundColor(int id) => id switch
                {
                    1 => "#e3f2fd", // Xanh dương nhạt
                    2 => "#fff4e6", // Cam nhạt
                    3 => "#f3e5f5", // Tím nhạt
                    4 => "#ffebee", // Đỏ nhạt
                    5 => "#e1f5fe", // Xanh trời nhạt
                    _ => "#f5f5f5"  // Xám nhạt
                };
                string GetTypeColor(int id) => id switch
                {
                    1 => "#2196f3", // Blue
                    2 => "#ed6c02", // Orange
                    3 => "#9c27b0", // Purple
                    4 => "#d32f2f", // Red
                    5 => "#757575", // Grey
                    _ => "#000000"
                };
                string GetTypeIcon(int id) => id switch
                {
                    1 => "description",
                    2 => "edit",
                    3 => "swap_horiz",
                    4 => "cancel",
                    5 => "info",
                    _ => "help"
                };
                string GetReasonType(int id) => id switch
                {
                    2 => "adjustment",
                    3 => "replacement",
                    4 => "cancellation",
                    5 => "explanation",
                    _ => "general"
                };
                return new
                {
                    Dto = new PendingInvoiceDto
                    {
                        InvoiceId = x.InvoiceID,
                        InvoiceNumber = x.InvoiceNumber?.ToString() ?? "N/A",
                        CustomerName = x.CustomerName,
                        TotalAmount = x.TotalAmount,
                        CreatedDate = x.CreatedAt,
                        Priority = priority,
                        HoursWaiting = Math.Round(hours, 1),
                        DaysWaiting = (int)(hours / 24),
                        InvoiceType = x.TypeId,
                        TypeName = GetTypeName(x.TypeId),
                        OriginalInvoiceNumber = x.RefNumber?.ToString() ?? "",
                        TypeBackgroundColor = GetTypeBackgroundColor(x.TypeId),
                        Reason = x.ReferenceNote,
                        TypeColor = GetTypeColor(x.TypeId),
                        TypeIcon = GetTypeIcon(x.TypeId),
                        ReasonType = GetReasonType(x.TypeId)
                    },
                    Order = priorityOrder,
                    Created = x.CreatedAt
                };
            })
            .OrderByDescending(p => p.Order) // Priority cao nhất lên đầu
            .ThenBy(p => p.Created)          // Cùng Priority thì ai chờ lâu hơn lên đầu
            .Take(50)
            .Select(p => p.Dto)
            .ToList();
            return new HodDashboardDto
            {
                OverviewStats = new HodOverviewStatsDto
                {
                    TotalMonthlyRevenue = totalMonthlyRevenue,
                    TotalCustomers = totalCustomers,
                    TotalInvoiceRequests = totalInvoiceRequests,
                    TotalProducts = totalProducts,
                    TotalInvoicesIssued = totalInvoicesIssued,
                    TotalInvoicesPendingApproval = totalInvoicesPendingApproval,
                    TotalDebtAll = totalDebtAll
                },
                Financials = metrics,
                CashFlow = cashFlowList,
                DebtAging = debtAging,
                PendingInvoices = pendingList,
                GeneratedAt = now,
                FiscalMonth = now.ToString("yyyy-MM")
            };
        }
        public async Task<AccountantDashboardDto> GetAccountantDashboardAsync(int userId, CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var todayStart = now.Date;
            var sevenDaysAgo = now.AddDays(-7);
            var thirtyDaysAgo = now.AddDays(-30);
            var oneDayAgo = now.AddDays(-1);
            var startOfMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var startOfNextMonth = startOfMonth.AddMonths(1);
            var startOfLastMonth = startOfMonth.AddMonths(-1);

            string? NormalizeInvoiceNumber(long? invoiceNumber) => invoiceNumber.HasValue ? invoiceNumber.Value.ToString() : null;
            string? NormalizeReason(string? reason) => string.IsNullOrWhiteSpace(reason) ? null : reason;
            string GetInvoiceTypeName(int id) => id switch
            {
                1 => "Gốc",
                2 => "Điều chỉnh",
                3 => "Thay thế",
                4 => "Hủy",
                5 => "Giải trình",
                _ => "Khác"
            };

            var user = await _context.Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserID == userId, cancellationToken);

            // 1. Base Query (Chỉ lấy hóa đơn của User này hoặc do User này phụ trách)
            var baseQuery = _context.Invoices.AsNoTracking().Where(i => i.CreatedBy == userId || i.SalesID == userId);

            // B. KPIs
            int statusDraft = 1;
            int statusRejected = 16;
            int statusSent = 9;
            int statusPendingApproval = 6;
            int statusIssued = 2;

            var kpiData = await baseQuery
                .GroupBy(x => 1)
                .Select(g => new
                {
                    Rejected = g.Count(i => i.InvoiceStatusID == statusRejected && i.LastModified >= sevenDaysAgo),
                    Drafts = g.Count(i => i.InvoiceStatusID == statusDraft && i.CreatedAt >= thirtyDaysAgo),
                    SentToday = g.Count(i => i.InvoiceStatusID == statusSent && i.LastModified >= todayStart),
                    PendingApproval = g.Count(i => i.InvoiceStatusID == statusPendingApproval)
                })
                .FirstOrDefaultAsync(cancellationToken);

            var totalMonthlyRevenue = await baseQuery
                .Where(i => i.IssuedDate != null
                            && i.IssuedDate >= startOfMonth
                            && i.IssuedDate < startOfNextMonth
                            && (i.InvoiceStatusID == statusIssued || i.InvoiceStatusID == statusSent || i.PaymentStatusID == 3))
                .SumAsync(i => i.TotalAmount, cancellationToken);

            var totalInvoiceRequests = await _context.InvoiceRequests
                .AsNoTracking()
                .CountAsync(cancellationToken);

            var totalProducts = await _context.Products
                .AsNoTracking()
                .CountAsync(cancellationToken);

            var totalInvoicesIssued = await _context.Invoices
                .AsNoTracking()
                .CountAsync(i => i.IssuedDate != null
                                 && (i.InvoiceStatusID == statusIssued), cancellationToken);

            var totalInvoicesPendingApproval = await _context.Invoices
                .AsNoTracking()
                .CountAsync(i => i.InvoiceStatusID == statusPendingApproval, cancellationToken);

            var totalDebtAll = await _context.Invoices
                .AsNoTracking()
                .Where(i => i.PaymentStatusID != 3)
                .SumAsync(i => i.RemainingAmount, cancellationToken);

            var lastMonthRevenue = await baseQuery
                .Where(i => i.IssuedDate != null
                            && i.IssuedDate >= startOfLastMonth
                            && i.IssuedDate < startOfMonth
                            && (i.InvoiceStatusID == statusIssued || i.InvoiceStatusID == statusSent || i.PaymentStatusID == 3))
                .SumAsync(i => i.TotalAmount, cancellationToken);

            double revenueGrowthPercent = 0;
            if (lastMonthRevenue > 0)
            {
                revenueGrowthPercent = (double)((totalMonthlyRevenue - lastMonthRevenue) / lastMonthRevenue) * 100;
            }
            else if (totalMonthlyRevenue > 0)
            {
                revenueGrowthPercent = 100;
            }

            // C. TASK QUEUE (Ghép 3 nguồn)

            // 1. Priority High: Rejected 7 ngày qua
            var highPriorityRaw = await baseQuery
                .Include(i => i.Customer)
                .Include(i => i.InvoiceStatus)
                .Where(i => i.InvoiceStatusID == statusRejected && i.LastModified >= sevenDaysAgo)
                .OrderByDescending(i => i.LastModified)
                .Select(i => new
                {
                    i.InvoiceID,
                    i.InvoiceNumber,
                    CustomerName = i.Customer.CustomerName,
                    i.TotalAmount,
                    Status = i.InvoiceStatus.StatusName,
                    TaskDate = i.LastModified ?? i.CreatedAt,
                    i.Notes,
                    i.InvoiceType
                })
                .ToListAsync(cancellationToken);

            var highPriority = highPriorityRaw.Select(i => new TaskQueueItemDto
            {
                InvoiceId = i.InvoiceID,
                InvoiceNumber = NormalizeInvoiceNumber(i.InvoiceNumber),
                CustomerName = i.CustomerName,
                Amount = i.TotalAmount,
                Status = i.Status,
                Priority = "High",
                TaskType = "Rejected",
                TaskDate = i.TaskDate,
                Reason = NormalizeReason(i.Notes),
                InvoiceType = i.InvoiceType,
                InvoiceTypeName = GetInvoiceTypeName(i.InvoiceType),
                DaysOld = null
            }).ToList();

            // 2. Priority Medium: Draft cũ tồn đọng (tạo > 1 ngày trước)
            var mediumPriorityRaw = await baseQuery
                .Include(i => i.Customer)
                .Include(i => i.InvoiceStatus)
                .Where(i => i.InvoiceStatusID == statusDraft && i.CreatedAt >= thirtyDaysAgo && i.CreatedAt < oneDayAgo)
                .OrderBy(i => i.CreatedAt)
                .Select(i => new
                {
                    i.InvoiceID,
                    i.InvoiceNumber,
                    CustomerName = i.Customer.CustomerName,
                    i.TotalAmount,
                    Status = i.InvoiceStatus.StatusName,
                    i.CreatedAt,
                    i.Notes,
                    i.InvoiceType
                })
                .ToListAsync(cancellationToken);

            var mediumPriority = mediumPriorityRaw.Select(i => new TaskQueueItemDto
            {
                InvoiceId = i.InvoiceID,
                InvoiceNumber = NormalizeInvoiceNumber(i.InvoiceNumber),
                CustomerName = i.CustomerName,
                Amount = i.TotalAmount,
                Status = i.Status,
                Priority = "Medium",
                TaskType = "Old Draft",
                TaskDate = i.CreatedAt,
                Reason = NormalizeReason(i.Notes),
                InvoiceType = i.InvoiceType,
                InvoiceTypeName = GetInvoiceTypeName(i.InvoiceType),
                DaysOld = (int)(now - i.CreatedAt).TotalDays
            }).ToList();

            // 3. Priority Low: Quá hạn > 30 ngày (Max 20)
            var lowPriorityTotal = await baseQuery
                .Where(i => i.PaymentStatusID != 3 && i.PaymentDueDate < thirtyDaysAgo)
                .CountAsync(cancellationToken);

            var lowPriorityRaw = await baseQuery
                .Include(i => i.Customer)
                .Include(i => i.InvoiceStatus)
                .Where(i => i.PaymentStatusID != 3 && i.PaymentDueDate < thirtyDaysAgo)
                .OrderBy(i => i.PaymentDueDate)
                .Take(20)
                .Select(i => new
                {
                    i.InvoiceID,
                    i.InvoiceNumber,
                    CustomerName = i.Customer.CustomerName,
                    i.TotalAmount,
                    TaskDate = i.PaymentDueDate ?? i.CreatedAt,
                    i.PaymentDueDate,
                    i.InvoiceType
                })
                .ToListAsync(cancellationToken);

            var lowPriority = lowPriorityRaw.Select(i => new TaskQueueItemDto
            {
                InvoiceId = i.InvoiceID,
                InvoiceNumber = NormalizeInvoiceNumber(i.InvoiceNumber),
                CustomerName = i.CustomerName,
                Amount = i.TotalAmount,
                Status = "Overdue",
                Priority = "Low",
                TaskType = "Debt Collection",
                TaskDate = i.TaskDate,
                Reason = $"Quá hạn {(now - i.PaymentDueDate!.Value).Days} ngày",
                InvoiceType = i.InvoiceType,
                InvoiceTypeName = GetInvoiceTypeName(i.InvoiceType),
                DaysOld = null
            }).ToList();

            // Gộp List: High -> Medium -> Low
            var taskQueue = new List<TaskQueueItemDto>();
            taskQueue.AddRange(highPriority);
            taskQueue.AddRange(mediumPriority);
            taskQueue.AddRange(lowPriority);
            var taskQueueTotal = highPriority.Count + mediumPriority.Count + lowPriorityTotal;
            taskQueue = taskQueue.Take(50).ToList();

            var urgentTasks = taskQueue.Count(t => (now - t.TaskDate).TotalHours > 24);

            var requestQuery = _context.InvoiceRequests.AsNoTracking();

            var pendingCount = await requestQuery
                .CountAsync(r => r.RequestStatusID == (int)EInvoiceRequestStatus.Pending, cancellationToken);

            var processedCount = await requestQuery
                .CountAsync(r => (r.RequestStatusID == (int)EInvoiceRequestStatus.Approved
                                  || r.RequestStatusID == (int)EInvoiceRequestStatus.Completed)
                                 && r.CreatedAt >= startOfMonth
                                 && r.CreatedAt < startOfNextMonth, cancellationToken);

            var rejectedCount = await requestQuery
                .CountAsync(r => r.RequestStatusID == (int)EInvoiceRequestStatus.Rejected
                                 && r.CreatedAt >= startOfMonth
                                 && r.CreatedAt < startOfNextMonth, cancellationToken);

            var totalThisMonth = await requestQuery
                .CountAsync(r => r.CreatedAt >= startOfMonth && r.CreatedAt < startOfNextMonth, cancellationToken);

            var recentRequestsRaw = await requestQuery
                .Include(r => r.Customer)
                .Include(r => r.RequestStatus)
                .OrderByDescending(r => r.CreatedAt)
                .Take(5)
                .Select(r => new
                {
                    r.RequestID,
                    CustomerName = r.InvoiceCustomerName ?? r.Customer.CustomerName,
                    r.TotalAmount,
                    StatusName = r.RequestStatus.StatusName,
                    r.RequestStatusID,
                    r.CreatedAt
                })
                .ToListAsync(cancellationToken);

            var recentRequests = recentRequestsRaw
                .Select(r => new AccountantInvoiceRequestRecentDto
                {
                    RequestId = r.RequestID,
                    CustomerName = r.CustomerName ?? "Unknown",
                    TotalAmount = r.TotalAmount,
                    Status = !string.IsNullOrWhiteSpace(r.StatusName)
                        ? r.StatusName
                        : Enum.GetName(typeof(EInvoiceRequestStatus), r.RequestStatusID) ?? "Unknown",
                    CreatedDate = r.CreatedAt,
                    DaysWaiting = (now - r.CreatedAt).Days
                })
                .ToList();

            var monthlyDebtQuery = _context.Invoices
                .AsNoTracking()
                .Where(i => i.IssuedDate != null
                            && i.IssuedDate >= startOfMonth
                            && i.IssuedDate < startOfNextMonth
                            && i.PaymentStatusID != 3
                            && i.RemainingAmount > 0);

            var totalUnpaidAmount = await monthlyDebtQuery
                .SumAsync(i => i.RemainingAmount, cancellationToken);

            var totalDebtors = await monthlyDebtQuery
                .Select(i => i.CustomerID)
                .Distinct()
                .CountAsync(cancellationToken);

            var totalOverdueAmount = await monthlyDebtQuery
                .Where(i => i.PaymentDueDate != null && i.PaymentDueDate < now)
                .SumAsync(i => i.RemainingAmount, cancellationToken);

            var overdueCustomerCount = await monthlyDebtQuery
                .Where(i => i.PaymentDueDate != null && i.PaymentDueDate < now)
                .Select(i => i.CustomerID)
                .Distinct()
                .CountAsync(cancellationToken);

            var lastMonthTotalDebt = await _context.Invoices
                .AsNoTracking()
                .Where(i => i.IssuedDate != null
                            && i.IssuedDate >= startOfLastMonth
                            && i.IssuedDate < startOfMonth
                            && i.PaymentStatusID != 3
                            && i.RemainingAmount > 0)
                .SumAsync(i => i.RemainingAmount, cancellationToken);

            double debtGrowthPercent = 0;
            if (lastMonthTotalDebt > 0)
            {
                debtGrowthPercent = (double)((totalUnpaidAmount - lastMonthTotalDebt) / lastMonthTotalDebt) * 100;
            }
            else if (totalUnpaidAmount > 0)
            {
                debtGrowthPercent = 100;
            }

            var averageDebtPerCustomer = totalDebtors > 0
                ? totalUnpaidAmount / totalDebtors
                : 0;

            var overdueCriticalDate = now.AddDays(-30);
            var overdueHighDate = now.AddDays(-15);

            var debtByUrgency = await monthlyDebtQuery
                .GroupBy(x => 1)
                .Select(g => new
                {
                    Critical = g.Where(i => i.PaymentDueDate != null
                                            && i.PaymentDueDate.Value <= overdueCriticalDate)
                        .Sum(i => i.RemainingAmount),
                    High = g.Where(i => i.PaymentDueDate != null
                                        && i.PaymentDueDate.Value <= overdueHighDate
                                        && i.PaymentDueDate.Value > overdueCriticalDate)
                        .Sum(i => i.RemainingAmount),
                    Medium = g.Where(i => i.PaymentDueDate == null
                                          || i.PaymentDueDate.Value > overdueHighDate)
                        .Sum(i => i.RemainingAmount)
                })
                .FirstOrDefaultAsync(cancellationToken);

            // D. RECENT INVOICES (Lịch sử làm việc)
            var recentWork = await baseQuery
                .Include(i => i.Customer)
                .Include(i => i.InvoiceStatus)
                .Where(i => i.CreatedAt >= sevenDaysAgo)
                .OrderByDescending(i => i.CreatedAt)
                .Take(20)
                .Select(i => new RecentWorkDto
                {
                    InvoiceId = i.InvoiceID,
                    InvoiceNumber = i.InvoiceNumber.HasValue ? i.InvoiceNumber.Value.ToString() : null,
                    CustomerName = i.Customer.CustomerName,
                    Status = i.InvoiceStatus.StatusName,
                    TotalAmount = i.TotalAmount,
                    CreatedAt = i.CreatedAt
                })
                .ToListAsync(cancellationToken);

            var recentInvoicesTotal = await baseQuery
                .Where(i => i.CreatedAt >= sevenDaysAgo)
                .CountAsync(cancellationToken);

            // E. ASSEMBLE
            return new AccountantDashboardDto
            {
                CurrentUser = new CurrentUserDto
                {
                    UserId = user?.UserID ?? 0,
                    FullName = user?.FullName ?? "Unknown",
                    Role = user?.Role?.RoleName ?? "",
                    Email = user?.Email ?? "",
                },
                OverviewStats = new AccountantOverviewStatsDto
                {
                    TotalMonthlyRevenue = totalMonthlyRevenue,
                    TotalInvoiceRequests = totalInvoiceRequests,
                    TotalProducts = totalProducts,
                    TotalInvoicesIssued = totalInvoicesIssued,
                    TotalInvoicesPendingApproval = totalInvoicesPendingApproval,
                    TotalDebtAll = totalDebtAll
                },
                Kpis = new AccountantKpiDto
                {
                    RejectedCount = kpiData?.Rejected ?? 0,
                    DraftsCount = kpiData?.Drafts ?? 0,
                    SentToday = kpiData?.SentToday ?? 0,
                    TotalMonthlyRevenue = totalMonthlyRevenue,
                    LastMonthRevenue = lastMonthRevenue,
                    RevenueGrowthPercent = Math.Round(revenueGrowthPercent, 2),
                    PendingApproval = kpiData?.PendingApproval ?? 0,
                    UrgentTasks = urgentTasks
                },
                InvoiceRequestStats = new AccountantInvoiceRequestStatsDto
                {
                    PendingCount = pendingCount,
                    ProcessedCount = processedCount,
                    RejectedCount = rejectedCount,
                    TotalThisMonth = totalThisMonth,
                    RecentRequests = recentRequests
                },
                TotalMonthlyDebt = new AccountantTotalMonthlyDebtDto
                {
                    TotalDebtors = totalDebtors,
                    TotalUnpaidAmount = totalUnpaidAmount,
                    TotalOverdueAmount = totalOverdueAmount,
                    OverdueCustomerCount = overdueCustomerCount,
                    LastMonthTotalDebt = lastMonthTotalDebt,
                    DebtGrowthPercent = Math.Round(debtGrowthPercent, 2),
                    AverageDebtPerCustomer = averageDebtPerCustomer,
                    DebtByUrgency = new AccountantDebtByUrgencyDto
                    {
                        Critical = debtByUrgency?.Critical ?? 0,
                        High = debtByUrgency?.High ?? 0,
                        Medium = debtByUrgency?.Medium ?? 0
                    }
                },
                TaskQueue = taskQueue,
                RecentInvoices = recentWork,
                TaskQueueTotal = taskQueueTotal,
                RecentInvoicesTotal = recentInvoicesTotal,
                GeneratedAt = now
            };
        }
    }
}
