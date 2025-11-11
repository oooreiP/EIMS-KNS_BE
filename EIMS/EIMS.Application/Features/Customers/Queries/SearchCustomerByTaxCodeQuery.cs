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
    public record SearchCustomerByTaxCodeQuery(string TaxCode)
    : IRequest<Result<CustomerDto>>;
}
