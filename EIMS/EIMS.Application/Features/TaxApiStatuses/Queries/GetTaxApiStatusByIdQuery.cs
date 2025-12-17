using EIMS.Application.DTOs.TaxAPIDTO;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.TaxApiStatuses.Queries
{
    public class GetTaxApiStatusByIdQuery : IRequest<Result<TaxApiStatusDto>>
    {
        public int Id { get; set; }
        public GetTaxApiStatusByIdQuery(int id) => Id = id;
    }
}
