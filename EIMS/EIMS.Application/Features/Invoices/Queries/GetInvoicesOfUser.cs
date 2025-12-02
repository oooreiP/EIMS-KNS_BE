using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.Invoices;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Invoices.Queries
{
    public class GetInvoicesOfUser : IRequest<Result<PaginatedList<InvoiceDTO>>>, IAuthenticatedCommand
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchTerm { get; set; }
        public string? RoleFilter { get; set; }
        public int AuthenticatedUserId { get; set; }
    }
}

