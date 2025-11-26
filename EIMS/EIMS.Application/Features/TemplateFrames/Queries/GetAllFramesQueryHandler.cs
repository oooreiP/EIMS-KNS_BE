using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.TemplateFrames;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.TemplateFrames.Queries
{
    public class GetAllFramesQueryHandler : IRequestHandler<GetAllFramesQuery, Result<List<TemplateFramesResponse>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetAllFramesQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<Result<List<TemplateFramesResponse>>> Handle(GetAllFramesQuery request, CancellationToken cancellationToken)
        {
            var frames = await _uow.TemplateFrameRepository.GetAllAsync();
            var response = frames.Select(f => new TemplateFramesResponse
            {
                FrameID = f.FrameID,
                FrameName = f.FrameName,
                ImageUrl = f.ImageUrl
            }).ToList();
            return Result.Ok(response);
        }

    }
}