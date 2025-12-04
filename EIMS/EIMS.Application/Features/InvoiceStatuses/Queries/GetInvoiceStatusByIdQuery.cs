using EIMS.Application.DTOs.InvoiceStatuses;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceStatuses.Queries
{
    public class GetInvoiceStatusByIdQuery : IRequest<Result<InvoiceStatusDto>>
    {
        public int Id { get; set; }
    }
}
