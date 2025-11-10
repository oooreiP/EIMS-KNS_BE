using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs.InvoiceType;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoiceType.Queries
{
    public class GetInvoiceTypeQuery : IRequest<Result<List<InvoiceTypeResponse>>>
    {
        
    }
}