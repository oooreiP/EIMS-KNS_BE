using EIMS.Application.DTOs.Invoices;
using EIMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Queries
{
    public class GetInvoiceByIdQuery : IRequest<InvoiceDTO?>
    {
        public int Id { get; set; }
        public GetInvoiceByIdQuery(int id) => Id = id;
    }
}
