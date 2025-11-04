using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Serials;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Serial.Commands
{
    public class CreateSerialCommandHandler : IRequestHandler<CreateSerialCommand, Result<SerialResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateSerialCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<SerialResponse>> Handle(CreateSerialCommand request, CancellationToken cancellationToken)
        {
            var newSerial = new Domain.Entities.Serial
            {
                PrefixID = request.PrefixID,
                SerialStatusID = request.SerialStatusID,
                Year = request.Year,
                InvoiceTypeID = request.InvoiceTypeID,
                Tail = request.Tail
            };
            await _unitOfWork.SerialRepository.CreateAsync(newSerial);
            await _unitOfWork.SaveChanges();
            var createdSerial = await _unitOfWork.SerialRepository.GetSerialWithDetailsAsync(newSerial.SerialID);
            if (createdSerial == null)
                return Result.Fail(new Error("Failed to create or retrive data").WithMetadata("ErrorCode", "Serial.Create.Failed"));
            var response = new SerialResponse
            {
                SerialID = createdSerial.SerialID,
                Serial = $"{createdSerial.Prefix.PrefixID}{createdSerial.SerialStatus.Symbol}{createdSerial.Year}{createdSerial.InvoiceType.Symbol}{createdSerial.Tail}",
                Description = $"{createdSerial.Prefix.PrefixName} - {createdSerial.SerialStatus.StatusName}"
            };
            return Result.Ok(response);
        }
    }
}
