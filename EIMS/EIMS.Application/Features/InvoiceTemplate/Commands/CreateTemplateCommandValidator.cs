using EIMS.Application.Commons.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.InvoiceTemplate.Commands
{
    public class CreateTemplateCommandValidator : AbstractValidator<CreateTemplateCommand>
    {
        private readonly IApplicationDBContext _context;

        public CreateTemplateCommandValidator(IApplicationDBContext context)
        {
            _context = context;

            RuleFor(v => v.TemplateName)
                .NotEmpty().WithMessage("TemplateName is required.")
                .MaximumLength(255).WithMessage("TemplateName must not exceed 255 characters.");

            // This is the validation you asked about.
            // It runs BEFORE the handler.
            RuleFor(v => v.SerialID)
                .MustAsync(SerialMustExist)
                .WithMessage("Invalid SerialID. The 'Ký hiệu' (Serial) does not exist.");

            RuleFor(v => v.TemplateTypeID)
                .MustAsync(TemplateTypeMustExist)
                .WithMessage("Invalid TemplateTypeID. The template type does not exist.");
        }

        private async Task<bool> SerialMustExist(int id, CancellationToken cancellationToken)
        {
            return await _context.Serials.AnyAsync(s => s.SerialID == id, cancellationToken);
        }

        private async Task<bool> TemplateTypeMustExist(int id, CancellationToken cancellationToken)
        {
            return await _context.TemplateTypes.AnyAsync(t => t.TemplateTypeID == id, cancellationToken);
        }

        private async Task<bool> UserMustExist(int id, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(u => u.UserID == id, cancellationToken);
        }
    }
}