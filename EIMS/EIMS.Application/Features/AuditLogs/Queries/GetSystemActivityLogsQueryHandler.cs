using AutoMapper;
using AutoMapper.QueryableExtensions;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.LogsDTO;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.AuditLogs.Queries
{
    public class GetSystemActivityLogsQueryHandler : IRequestHandler<GetSystemActivityLogsQuery, Result<PaginatedList<SystemActivityLogDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetSystemActivityLogsQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedList<SystemActivityLogDto>>> Handle(GetSystemActivityLogsQuery request, CancellationToken cancellationToken)
        {
            var query = _uow.SystemActivityLogRepository.GetAllQueryable();
            if (!string.IsNullOrEmpty(request.UserId))
                query = query.Where(x => x.UserId == request.UserId);

            if (!string.IsNullOrEmpty(request.IpAddress))
                query = query.Where(x => x.IpAddress.Contains(request.IpAddress)); // Contains để tìm tương đối

            if (!string.IsNullOrEmpty(request.ActionName))
                query = query.Where(x => x.ActionName == request.ActionName);

            if (!string.IsNullOrEmpty(request.Status))
                query = query.Where(x => x.Status == request.Status);

            if (request.FromDate.HasValue)
                query = query.Where(x => x.Timestamp >= request.FromDate.Value);

            if (request.ToDate.HasValue)
                query = query.Where(x => x.Timestamp < request.ToDate.Value.AddDays(1));
            query = query.OrderByDescending(x => x.Timestamp);
            var projectedQuery = query.ProjectTo<SystemActivityLogDto>(_mapper.ConfigurationProvider);

            var list = await PaginatedList<SystemActivityLogDto>.CreateAsync(
                projectedQuery,
                request.PageNumber,
                request.PageSize
            );
            return Result.Ok(list);
        }
    }
}
