using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs.Serials;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Serial.Queries
{
    public class GetSerialQuery : IRequest<Result<List<SerialResponse>>>
    {
        
    }
}