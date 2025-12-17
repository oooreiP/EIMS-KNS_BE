using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.TaxApiStatuses.Commands
{
    public class CreateTaxApiStatusHandler : IRequestHandler<CreateTaxApiStatusCommand, Result<int>>
    {
        private readonly IUnitOfWork _uow;

        public CreateTaxApiStatusHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<Result<int>> Handle(CreateTaxApiStatusCommand request, CancellationToken token)
        {
            var exists = await _uow.TaxApiStatusRepository.FindAsync(x => x.Code == request.Code);
            if (exists != null) return Result.Fail($"Mã trạng thái '{request.Code}' đã tồn tại.");
            var entity = new TaxApiStatus
            {
                Code = request.Code,
                StatusName = request.StatusName
            };

            await _uow.TaxApiStatusRepository.CreateAsync(entity);
            await _uow.SaveChanges();

            return Result.Ok(entity.TaxApiStatusID);
        }
    }
}
