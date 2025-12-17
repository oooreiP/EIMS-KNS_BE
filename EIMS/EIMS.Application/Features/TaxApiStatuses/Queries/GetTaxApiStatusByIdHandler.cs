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
    public class GetTaxApiStatusByIdHandler : IRequestHandler<GetTaxApiStatusByIdQuery, Result<TaxApiStatusDto>>
    {
        private readonly IUnitOfWork _uow;

        public GetTaxApiStatusByIdHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<Result<TaxApiStatusDto>> Handle(GetTaxApiStatusByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _uow.TaxApiStatusRepository.GetByIdAsync(request.Id);
            if (entity == null) return Result.Fail("Không tìm thấy trạng thái.");

            return Result.Ok(new TaxApiStatusDto
            {
                TaxApiStatusID = entity.TaxApiStatusID,
                Code = entity.Code,
                StatusName = entity.StatusName
            });
        }
    }
}
