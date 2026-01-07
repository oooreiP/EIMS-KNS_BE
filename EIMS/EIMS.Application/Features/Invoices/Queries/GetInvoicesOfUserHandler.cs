using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.Invoices;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.Invoices.Queries
{
    public class GetInvoicesOfUserHandler : IRequestHandler<GetInvoicesOfUser, Result<PaginatedList<InvoiceDTO>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetInvoicesOfUserHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedList<InvoiceDTO>>> Handle(GetInvoicesOfUser request, CancellationToken cancellationToken)
        {
            // 1. Fetch User with Role
            var user = await _uow.UserRepository.GetAllQueryable()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserID == request.AuthenticatedUserId, cancellationToken);

            if (user == null) return Result.Fail("User not found.");

            var query = _uow.InvoicesRepository.GetAllQueryable(
                includeProperties: "Customer,InvoiceStatus,InvoiceItems.Product,PaymentStatus,TaxApiLogs.TaxApiStatus,OriginalInvoice"
            );
            string role = user.Role?.RoleName ?? "";

            // 2. APPLY PERMISSIONS
            switch (role)
            {
                case "HOD":
                case "Accountant":
                case "Admin":
                case "Director":
                    // "HOD and Accountant can see all the invoices"
                    // No filter applied. They see everything.
                    break;

                case "Sale":
                    // "Sales just can see the invoice they sale"
                    // We filter by the SalesID column we just added
                    query = query.Where(x => x.SalesID == user.UserID);
                    break;

                case "Customer":
                    if (user.CustomerID == null)
                        return Result.Fail("This account is not linked to any customer.");

                    query = query.Where(x => x.CustomerID == user.CustomerID.Value);
                    break;

                default:
                    // Safety Fallback: Unknown roles see NOTHING or only their own created items
                    return Result.Ok(new PaginatedList<InvoiceDTO>(new List<InvoiceDTO>(), 0, request.PageNumber, request.PageSize));
            }
            // A. Date Range (Using CreatedAt, but you could use IssuedDate)
            if (request.FromDate.HasValue)
            {
                // Ensure we compare start of day
                var fromDate = request.FromDate.Value.Date;
                query = query.Where(x => x.CreatedAt >= fromDate);
            }
            if (request.ToDate.HasValue)
            {
                // Ensure we compare end of day
                var toDate = request.ToDate.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(x => x.CreatedAt <= toDate);
            }

            // B. Statuses
            if (request.InvoiceStatusId.HasValue)
            {
                query = query.Where(x => x.InvoiceStatusID == request.InvoiceStatusId.Value);
            }
            if (request.PaymentStatusId.HasValue)
            {
                query = query.Where(x => x.PaymentStatusID == request.PaymentStatusId.Value);
            }

            // C. Amount Range
            if (request.MinAmount.HasValue)
            {
                query = query.Where(x => x.TotalAmount >= request.MinAmount.Value);
            }
            if (request.MaxAmount.HasValue)
            {
                query = query.Where(x => x.TotalAmount <= request.MaxAmount.Value);
            }
            // 3. Search Logic
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string search = request.SearchTerm.ToLower().Trim();
                query = query.Where(x => 
                    x.InvoiceNumber.ToString().Contains(search) || 
                    (x.Notes != null && x.Notes.ToLower().Contains(search)) ||
                    
                    // Search Snapshot Name (if exists) OR Master Customer Name
                    (x.InvoiceCustomerName != null && x.InvoiceCustomerName.ToLower().Contains(search)) ||
                    (x.Customer.CustomerName.ToLower().Contains(search))
                );
            }

           // 5. SORTING
            query = _uow.InvoicesRepository.ApplySorting(query, request.SortColumn, request.SortDirection);

            // 6. PAGINATION
            var dtoQuery = query.ProjectTo<InvoiceDTO>(_mapper.ConfigurationProvider);
            
            return await PaginatedList<InvoiceDTO>.CreateAsync(
                dtoQuery, 
                request.PageNumber, 
                request.PageSize
            );
        }
    }
}