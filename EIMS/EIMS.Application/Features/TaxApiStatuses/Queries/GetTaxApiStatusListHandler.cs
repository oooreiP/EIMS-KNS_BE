using EIMS.Application.Commons.Interfaces;
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
    public class GetTaxApiStatusListHandler : IRequestHandler<GetTaxApiStatusListQuery, Result<List<TaxApiStatusDto>>>
    {
        private readonly IUnitOfWork _uow;

        public GetTaxApiStatusListHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<List<TaxApiStatusDto>>> Handle(GetTaxApiStatusListQuery request, CancellationToken cancellationToken)
        {
            var list = await _uow.TaxApiStatusRepository.GetAllAsync();
            var dtos = list.Select(x => new TaxApiStatusDto
            {
                TaxApiStatusID = x.TaxApiStatusID,
                Code = x.Code,
                StatusName = x.StatusName
            }).ToList();

            return Result.Ok(dtos);
        }
    }
}
