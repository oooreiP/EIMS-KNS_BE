using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs.TemplateFrames;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.TemplateFrames.Queries
{
    public class GetFrameById : IRequest<Result<TemplateFramesResponse>>
    {
        public int Id { get; }
        public GetFrameById(int id) => Id = id;
    }
}