using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Customers.Commands
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            // 1. Check if TaxCode already exists (if provided)
            if (!string.IsNullOrWhiteSpace(request.TaxCode))
            {
                var existingCustomer = await _unitOfWork.CustomerRepository.GetByTaxCodeAsync(request.TaxCode);
                if (existingCustomer != null)
                {
                    return Result.Fail(new Error($"Customer with Tax Code {request.TaxCode} already exists.")
                        .WithMetadata("ErrorCode", "Customer.Create.DuplicateTaxCode"));
                }
            }

            // 2. Map Command to Entity
            var customer = new Customer
            {
                CustomerName = request.CustomerName,
                TaxCode = request.TaxCode ?? string.Empty,
                Address = request.Address,
                SaleID = request.SaleID,
                ContactEmail = request.ContactEmail,
                ContactPerson = request.ContactPerson,
                ContactPhone = request.ContactPhone,
                IsActive = request.IsActive ?? true
            };

            // 3. Save to Database
            // The CustomerRepository.CreateCustomerAsync implementation already calls SaveChangesAsync
            await _unitOfWork.CustomerRepository.CreateCustomerAsync(customer);

            return Result.Ok(customer.CustomerID);
        }
    }
}