using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Commands.IssueInvoice
{
    public class IssueInvoiceCommand : IRequest<Result>
    {
        public int InvoiceId { get; set; }
        public int IssuerId { get; set; }
        public bool AutoCreatePayment { get; set; } = false; // Có muốn tạo payment luôn không?
        public decimal? PaymentAmount { get; set; } // Số tiền trả ngay (nếu có)
        public string? PaymentMethod { get; set; } // "Cash", "Transfer"...
        public string? Note { get; set; }
    }
}
