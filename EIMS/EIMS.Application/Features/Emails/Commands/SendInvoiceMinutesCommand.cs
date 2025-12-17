using EIMS.Domain.Enums;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Emails.Commands
{
    public class SendInvoiceMinutesCommand : IRequest<Result>
    {
        public int InvoiceId { get; set; }
        public MinutesType Type { get; set; } // Loại biên bản
        public string Reason { get; set; }    // Lý do sai sót (để điền vào biên bản và email)
        public string? ContentBefore { get; set; } // Nội dung trước điều chỉnh
        public string? ContentAfter { get; set; }  // Nội dung sau điều chỉnh
        public DateTime AgreementDate { get; set; } = DateTime.Now; // Ngày lập biên bản
    }
}
