using EIMS.Application.DTOs.Mails;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceStatements.Queries
{
    public class PreviewStatementQuery : IRequest<Result<string>>
    {
        public int StatementId { get; set; }
        public string RootPath { get; set; }

        public PreviewStatementQuery(int statementId, string rootPath)
        {
            StatementId = statementId;
            RootPath = rootPath;
        }
    }
}
