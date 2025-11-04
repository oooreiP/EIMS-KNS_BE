using EIMS.Application.Commons.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Application.Features.Serial.Commands
{
    public class CreateSerialCommandValidator : AbstractValidator<CreateSerialCommand>
    {
        private readonly IApplicationDBContext _context;

        public CreateSerialCommandValidator(IApplicationDBContext context)
        {
            _context = context;

            // Rule for PrefixID
            RuleFor(v => v.PrefixID)
                .MustAsync(PrefixMustExist)
                .WithMessage("The specified PrefixID does not exist.");

            // Rule for InvoiceTypeID (This would have caught your error)
            RuleFor(v => v.InvoiceTypeID)
                .MustAsync(InvoiceTypeMustExist)
                .WithMessage("The specified InvoiceTypeID does not exist.");

            // Rule for SerialStatusID
            RuleFor(v => v.SerialStatusID)
                .MustAsync(SerialStatusMustExist)
                .WithMessage("The specified SerialStatusID does not exist.");

            // Rule for Year
            RuleFor(v => v.Year)
                .NotEmpty().WithMessage("Year is required.")
                .Length(2).WithMessage("Year must be exactly 2 characters (e.g., '25').");

            // Rule for Tail
            RuleFor(v => v.Tail)
                .NotEmpty().WithMessage("Tail is required.")
                .MaximumLength(2).WithMessage("Tail must not exceed 2 characters.");
        }

        // --- Database Existence Checks ---

        private async Task<bool> PrefixMustExist(int id, CancellationToken cancellationToken)
        {
            // Checks if an entity with this ID exists in the Prefixes table
            return await _context.Prefixes.AnyAsync(p => p.PrefixID == id, cancellationToken);
        }

        private async Task<bool> InvoiceTypeMustExist(int id, CancellationToken cancellationToken)
        {
            // This check targets the exact foreign key that failed in your log
            return await _context.InvoiceTypes.AnyAsync(it => it.InvoiceTypeID == id, cancellationToken);
        }

        private async Task<bool> SerialStatusMustExist(int id, CancellationToken cancellationToken)
        {
            // Checks if an entity with this ID exists in the SerialStatuses table
            return await _context.SerialStatuses.AnyAsync(ss => ss.SerialStatusID == id, cancellationToken);
        }
    }
}
