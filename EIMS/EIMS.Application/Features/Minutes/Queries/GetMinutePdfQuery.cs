using EIMS.Application.DTOs.Mails;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Minutes.Queries
{
    public class GetMinutePdfQuery : IRequest<Result<FileAttachment>>
    {
        public int MinuteId { get; set; }
        public string RootPath { get; set; }

        public GetMinutePdfQuery(int minuteId, string rootPath)
        {
            MinuteId = minuteId;
            RootPath = rootPath;
        }
    }
}
