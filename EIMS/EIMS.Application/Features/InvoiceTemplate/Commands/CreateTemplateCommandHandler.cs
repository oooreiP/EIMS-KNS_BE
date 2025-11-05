using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoiceTemplate.Commands
{
    public class CreateTemplateCommandHandler : IRequestHandler<CreateTemplateCommand, Result<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CreateTemplateCommandHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Result<int>> Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateTemplateCommand>(request);
            var newTemplate = new Domain.Entities.InvoiceTemplate
            {
                TemplateName = command.TemplateName,
                SerialID = command.SerialID,
                TemplateTypeID = command.TemplateTypeID,
                CreatedByUserID = command.AuthenticatedUserId,
                LayoutDefinition = command.LayoutDefinition,
                IsActive = true, // Set default
            };
            await _uow.InvoiceTemplateRepository.CreateAsync(newTemplate);
            await _uow.SaveChanges();
            return Result.Ok(newTemplate.TemplateID);
        }
    }
}