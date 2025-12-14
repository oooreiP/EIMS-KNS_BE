using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Customer;
using EIMS.Application.DTOs.InvoicePayment;
using EIMS.Application.DTOs.Invoices;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.Customers.Queries
{
    public class GetCustomerDebtDetailQueryHandler : IRequestHandler<GetCustomerDebtDetailQuery, Result<CustomerDebtDetailDto>>
    {
        private readonly IUnitOfWork _uow;

        public GetCustomerDebtDetailQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<CustomerDebtDetailDto>> Handle(GetCustomerDebtDetailQuery request, CancellationToken cancellationToken)
        {
            // 1. Fetch Customer with Invoices and Payments
            // We need to 'Include' everything to do the calculations
            var customer = await _uow.CustomerRepository
                .GetAllQueryable()
                .Include(c => c.Invoices)
                    .ThenInclude(i => i.Payments)
                .Include(c => c.Invoices)
                    .ThenInclude(i => i.InvoiceStatus) // To check if Draft
                .FirstOrDefaultAsync(c => c.CustomerID == request.CustomerId, cancellationToken);

            if (customer == null)
            {
                return Result.Fail($"Customer with ID {request.CustomerId} not found.");
            }

            // 2. Filter Valid Invoices (Ignore Drafts - StatusID 1)
            // Assuming 1 = Draft
            var validInvoices = customer.Invoices.Where(i => i.InvoiceStatusID != 1).ToList();
            var userIds = validInvoices
        .SelectMany(i => i.Payments)
        .Where(p => p.CreatedBy.HasValue)
        .Select(p => p.CreatedBy.Value)
        .Distinct()
        .ToList();

            // Fetch "ID -> Name" dictionary from the database
            // Note: If _uow.UserRepository doesn't support this directly, use _context.Users
            var userNames = await _uow.UserRepository.GetAllQueryable()
                .Where(u => userIds.Contains(u.UserID))
                .ToDictionaryAsync(u => u.UserID, u => u.FullName, cancellationToken);
            // 3. Calculate Aggregates
            decimal totalPaid = validInvoices.Sum(i => i.Payments.Sum(p => p.AmountPaid));
            decimal totalInvoiceAmount = validInvoices.Sum(i => i.TotalAmount);
            decimal totalDebt = totalInvoiceAmount - totalPaid;

            // Calculate Overdue Debt (Only count the remaining balance of overdue invoices)
            var overdueInvoices = validInvoices
                .Where(i => (i.PaymentDueDate ?? i.CreatedAt.AddDays(30)) < DateTime.UtcNow)
                .Where(i => i.TotalAmount > i.Payments.Sum(p => p.AmountPaid))
                .ToList();

            decimal overdueDebt = overdueInvoices.Sum(i => i.TotalAmount - i.Payments.Sum(p => p.AmountPaid));

            var lastPayment = validInvoices
                .SelectMany(i => i.Payments)
                .OrderByDescending(p => p.PaymentDate)
                .FirstOrDefault();

            // 4. Build Response
            var response = new CustomerDebtDetailDto
            {
                // A. Customer Info
                Customer = new CustomerInfoDto
                {
                    CustomerId = customer.CustomerID,
                    CustomerName = customer.CustomerName,
                    TaxCode = customer.TaxCode,
                    Email = customer.ContactEmail,
                    Phone = customer.ContactPhone,
                    Address = customer.Address
                },

                // B. Summary
                Summary = new DebtSummaryDto
                {
                    TotalDebt = totalDebt,
                    OverdueDebt = overdueDebt,
                    TotalPaid = totalPaid,
                    InvoiceCount = validInvoices.Count,
                    UnpaidInvoiceCount = validInvoices.Count(i => (i.TotalAmount - i.Payments.Sum(p => p.AmountPaid)) > 0),
                    LastPaymentDate = lastPayment?.PaymentDate
                },

                // C. Unpaid Invoices List
                UnpaidInvoices = validInvoices
                    .Where(i => (i.TotalAmount - i.Payments.Sum(p => p.AmountPaid)) > 0) // Filter only unpaid
                    .Select(i =>
                    {
                        var paid = i.Payments.Sum(p => p.AmountPaid);
                        var remaining = i.TotalAmount - paid;
                        var isOverdue = (i.PaymentDueDate ?? i.CreatedAt.AddDays(30)) < DateTime.UtcNow;

                        // Determine Status String
                        string status = "Unpaid";
                        if (isOverdue) status = "Overdue";
                        else if (paid > 0) status = "PartiallyPaid";

                        return new UnpaidInvoiceDto
                        {
                            InvoiceId = i.InvoiceID,
                            InvoiceNumber = i.InvoiceNumber.ToString(), // Or your formatted code
                            InvoiceDate = i.IssuedDate ?? i.CreatedAt,
                            DueDate = i.PaymentDueDate ?? i.CreatedAt.AddDays(30),
                            TotalAmount = i.TotalAmount,
                            PaidAmount = paid,
                            RemainingAmount = remaining,
                            PaymentStatus = status,
                            Description = i.Notes ?? $"Invoice #{i.InvoiceNumber}",
                            IsOverdue = isOverdue
                        };
                    })
                    .OrderBy(i => i.DueDate) // Oldest debt first
                    .ToList(),

                // D. Payment History (Flattened list of all payments)
                PaymentHistory = validInvoices
            .SelectMany(i => i.Payments)
            .OrderByDescending(p => p.PaymentDate)
            .Take(20)
            .Select(p => new PaymentHistoryDto
            {
                PaymentId = p.PaymentID,
                InvoiceId = p.InvoiceID,
                InvoiceNumber = p.Invoice.InvoiceNumber.ToString(),
                Amount = p.AmountPaid,
                PaymentMethod = p.PaymentMethod,
                TransactionCode = p.TransactionCode,
                Note = p.Note,
                PaymentDate = p.PaymentDate,
                UserId = p.CreatedBy,
                UserName = (p.CreatedBy.HasValue && userNames.ContainsKey(p.CreatedBy.Value))
                           ? userNames[p.CreatedBy.Value]
                           : "System"
            })
            .ToList()
            };

            return Result.Ok(response);
        }
    }
}