using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Products.Commands
{
    public class UpdateProductStatusCommandHandler : IRequestHandler<UpdateProductStatusCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateProductStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(UpdateProductStatusCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.ProductID);
            if (product == null)
            {
                return Result.Fail(new Error($"Product with ID {request.ProductID} not found")
                    .WithMetadata("ErrorCode", "Product.NotFound"));
            }
            if (product.IsActive == request.IsActive)
            {
                return Result.Ok();
            }

            product.IsActive = request.IsActive;
            await _unitOfWork.ProductRepository.UpdateAsync(product);
            await _unitOfWork.SaveChanges();
            return Result.Ok();
        }
    }
}