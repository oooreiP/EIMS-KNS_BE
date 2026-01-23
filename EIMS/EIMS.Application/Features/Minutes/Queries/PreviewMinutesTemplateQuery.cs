using EIMS.Application.DTOs.Mails;
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
    public class PreviewMinutesTemplateQuery : IRequest<Result<FileAttachment>>
    {
        public MinutesType Type { get; set; }
        public string RootPath { get; set; }
    }
}
