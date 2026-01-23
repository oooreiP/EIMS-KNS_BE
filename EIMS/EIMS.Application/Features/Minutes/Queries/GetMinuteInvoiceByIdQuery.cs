using EIMS.Application.DTOs.Minutes;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Minutes.Queries
{
    public record GetMinuteInvoiceByIdQuery(int Id) : IRequest<Result<MinuteInvoiceDto>>;
}
