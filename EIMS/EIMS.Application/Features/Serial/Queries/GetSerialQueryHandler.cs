using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Serials;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Serial.Queries
{
    public class GetSerialQueryHandler : IRequestHandler<GetSerialQuery, Result<List<SerialResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetSerialQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<SerialResponse>>> Handle(GetSerialQuery request, CancellationToken cancellationToken)
        {
            //lay het Serial
            var serials = await _unitOfWork.SerialRepository.GetSerialsWithDetailsAsync();
            var response = serials.Select(s => new SerialResponse
            {
                SerialID = s.SerialID,
                Serial = $"{s.Prefix.PrefixID}{s.SerialStatus.Symbol}{s.Year}{s.InvoiceType.Symbol}{s.Tail}",
                Description = $"{s.Prefix.PrefixName} - {s.SerialStatus.StatusName}"
            }).ToList();
            return Result.Ok(response);
        }
    }
}