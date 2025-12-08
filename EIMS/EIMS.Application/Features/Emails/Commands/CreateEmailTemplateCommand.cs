using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Emails.Commands
{
    public class CreateEmailTemplateCommand : IRequest<Result<int>>
    {
        [Required]
        [StringLength(50)]
        public string TemplateCode { get; set; } 

        [Required]
        public string LanguageCode { get; set; } = "vi";

        [Required]
        public string Subject { get; set; }

        public string BodyContent { get; set; } = ""; 
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
