using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.TaxApiStatuses.Commands
{
    public class DeleteTaxApiStatusCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public DeleteTaxApiStatusCommand(int id) => Id = id;
    }
}
