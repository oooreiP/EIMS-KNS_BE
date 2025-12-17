using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.TaxApiStatuses.Commands
{
    public class UpdateTaxApiStatusCommand : IRequest<Result>
    {
        public int TaxApiStatusID { get; set; }
        public string Code { get; set; }
        public string? StatusName { get; set; }
    }
}
