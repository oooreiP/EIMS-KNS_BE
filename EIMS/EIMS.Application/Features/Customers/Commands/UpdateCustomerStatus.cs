using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Customers.Commands.UpdateCustomerStatus
{
    public class UpdateCustomerStatusCommandHandler : IRequestHandler<UpdateCustomerStatusCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateCustomerStatusCommand request, CancellationToken cancellationToken)
        {
            // 1. Get the customer
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerId);

            // 2. Check if exists
            if (customer == null)
            {
                return Result.Fail(new Error($"Customer with ID {request.CustomerId} not found")
                    .WithMetadata("ErrorCode", "Customer.Status.NotFound"));
            }

            // 3. Update status
            customer.IsActive = request.IsActive;

            // 4. Save changes
            await _unitOfWork.CustomerRepository.UpdateAsync(customer);
            await _unitOfWork.SaveChanges();

            return Result.Ok();
        }
    }
}