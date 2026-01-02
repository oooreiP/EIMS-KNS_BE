using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.InvoiceTemplate;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoiceTemplate.Commands
{
    public class CreateTemplateCommand : IRequest<Result<int>>, IAuthenticatedCommand
    {
        public string TemplateName { get; set; } = string.Empty;
        public int SerialID { get; set; }
        public int TemplateTypeID { get; set; }

        public int TemplateFrameID { get; set; }
        public string? LogoUrl { get; set; }
        public object LayoutDefinition { get; set; }
        public int AuthenticatedUserId { get; set; }
        public int? CustomerId { get; set; }
    }
}