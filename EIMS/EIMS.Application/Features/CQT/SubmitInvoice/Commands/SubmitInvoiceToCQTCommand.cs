using EIMS.Application.DTOs.Results;
using EIMS.Application.DTOs.TaxAPIDTO;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.CQT.SubmitInvoice.Commands
{
    public record SubmitInvoiceToCQTCommand(int invoiceId ) : IRequest<Result<SubmitInvoiceToCQTResult>>;
}
