using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs;
using EIMS.Application.DTOs.Payments;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoicePayment.Queries
{
    public class GetMonthlyDebtQuery : IRequest<Result<MonthlyDebtResult>>
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int? CustomerId { get; set; } 
        public string SearchTerm { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
