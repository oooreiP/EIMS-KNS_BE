using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs;
using EIMS.Application.Features.Emails.Commands;
using EIMS.Application.Features.Emails.Queries;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Emails
{
    public class EmailTemplateHandlers :
        IRequestHandler<GetEmailTemplatesQuery, Result<IEnumerable<EmailTemplateDto>>>,
        IRequestHandler<GetEmailTemplateByIdQuery, Result<EmailTemplateDto>>,
        IRequestHandler<CreateEmailTemplateCommand, Result<int>>,
        IRequestHandler<UpdateEmailTemplateCommand, Result>,
        IRequestHandler<DeleteEmailTemplateCommand, Result>
    {
        private readonly IUnitOfWork _uow;

        public EmailTemplateHandlers(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<Result<IEnumerable<EmailTemplateDto>>> Handle(GetEmailTemplatesQuery request, CancellationToken cancellationToken)
        {
            var query = _uow.EmailTemplateRepository.GetAllQueryable();

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string term = request.SearchTerm.ToLower();
                query = query.Where(x => x.Subject.ToLower().Contains(term)
                                      || x.TemplateCode.ToLower().Contains(term)
                                      || x.Description.ToLower().Contains(term));
            }
            var list = await query.OrderBy(x => x.TemplateCode).ThenBy(x => x.LanguageCode).ToListAsync();

            var dtos = list.Select(x => new EmailTemplateDto
            {
                EmailTemplateID = x.EmailTemplateID,
                TemplateCode = x.TemplateCode,
                LanguageCode = x.LanguageCode,
                Subject = x.Subject,
                BodyContent = x.BodyContent,
                Description = x.Description,
                IsActive = x.IsActive
            });

            return Result.Ok(dtos);
        }

        public async Task<Result<EmailTemplateDto>> Handle(GetEmailTemplateByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _uow.EmailTemplateRepository.GetByIdAsync(request.Id);
            if (entity == null) return Result.Fail("Không tìm thấy mẫu email.");

            return Result.Ok(new EmailTemplateDto
            {
                EmailTemplateID = entity.EmailTemplateID,
                TemplateCode = entity.TemplateCode,
                LanguageCode = entity.LanguageCode,
                Subject = entity.Subject,
                BodyContent = entity.BodyContent,
                Description = entity.Description,
                IsActive = entity.IsActive
            });
        }
        public async Task<Result<int>> Handle(CreateEmailTemplateCommand request, CancellationToken cancellationToken)
        {
            var exists = await _uow.EmailTemplateRepository.GetAllQueryable()
                .AnyAsync(x => x.TemplateCode == request.TemplateCode && x.LanguageCode == request.LanguageCode);

            if (exists)
                return Result.Fail($"Mẫu email '{request.TemplateCode}' ngôn ngữ '{request.LanguageCode}' đã tồn tại.");

            var entity = new EmailTemplate
            {
                TemplateCode = request.TemplateCode.ToUpper(), 
                LanguageCode = request.LanguageCode.ToLower(),
                Subject = request.Subject,
                BodyContent = request.BodyContent,
                Description = request.Description,
                IsActive = request.IsActive
            };

            await _uow.EmailTemplateRepository.CreateAsync(entity);
            await _uow.SaveChanges();

            return Result.Ok(entity.EmailTemplateID);
        }
        public async Task<Result> Handle(UpdateEmailTemplateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _uow.EmailTemplateRepository.GetByIdAsync(request.EmailTemplateID);
            if (entity == null) return Result.Fail("Không tìm thấy mẫu email.");

            entity.Subject = request.Subject;
            entity.BodyContent = request.BodyContent;
            entity.Description = request.Description;
            entity.IsActive = request.IsActive;
            await _uow.EmailTemplateRepository.UpdateAsync(entity);
            await _uow.SaveChanges();

            return Result.Ok();
        }
        public async Task<Result> Handle(DeleteEmailTemplateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _uow.EmailTemplateRepository.GetByIdAsync(request.Id);
            if (entity == null) return Result.Fail("Không tìm thấy mẫu email.");
            await _uow.EmailTemplateRepository.DeleteAsync(entity);
            await _uow.SaveChanges();

            return Result.Ok();
        }
    }
}
