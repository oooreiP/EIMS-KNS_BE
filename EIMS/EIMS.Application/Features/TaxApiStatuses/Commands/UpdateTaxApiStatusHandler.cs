using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.TaxApiStatuses.Commands
{
    public class UpdateTaxApiStatusHandler : IRequestHandler<UpdateTaxApiStatusCommand, Result>
    {
        private readonly IUnitOfWork _uow;

        public UpdateTaxApiStatusHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<Result> Handle(UpdateTaxApiStatusCommand request, CancellationToken token)
        {
            var entity = await _uow.TaxApiStatusRepository.GetByIdAsync(request.TaxApiStatusID);
            if (entity == null) return Result.Fail("Không tìm thấy trạng thái cần sửa.");

            entity.Code = request.Code;
            entity.StatusName = request.StatusName;

            await _uow.TaxApiStatusRepository.UpdateAsync(entity);
            await _uow.SaveChanges();

            return Result.Ok();
        }
    }
}
