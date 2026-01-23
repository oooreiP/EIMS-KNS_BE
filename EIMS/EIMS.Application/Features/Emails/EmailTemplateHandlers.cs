using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs;
using EIMS.Application.Features.Emails.Commands;
using EIMS.Application.Features.Emails.Queries;
using EIMS.Domain.Entities;
using FluentResults;
using Humanizer;
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
        IRequestHandler<UpdateEmailTemplateCommand, Result>,
        IRequestHandler<ResetEmailTemplateCommand, Result<bool>>,
        IRequestHandler<GetEmailTemplateVariablesQuery, Result<List<string>>>,
        IRequestHandler<GetBaseContentByCodeQuery, Result<string>>
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
        public async Task<Result<bool>> Handle(ResetEmailTemplateCommand request, CancellationToken cancellationToken)
        {
            var template = await _uow.EmailTemplateRepository.GetByIdAsync(request.EmailTemplateID);

            // 2. Kiểm tra tồn tại
            if (template == null)
            {
                return Result.Fail<bool>($"Không tìm thấy mẫu email có ID: {request.EmailTemplateID}");
            }
            if (string.IsNullOrEmpty(template.OriginalBodyContent))
            {
                return Result.Fail<bool>("Mẫu này không có nội dung gốc (Original Content) để khôi phục.");
            }
            template.BodyContent = template.OriginalBodyContent; 
            template.UpdatedAt = DateTime.UtcNow;
            await _uow.SaveChanges();
                return Result.Ok(true);
        }
        public async Task<Result> Handle(UpdateEmailTemplateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _uow.EmailTemplateRepository.GetByIdAsync(request.EmailTemplateID);
            if (entity == null) return Result.Fail("Không tìm thấy mẫu email.");
            entity.Subject = request.Subject;
            entity.BodyContent = request.BodyContent;
            entity.UpdatedAt = DateTime.UtcNow; 

            await _uow.EmailTemplateRepository.UpdateAsync(entity);
            await _uow.SaveChanges();

            return Result.Ok();
        }
        public async Task<Result<string>> Handle(GetBaseContentByCodeQuery request, CancellationToken cancellationToken)
        {
            // Logic: Lấy bản ghi cũ nhất (được coi là bản gốc của hệ thống)
            var content = await _uow.EmailTemplateRepository.GetAllQueryable()
                .AsNoTracking()
                .Where(x => x.TemplateCode == request.TemplateCode)
                .OrderByDescending(x => x.IsSystemTemplate)
                .ThenBy(x => x.CreatedAt)
                .Select(x => x.BodyContent) 
                .FirstOrDefaultAsync(cancellationToken);

            if (string.IsNullOrEmpty(content))
            {
                return Result.Fail<string>($"Không tìm thấy mẫu email gốc cho mã: {request.TemplateCode}");
            }

            return Result.Ok(content);
        }
        public Task<Result<List<string>>> Handle(GetEmailTemplateVariablesQuery request, CancellationToken cancellationToken)
        {
            var commonVariables = new List<string>
                {
                    "{{CustomerName}}",
                    "{{AttachmentList}}", // Link tải file hoặc danh sách file đính kèm
                    "{{CompanyName}}",
                    "{{SupportEmail}}"
                };
            var variables = new List<string>();
            switch (request.Category?.ToLower())
            {
                case "invoice":
                    variables.AddRange(new[]
                    {
                "{{InvoiceNumber}}",
                "{{TotalAmount}}",
                "{{IssuedDate}}",    // Ngày lập hóa đơn (Ngày ký)
                "{{CreatedAt}}",     // Ngày tạo trên hệ thống
                "{{DueDate}}",       // Hạn thanh toán
                "{{Message}}",       // Lời nhắn từ người gửi (User nhập lúc gửi mail)
                "{{PaymentLink}}"    // Link thanh toán online (nếu có)
            });
                    break;

                case "payment":
                    variables.AddRange(new[]
                    {
                "{{InvoiceNumber}}", // Nhắc nợ thì cần số hóa đơn
                "{{TotalAmount}}",   // Số tiền nợ/đã trả
                "{{AmountPaid}}",    // Số tiền vừa thanh toán
                "{{PaymentDate}}",   // Ngày thanh toán
                "{{ReceiptNumber}}", // Số phiếu thu
                "{{Message}}"        // Lời nhắn nhắc nợ
            });
                    break;

                case "minutes": // [MỚI] Dành cho biên bản điều chỉnh/thu hồi
                    variables.AddRange(new[]
                    {
                "{{InvoiceNumber}}", // Số hóa đơn gốc bị điều chỉnh/thay thế
                "{{IssuedDate}}",    // Ngày lập hóa đơn gốc
                "{{Reason}}",        // Lý do điều chỉnh/thu hồi
                "{{MinutesDate}}"    // Ngày lập biên bản
            });
                    break;

                case "system":
                    variables.AddRange(new[]
                    {
                "{{UserName}}",
                "{{ResetPasswordLink}}",
                "{{OtpCode}}"
            });
                    break;
                default:
                    break;
            }

            var resultVariables = commonVariables
                .Concat(variables)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            return Task.FromResult(Result.Ok(resultVariables));
        }
    }
}
