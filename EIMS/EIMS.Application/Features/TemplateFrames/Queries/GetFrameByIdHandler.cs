using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.TemplateFrames;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.TemplateFrames.Queries
{
    public class GetFrameByIdHandler : IRequestHandler<GetFrameById, Result<TemplateFramesResponse>>
    {
        private readonly IUnitOfWork _uow;
        public GetFrameByIdHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result<TemplateFramesResponse>> Handle(GetFrameById request, CancellationToken cancellationToken)
        {
            var frame = await _uow.TemplateFrameRepository.GetByIdAsync(request.Id);
            if (frame == null)
            {
                return Result.Fail(new Error($"Frame with ID {request.Id} not found.").WithMetadata("FrameID", "TemplateFrames.GetFrameById"));
            }
            var response = new TemplateFramesResponse
            {
                FrameID = frame.FrameID,
                FrameName = frame.FrameName,
                ImageUrl = frame.ImageUrl
            };
            return Result.Ok(response);
        }
    }
}