using EIMS.Application.DTOs.TaxAPIDTO;
using EIMS.Application.Features.CQT.SubmitInvoice.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaxController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> Submit(SubmitInvoiceRequest submitInvoiceRequest)
        {
            var result = await _mediator.Send(new SubmitInvoiceToCQTCommand(submitInvoiceRequest));

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Errors);
        }
    }
}
