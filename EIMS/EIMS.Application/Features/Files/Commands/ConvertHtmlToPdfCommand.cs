using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Files.Commands
{
    public class ConvertHtmlToPdfCommand : IRequest<Result<byte[]>>
    {
        public string HtmlContent { get; set; }

        public ConvertHtmlToPdfCommand(string htmlContent)
        {
            HtmlContent = htmlContent;
        }
    }
}
