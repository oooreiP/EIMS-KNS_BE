using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs.SerialStatus;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.SerialStatus.Queries
{
    public class GetSerialStatusQuery : IRequest<Result<List<SerialStatusResponse>>>
    {
        
    }
}