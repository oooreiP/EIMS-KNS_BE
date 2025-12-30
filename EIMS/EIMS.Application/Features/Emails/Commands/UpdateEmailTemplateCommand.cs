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
    public class UpdateEmailTemplateCommand : IRequest<Result>
    {
        public int EmailTemplateID { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string BodyContent { get; set; } // HTML update
        public string Category { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
    }
}
