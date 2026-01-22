using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceStatements.Commands
{
    public class CreateStatementCommandValidator : AbstractValidator<CreateStatementCommand>
    {
        public CreateStatementCommandValidator()
        {
            RuleFor(x => x.CustomerID).GreaterThan(0).WithMessage("Khách hàng không hợp lệ.");

            RuleFor(x => x.Month).InclusiveBetween(1, 12).WithMessage("Tháng phải từ 1 đến 12.");

            RuleFor(x => x.Year).GreaterThanOrEqualTo(2000).WithMessage("Năm không hợp lệ.");
            RuleFor(x => x).Must(x =>
            {
                var date = new DateTime(x.Year, x.Month, 1);
                return date <= DateTime.UtcNow.AddMonths(1);
            }).WithMessage("Không thể tạo bảng kê cho tương lai quá xa.");
        }
    }
}
