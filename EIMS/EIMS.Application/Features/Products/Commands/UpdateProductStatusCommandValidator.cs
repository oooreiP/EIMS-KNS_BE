using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace EIMS.Application.Features.Products.Commands
{
    public class UpdateProductStatusCommandValidator : AbstractValidator<UpdateProductStatusCommand>
    {
        public UpdateProductStatusCommandValidator()
        {
            RuleFor(x => x.ProductID)
                .GreaterThan(0)
                .WithMessage("Product ID must be greater than 0.");
        }
    }
}