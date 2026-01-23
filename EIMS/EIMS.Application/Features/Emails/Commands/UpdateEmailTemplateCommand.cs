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
        public string Subject { get; set; }
        public string BodyContent { get; set; }
    }
}
