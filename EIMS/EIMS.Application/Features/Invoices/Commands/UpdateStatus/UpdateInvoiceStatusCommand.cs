using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Commands.UpdateStatus
{
    public class UpdateInvoiceStatusCommand : IRequest<Result>
    {
        [Required]
        public int InvoiceId { get; set; }
        [Required]
        public int NewStatusId { get; set; }
        public string Note { get; set; } = "Cập nhật trạng thái thủ công";
    }
}
