using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.Customer;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.Customers.Queries
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, Result<PaginatedList<CustomerDto>>>
    {
        private readonly IApplicationDBContext _context;
        private readonly IMapper _mapper;

        public GetCustomersQueryHandler(IApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedList<CustomerDto>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            // 1. Start with the base query (NoTracking for read performance)
            var query = _context.Customers.AsNoTracking();

            // 2. Apply Search Filter (if provided)
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                string term = request.SearchTerm.ToLower().Trim();
                query = query.Where(c =>
                    c.CustomerName.ToLower().Contains(term) ||
                    c.TaxCode.Contains(term) ||
                    c.ContactEmail.ToLower().Contains(term) ||
                    (c.ContactPhone != null && c.ContactPhone.Contains(term))
                );
            }

            // 2.1 Apply Active Filter (if provided)
            if (request.IsActive.HasValue)
            {
                query = query.Where(c => c.IsActive == request.IsActive.Value);
            }

            // 3. Order by most recently added (or by ID)
            query = query.OrderByDescending(c => c.CustomerID);

            // 4. Project to DTO using AutoMapper
            var projectQuery = query.ProjectTo<CustomerDto>(_mapper.ConfigurationProvider);

            // 5. Execute Pagination
            var paginatedList = await PaginatedList<CustomerDto>.CreateAsync(
                projectQuery,
                request.PageNumber,
                request.PageSize
            );
            return Result.Ok(paginatedList);
        }
    }
}
