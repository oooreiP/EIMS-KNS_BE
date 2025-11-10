using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.SerialStatus;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.SerialStatus.Queries
{
    public class GetSerialStatusQueryHandler : IRequestHandler<GetSerialStatusQuery, Result<List<SerialStatusResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetSerialStatusQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<SerialStatusResponse>>> Handle(GetSerialStatusQuery request, CancellationToken cancellationToken)
        {
            var serialStatuses = await _unitOfWork.SerialStatusRepository.GetAllAsync();
            var response = serialStatuses.Select(p => new SerialStatusResponse
            {
                SerialStatusID = p.SerialStatusID,
                Symbol = p.Symbol,
                StatusName = p.StatusName
            }).ToList();
            return Result.Ok(response);
        }
    }

}