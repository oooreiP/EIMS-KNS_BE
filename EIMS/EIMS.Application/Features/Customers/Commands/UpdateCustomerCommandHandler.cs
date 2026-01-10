using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Customers.Commands
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            // 1. Tìm khách hàng trong DB theo ID
            // Giả sử repository của bạn có hàm GetByIdAsync
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerID);

            if (customer == null)
            {
                return Result.Fail(new Error($"Customer with ID {request.CustomerID} not found.")
                             .WithMetadata("ErrorCode", "Customer.Update.NotFound"));
            }
            string newTaxCode = request.TaxCode ?? string.Empty;
            if (!string.Equals(customer.TaxCode, newTaxCode, StringComparison.OrdinalIgnoreCase)
                && !string.IsNullOrWhiteSpace(newTaxCode))
            {
                var existingWithTaxCode = await _unitOfWork.CustomerRepository.GetByTaxCodeAsync(newTaxCode);
                if (existingWithTaxCode != null)
                {
                    return Result.Fail(new Error($"Tax Code {newTaxCode} is already used by another customer.")
                        .WithMetadata("ErrorCode", "Customer.Update.DuplicateTaxCode"));
                }
            }
            customer.CustomerName = request.CustomerName;
            customer.TaxCode = newTaxCode;
            customer.Address = request.Address;
            customer.ContactEmail = request.ContactEmail;
            customer.ContactPerson = request.ContactPerson;
            customer.ContactPhone = request.ContactPhone;
            if (request.IsActive.HasValue)
            {
                customer.IsActive = request.IsActive.Value;
            }
            await _unitOfWork.CustomerRepository.UpdateAsync(customer);
            await _unitOfWork.SaveChanges();

            return Result.Ok();
        }
    }
}
