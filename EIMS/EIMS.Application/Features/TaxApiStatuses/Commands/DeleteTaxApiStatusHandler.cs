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
    public class DeleteTaxApiStatusHandler : IRequestHandler<DeleteTaxApiStatusCommand, Result>
    {
        private readonly IUnitOfWork _uow;

        public DeleteTaxApiStatusHandler(IUnitOfWork uow) => _uow = uow;

        public async Task<Result> Handle(DeleteTaxApiStatusCommand request, CancellationToken token)
        {
            var entity = await _uow.TaxApiStatusRepository.GetByIdAsync(request.Id);
            if (entity == null) return Result.Fail("Không tìm thấy trạng thái.");
            var isUsed = await _uow.TaxApiLogRepository.FindAsync(l => l.TaxApiStatusID == request.Id);
            if (isUsed != null) return Result.Fail("Trạng thái này đang được sử dụng trong lịch sử Log, không thể xóa.");
            

            await _uow.TaxApiStatusRepository.DeleteAsync(entity);
            await _uow.SaveChanges();

            return Result.Ok();
        }
    }
}
