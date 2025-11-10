using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Prefix;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Prefix.Queries
{
    public class GetPrefixQueryHandler : IRequestHandler<GetPrefixQuery, Result<List<PrefixResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPrefixQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<PrefixResponse>>> Handle(GetPrefixQuery request, CancellationToken cancellationToken)
        {
            var prefixes = await _unitOfWork.PrefixRepository.GetAllAsync();
            var response = prefixes.Select(p => new PrefixResponse
            {
                PrefixID = p.PrefixID,
                PrefixName = p.PrefixName
            }).ToList();
            return Result.Ok(response);
        }
    }
}