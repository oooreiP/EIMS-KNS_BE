using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Customer;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Customers.Queries
{
    public class SearchCustomerQueryHandler : IRequestHandler<SearchCustomerQuery, Result<List<CustomerDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchCustomerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<CustomerDto>>> Handle(SearchCustomerQuery request, CancellationToken cancellationToken)
        {
            var customers = await _unitOfWork.CustomerRepository.SearchAsync(request.SearchTerm);
            var customerDtos = customers.Select(c => new CustomerDto
            {
                CustomerID = c.CustomerID,
                CustomerName = c.CustomerName,
                TaxCode = c.TaxCode,
                Address = c.Address,
                ContactEmail = c.ContactEmail,
                ContactPerson = c.ContactPerson,
                ContactPhone = c.ContactPhone
            }).ToList();
            return Result.Ok(customerDtos);
        }
    }
}