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
    public class GetTaxApiStatusListQuery : IRequest<Result<List<TaxApiStatusDto>>>
    {
    }
}
