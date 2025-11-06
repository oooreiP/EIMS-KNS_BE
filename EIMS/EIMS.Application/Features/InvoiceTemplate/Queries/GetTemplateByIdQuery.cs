using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs.InvoiceTemplate;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoiceTemplate.Queries
{
    public class GetTemplateByIdQuery : IRequest<Result<TemplateDetailResponse>>
    {
        public int Id { get; }
        public GetTemplateByIdQuery(int id) => Id = id;
    }
}