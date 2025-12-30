using AutoMapper;
using AutoMapper.QueryableExtensions;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.LogsDTO;
using FluentResults;
using MediatR;
using EIMS.Application.Commons.Mapping;

namespace EIMS.Application.Features.AuditLogs.Queries
{
    public class GetAuditLogsQueryHandler : IRequestHandler<GetAuditLogsQuery, Result<PaginatedList<AuditLogDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetAuditLogsQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedList<AuditLogDto>>> Handle(GetAuditLogsQuery request, CancellationToken cancellationToken)
        {
            var query = _uow.AuditLogRepository.GetAllQueryable();
            if (!string.IsNullOrEmpty(request.UserId))
            {
                query = query.Where(x => x.UserId == request.UserId);
            }

            if (!string.IsNullOrEmpty(request.TableName))
            {
                query = query.Where(x => x.TableName.ToLower() == request.TableName.ToLower());
            }

            if (!string.IsNullOrEmpty(request.Action))
            {
                query = query.Where(x => x.Action == request.Action);
            }

            if (request.FromDate.HasValue)
            {
                query = query.Where(x => x.Timestamp >= request.FromDate.Value);
            }

            if (request.ToDate.HasValue)
            {
                query = query.Where(x => x.Timestamp < request.ToDate.Value.AddDays(1));
            }
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                var term = request.SearchTerm.ToLower();
                query = query.Where(x =>
                    x.RecordId.Contains(term) ||
                    x.OldValues.Contains(term) ||
                    x.NewValues.Contains(term));
            }
            var projectedQuery = query.ProjectTo<AuditLogDto>(_mapper.ConfigurationProvider);

            var list = await PaginatedList<AuditLogDto>.CreateAsync(
                projectedQuery,
                request.PageNumber,
                request.PageSize
            );
            return Result.Ok(list);
        }
    }
}
