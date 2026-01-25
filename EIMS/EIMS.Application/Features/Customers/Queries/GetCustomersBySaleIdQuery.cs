using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.Customer;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Customers.Queries
{
    public class GetCustomersBySaleIdQuery : IRequest<Result<PaginatedList<CustomerDto>>>
    {
        public int SaleID { get; set; }      
        public string? SearchTerm { get; set; } 
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public GetCustomersBySaleIdQuery(int saleID, string searchTerm, int pageIndex, int pageSize)
        {
            SaleID = saleID;
            SearchTerm = searchTerm;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
