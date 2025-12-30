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
using System.Xml.Linq;

namespace EIMS.Application.Features.Emails
{
    public class EmailTemplateHandlers :
        IRequestHandler<GetEmailTemplatesQuery, Result<IEnumerable<EmailTemplateDto>>>,
        IRequestHandler<GetEmailTemplateByIdQuery, Result<EmailTemplateDto>>,
        IRequestHandler<CreateEmailTemplateCommand, Result<int>>,
        IRequestHandler<UpdateEmailTemplateCommand, Result>,
        IRequestHandler<DeleteEmailTemplateCommand, Result>,
        IRequestHandler<GetEmailTemplateVariablesQuery, Result<List<string>>>
    {
        private readonly IUnitOfWork _uow;

        public EmailTemplateHandlers(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<Result<IEnumerable<EmailTemplateDto>>> Handle(GetEmailTemplatesQuery request, CancellationToken cancellationToken)
        {
            var query = _uow.EmailTemplateRepository.GetAllQueryable();

            // Filter by Search Term (Subject, Code, Name)
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string term = request.SearchTerm.ToLower();
                query = query.Where(x => x.Subject.ToLower().Contains(term)
                                      || x.TemplateCode.ToLower().Contains(term)
                                      || x.Name.ToLower().Contains(term));
            }
            if (!string.IsNullOrEmpty(request.LanguageCode))
            {
                query = query.Where(x => x.LanguageCode == request.LanguageCode);
            }
            if (!string.IsNullOrEmpty(request.Category))
            {
                query = query.Where(x => x.Category == request.Category);
            }

            // [cite: 16] Filter by IsActive
            if (request.IsActive.HasValue)
            {
                query = query.Where(x => x.IsActive == request.IsActive.Value);
            }

            var list = await query
                .OrderByDescending(x => x.IsSystemTemplate) 
                .ThenBy(x => x.TemplateCode)
                .ToListAsync();
            var dtos = list.Select(x => new EmailTemplateDto
            {
                EmailTemplateID = x.EmailTemplateID,
                TemplateCode = x.TemplateCode,
                LanguageCode = x.LanguageCode,
                Category = x.Category,             
                Name = x.Name,                    
                Subject = x.Subject,
                BodyContent = x.BodyContent,
                IsActive = x.IsActive,
                IsSystemTemplate = x.IsSystemTemplate, 
                CreatedAt = x.CreatedAt,           
                UpdatedAt = x.UpdatedAt           
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
                Category = entity.Category,
                Name = entity.Name,
                Subject = entity.Subject,
                BodyContent = entity.BodyContent,
                IsActive = entity.IsActive,
                IsSystemTemplate = entity.IsSystemTemplate,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
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
                Category = request.Category,  
                Name = request.Name,              
                Subject = request.Subject,
                BodyContent = request.BodyContent,
                IsSystemTemplate = false,         
                IsActive = request.IsActive,
                CreatedAt = DateTime.UtcNow        
            };

            await _uow.EmailTemplateRepository.CreateAsync(entity);
            await _uow.SaveChanges();

            return Result.Ok(entity.EmailTemplateID);
        }
        public async Task<Result> Handle(UpdateEmailTemplateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _uow.EmailTemplateRepository.GetByIdAsync(request.EmailTemplateID);
            if (entity == null) return Result.Fail("Không tìm thấy mẫu email.");
            if (entity.IsSystemTemplate)
            {
                entity.Subject = request.Subject;
                entity.BodyContent = request.BodyContent;
                entity.Name = request.Name;
                entity.IsActive = request.IsActive;
            }
            else
            {
                entity.Category = request.Category;
                entity.Name = request.Name;
                entity.Subject = request.Subject;
                entity.BodyContent = request.BodyContent;
                entity.IsActive = request.IsActive;
            }
            entity.UpdatedAt = DateTime.UtcNow; 

            await _uow.EmailTemplateRepository.UpdateAsync(entity);
            await _uow.SaveChanges();

            return Result.Ok();
        }
        public async Task<Result> Handle(DeleteEmailTemplateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _uow.EmailTemplateRepository.GetByIdAsync(request.Id);
            if (entity == null) return Result.Fail("Không tìm thấy mẫu email.");
            if (entity.IsSystemTemplate)
            {
                return Result.Fail("Không thể xóa mẫu email mặc định của hệ thống.");
            }

            await _uow.EmailTemplateRepository.DeleteAsync(entity);
            await _uow.SaveChanges();

            return Result.Ok();
        }
        public Task<Result<List<string>>> Handle(GetEmailTemplateVariablesQuery request, CancellationToken cancellationToken)
        {
            var variables = new List<string>();
            switch (request.Category?.ToLower())
            {
                case "invoice":
                    variables.AddRange(new[] { "{{InvoiceNumber}}", "{{CustomerName}}", "{{TotalAmount}}", "{{DueDate}}", "{{InvoiceDate}}", "{{PaymentLink}}" });
                    break;
                case "payment":
                    variables.AddRange(new[] { "{{ReceiptNumber}}", "{{PaymentDate}}", "{{AmountPaid}}", "{{CustomerName}}" });
                    break;
                case "statement":
                    variables.AddRange(new[] { "{{StatementPeriod}}", "{{OpeningBalance}}", "{{ClosingBalance}}", "{{CustomerName}}" });
                    break;
                case "system":
                    variables.AddRange(new[] { "{{UserName}}", "{{ResetPasswordLink}}", "{{OtpCode}}", "{{CompanyName}}" });
                    break;
                default:
                    // Biến chung cho tất cả
                    variables.AddRange(new[] { "{{CompanyName}}", "{{SupportEmail}}" });
                    break;
            }

            return Task.FromResult(Result.Ok(variables));
        }
    }
}
