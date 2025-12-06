using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceStatuses.Commands
{
    public class CreateInvoiceStatusCommand : IRequest<Result<int>>
    {
        [Required]
        [StringLength(50)]
        public string StatusName { get; set; }
    }
}
