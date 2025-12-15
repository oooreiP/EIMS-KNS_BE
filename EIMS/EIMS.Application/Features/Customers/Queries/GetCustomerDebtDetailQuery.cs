using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs.Customer;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Customers.Queries
{
    public class GetCustomerDebtDetailQuery : IRequest<Result<CustomerDebtDetailDto>>
    {
        public int CustomerId { get; set; }

        public GetCustomerDebtDetailQuery(int customerId)
        {
            CustomerId = customerId;
        }
    }
}