using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.InvoiceTemplate.Commands
{
    public class CreateTemplateCommand : IRequest<Result<int>>, IAuthenticatedCommand
    {
        public string TemplateName { get; set; } = string.Empty;

        public int SerialID { get; set; } 

        public int TemplateTypeID { get; set; } 

        public string LayoutDefinition { get; set; } = string.Empty;
        
        public int AuthenticatedUserId { get; set; }    
        }
}