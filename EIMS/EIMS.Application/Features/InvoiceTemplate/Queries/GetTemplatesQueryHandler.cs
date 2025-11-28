using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.InvoiceTemplate;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoiceTemplate.Queries
{
    public class GetTemplatesQueryHandler : IRequestHandler<GetTemplatesQuery, Result<List<TemplateDetailResponse>>>
    {
    private readonly IUnitOfWork _unitOfWork;
        public GetTemplatesQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
    
        public async Task<Result<List<TemplateDetailResponse>>> Handle(GetTemplatesQuery request, CancellationToken cancellationToken)
        {
        
            var templates = await _unitOfWork.InvoiceTemplateRepository.GetTemplatesWithDetailsAsync();

            var response = templates.Select(t => new TemplateDetailResponse
            {
                TemplateID = t.TemplateID,
                TemplateName = t.TemplateName,
                IsActive = t.IsActive,
                SerialID = t.SerialID,
                Serial = $"{t.Serial.Prefix.PrefixID}{t.Serial.SerialStatus.Symbol}{t.Serial.Year}{t.Serial.InvoiceType.Symbol}{t.Serial.Tail}",
                TemplateTypeID = t.TemplateTypeID,
                TemplateTypeName = t.TemplateType.TypeName,
                LayoutDefinition = t.LayoutDefinition ?? string.Empty,
                LogoUrl = t.LogoUrl,
                TemplateFrameID = t.TemplateFrameID,
                FrameUrl = t.TemplateFrame?.ImageUrl
            }).ToList();
            return Result.Ok(response);        
            }
    }
}