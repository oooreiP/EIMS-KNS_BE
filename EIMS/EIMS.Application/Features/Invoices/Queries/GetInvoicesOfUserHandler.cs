using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions; // Important for ProjectTo
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.Invoices;
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
            // 1. Fetch the logged-in User to get their Link to the Customer
            // (Even though you added the Claim, looking it up here is safer to ensure the user wasn't just disabled)
            var user = await _uow.UserRepository.GetByIdAsync(request.AuthenticatedUserId);

            if (user == null)
            {
                return Result.Fail("User not found.");
            }

            // 2. Check if this User is actually a Customer
            if (user.CustomerID == null)
            {
                // If they are not linked to a customer (e.g. an Admin logged in), 
                // you might want to return empty or handle differently. 
                // For now, we return an empty list for safety.
                return Result.Ok(new PaginatedList<InvoiceDTO>(new List<InvoiceDTO>(), 0, request.PageNumber, request.PageSize));
            }

            // 3. Start building the Query
            var query = _uow.InvoicesRepository.GetAllQueryable(); // Assuming your Repo has this or similar

            // 4. CRITICAL: Filter by the User's CustomerID
            query = query.Where(x => x.CustomerID == user.CustomerID.Value);

            // 5. Apply Search (Optional)
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string search = request.SearchTerm.ToLower().Trim();
                
                // Search by Invoice Number, Snapshot Name, or Notes
                query = query.Where(x => 
                    x.InvoiceNumber.ToString().Contains(search) || 
                    (x.InvoiceCustomerName != null && x.InvoiceCustomerName.ToLower().Contains(search)) ||
                    (x.Notes != null && x.Notes.ToLower().Contains(search))
                );
            }

            // 6. Ordering (Newest first)
            query = query.OrderByDescending(x => x.CreatedAt);

            // 7. Map to DTO
            // Ensure you have ProjectTo (requires AutoMapper.QueryableExtensions)
            var dtoQuery = query.ProjectTo<InvoiceDTO>(_mapper.ConfigurationProvider);

            // 8. Paginate
            var paginatedResult = await PaginatedList<InvoiceDTO>.CreateAsync(
                dtoQuery, 
                request.PageNumber, 
                request.PageSize
            );

            return Result.Ok(paginatedResult);
        }
    }
}