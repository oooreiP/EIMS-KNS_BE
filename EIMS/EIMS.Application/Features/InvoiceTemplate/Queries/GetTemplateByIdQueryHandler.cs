using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.InvoiceTemplate;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoiceTemplate.Queries
{
    public class GetTemplateByIdQueryHandler : IRequestHandler<GetTemplateByIdQuery, Result<TemplateDetailResponse>>
    {

        private readonly IUnitOfWork _unitOfWork;
        public GetTemplateByIdQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        public async Task<Result<TemplateDetailResponse>> Handle(GetTemplateByIdQuery request, CancellationToken cancellationToken)
        {
            var template = await _unitOfWork.InvoiceTemplateRepository.GetTemplateDetailsAsync(request.Id);

            if (template == null)
            {
                return Result.Fail(new Error($"Template with ID {request.Id} not found.").WithMetadata("TemplateID", "InvoiceTemplate.GetTemplateById"));
            }
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(1);
            var response = new TemplateDetailResponse
            {
                TemplateID = template.TemplateID,
                TemplateName = template.TemplateName,
                IsActive = template.IsActive,
                SerialID = template.SerialID,
                Serial = $"{template.Serial.Prefix.PrefixID}{template.Serial.SerialStatus.Symbol}{template.Serial.Year}{template.Serial.InvoiceType.Symbol}{template.Serial.Tail}",
                TemplateTypeID = template.TemplateTypeID,
                TemplateTypeName = template.TemplateType.TypeName,
                LayoutDefinition = template.LayoutDefinition ?? string.Empty,
                LogoUrl = template.LogoUrl,
                TemplateFrameID = template.TemplateFrameID,
                FrameUrl = template.TemplateFrame?.ImageUrl,
                Seller = new SellerInfo
                {
                    CompanyName = company?.CompanyName ?? "N/A",
                    TaxCode = company?.TaxCode ?? "N/A",
                    Address = company?.Address ?? "N/A",
                    Phone = company?.ContactPhone ?? "",
                    BankAccount = company?.AccountNumber ?? "",
                    BankName = company?.BankName ?? ""
                }
            };
            return Result.Ok(response);
        }
    }
}