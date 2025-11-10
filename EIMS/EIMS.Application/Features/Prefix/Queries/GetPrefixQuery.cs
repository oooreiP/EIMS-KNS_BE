using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs.Prefix;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Prefix.Queries
{
    public class GetPrefixQuery : IRequest<Result<List<PrefixResponse>>>
    {
        
    }
}