using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs.Customer;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Customers.Queries
{
    public record SearchCustomerQuery(string SearchTerm) : IRequest<Result<List<CustomerDto>>>;


}