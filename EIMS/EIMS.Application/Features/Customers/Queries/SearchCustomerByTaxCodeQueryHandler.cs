using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Customer;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Customers.Queries
{
    public class SearchCustomerByTaxCodeQueryHandler
    : IRequestHandler<SearchCustomerByTaxCodeQuery, Result<CustomerDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExternalCompanyLookupService _lookupService;

        public SearchCustomerByTaxCodeQueryHandler(IUnitOfWork unitOfWork, IExternalCompanyLookupService lookupService)
        {
            _unitOfWork = unitOfWork;
            _lookupService = lookupService;
        }

        public async Task<Result<CustomerDto>> Handle(SearchCustomerByTaxCodeQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.TaxCode))
                return Result.Fail(new Error("Mã số thuế không được để trống").WithMetadata("ErrorCode", "Customer.EmptyTaxCode"));

            var customer = await _unitOfWork.CustomerRepository.GetByTaxCodeAsync(request.TaxCode.Trim());
            if (customer != null)
            {
                return Result.Ok(new CustomerDto
                {
                    CustomerID = customer.CustomerID,
                    CustomerName = customer.CustomerName,
                    TaxCode = customer.TaxCode,
                    Address = customer.Address,
                    ContactEmail = customer.ContactEmail,
                    ContactPerson = customer.ContactPerson,
                    ContactPhone = customer.ContactPhone
                });
            }
            var lookupRes = await _lookupService.LookupByTaxCodeAsync(request.TaxCode);
            if (lookupRes.IsFailed)
                return lookupRes.ToResult<CustomerDto>(); // convert fail to same type

            var data = lookupRes.Value;
            // 3️⃣ Lưu vào DB
            var newCust = new Domain.Entities.Customer
            {
                CustomerName = data.CompanyName,
                TaxCode = data.TaxCode,
                Address = data.Address,
                ContactEmail = "",     
                ContactPerson = null,
                ContactPhone = null
            };
            var added = await _unitOfWork.CustomerRepository.CreateAsync(newCust);
            return Result.Ok(new CustomerDto
            {
                CustomerID = added.CustomerID,
                CustomerName = added.CustomerName,
                TaxCode = added.TaxCode,
                Address = added.Address,
                ContactEmail = added.ContactEmail,
                ContactPerson = added.ContactPerson,
                ContactPhone = added.ContactPhone
            });
        }
    }
}
