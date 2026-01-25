using AutoMapper;
using AutoMapper.QueryableExtensions;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.Customer;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Customers.Queries
{
    public class GetCustomersBySaleIdQueryHandler : IRequestHandler<GetCustomersBySaleIdQuery, Result<PaginatedList<CustomerDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetCustomersBySaleIdQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedList<CustomerDto>>> Handle(GetCustomersBySaleIdQuery request, CancellationToken cancellationToken)
        {

            var query = _uow.CustomerRepository.GetAllQueryable()
                .AsNoTracking()
                .Where(x => x.SaleID == request.SaleID);
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                string term = request.SearchTerm.Trim().ToLower();
                query = query.Where(x =>
                    x.CustomerName.ToLower().Contains(term) ||
                    x.ContactPerson.ToLower().Contains(term) ||
                    x.TaxCode.Contains(term) ||
                    x.Address.Contains(term) ||
                    x.ContactPhone.Contains(term));
            }
            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            // 5. Trả về kết quả
            var pagedResult = new PaginatedList<CustomerDto>(items, totalCount, request.PageIndex, request.PageSize);
            return Result.Ok(pagedResult);
        }
    }
}
