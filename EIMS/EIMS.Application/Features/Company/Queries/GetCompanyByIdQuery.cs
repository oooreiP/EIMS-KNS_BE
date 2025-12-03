using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs.Company;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Company.Queries
{
    public class GetCompanyByIdQuery : IRequest<Result<CompanyResponse>>
    {
        public int Id { get; set; }

        public GetCompanyByIdQuery(int id)
        {
            Id = id;
        }
    }
}