using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceStatements.Commands
{
    public class SendMonthlyDebtRemindersCommand : IRequest<Result<int>>
    {
        // Không cần tham số, mặc định lấy tháng hiện tại
        // Hoặc có thể truyền Month/Year nếu muốn nhắc nợ tháng trước
    }
}
