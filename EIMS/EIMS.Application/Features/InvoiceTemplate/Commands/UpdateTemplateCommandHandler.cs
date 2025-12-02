using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoiceTemplate.Commands
{
    public class UpdateTemplateCommandHandler : IRequestHandler<UpdateTemplateCommand, Result>
    {
        private readonly IUnitOfWork _uow;

        public UpdateTemplateCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Result> Handle(UpdateTemplateCommand request, CancellationToken cancellationToken)
        {
            var template = await _uow.InvoiceTemplateRepository.GetByIdAsync(request.TemplateID);

            if (template == null)
            {
                return Result.Fail(new Error($"Template with ID {request.TemplateID} not found.")
                    .WithMetadata("ErrorCode", "Template.NotFound"));
            }

            // Update fields
            template.TemplateName = request.TemplateName;
            template.LayoutDefinition = JsonSerializer.Serialize(request.LayoutDefinition);
            template.TemplateFrameID = request.TemplateFrameID;
            template.LogoUrl = request.LogoUrl;
            template.IsActive = request.IsActive;
            await _uow.InvoiceTemplateRepository.UpdateAsync(template);
            await _uow.SaveChanges();
            return Result.Ok();
        }
    }
}