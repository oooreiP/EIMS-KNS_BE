using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.CQT.NotifyInvoiceError
{
    public class NotifyInvoiceErrorCommand : IRequest<Result>
    {
        public int InvoiceId { get; set; } // ID hóa đơn bị sai
        public int ErrorType { get; set; } // 1: Hủy, 2: Điều chỉnh, 3: Thay thế, 4: Giải trình
        public string Reason { get; set; } // Lý do sai sót
    }
}
