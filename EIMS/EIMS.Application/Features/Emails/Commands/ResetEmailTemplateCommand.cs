using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Emails.Commands
{
    public class ResetEmailTemplateCommand : IRequest<Result<bool>>
    {
        public int EmailTemplateID { get; set; }

        public ResetEmailTemplateCommand(int id)
        {
            EmailTemplateID = id;
        }
    }
}
