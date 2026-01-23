using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Minutes;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Minutes.Queries
{
    public class GetMinuteInvoiceByIdQueryHandler : IRequestHandler<GetMinuteInvoiceByIdQuery, Result<MinuteInvoiceDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetMinuteInvoiceByIdQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<MinuteInvoiceDto>> Handle(GetMinuteInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            var minute = await _uow.MinuteInvoiceRepository.GetAllQueryable()
                .Include(m => m.Invoice) 
                .ThenInclude(c => c.Customer)
                .Include(m => m.Creator) 
                .FirstOrDefaultAsync(m => m.MinutesInvoiceId == request.Id, cancellationToken);

            if (minute == null)
            {
                return Result.Fail<MinuteInvoiceDto>(new Error("Không tìm thấy biên bản.").WithMetadata("ErrorCode", "NotFound"));
            }

            var dto = _mapper.Map<MinuteInvoiceDto>(minute);

            return Result.Ok(dto);
        }
    }
}
