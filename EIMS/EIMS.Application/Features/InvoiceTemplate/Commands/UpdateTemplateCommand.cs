using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoiceTemplate.Commands
{
    public class UpdateTemplateCommand : IRequest<Result>, IAuthenticatedCommand
    {
        public int TemplateID { get; set; }
        public string TemplateName { get; set; } = string.Empty;
        public object LayoutDefinition { get; set; }
        public int TemplateFrameID { get; set; }
        public string? LogoUrl { get; set; }
        public bool IsActive { get; set; }
        public int AuthenticatedUserId { get; set; }
        public int? CustomerId { get; set; }
    }
}