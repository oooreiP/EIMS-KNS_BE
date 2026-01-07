using EIMS.Application.DTOs;
using EIMS.Application.DTOs.InvoiceItems;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceItems.Queries
{
    public class GetInvoiceItemsByCustomerIdQuery : IRequest<Result<List<GetInvoiceItemsDTO>>>
    {
        public int CustomerId { get; set; }
    }
}
