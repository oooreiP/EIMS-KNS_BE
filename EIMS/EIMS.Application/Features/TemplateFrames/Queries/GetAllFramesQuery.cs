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
    public class GetAllFramesQuery : IRequest<Result<List<TemplateFramesResponse>>>
  {
  }
}