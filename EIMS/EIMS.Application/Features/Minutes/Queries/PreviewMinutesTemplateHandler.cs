using EIMS.Application.DTOs.Mails;
using EIMS.Application.Features.Emails.Queries;
using EIMS.Domain.Enums;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Minutes.Queries
{
    public class PreviewMinutesTemplateHandler : IRequestHandler<PreviewMinutesTemplateQuery, Result<FileAttachment>>
    {
        public async Task<Result<FileAttachment>> Handle(
        PreviewMinutesTemplateQuery request,
        CancellationToken cancellationToken)
        {
            string fileName = request.Type == MinutesType.Replacement
                ? "BienBan_ThayThe.docx"
                : "BienBan_DieuChinh.docx";

            string filePath = Path.Combine(
                request.RootPath,
                "Templates",
                fileName
            );

            if (!File.Exists(filePath))
                return Result.Fail("File mẫu không tồn tại");

            byte[] fileBytes = await File.ReadAllBytesAsync(filePath, cancellationToken);

            return Result.Ok(new FileAttachment
            {
                FileName = fileName,
                FileContent = fileBytes
            });
        }
    }
}
