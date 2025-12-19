using EIMS.Application.Features.InvoicePayment.Commands;
using EIMS.Application.Features.InvoicePayment.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsFailed)
            {
                var firstError = result.Errors.FirstOrDefault();
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Create Payment Failed",
                    Detail = firstError?.Message ?? "Invalid request."
                });
            }
            return Ok(result.Value);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var result = await _mediator.Send(new GetInvoicePaymentByIdQuery { Id = id });
            if (result.IsFailed)
            {
                var firstError = result.Errors.FirstOrDefault();
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = $"Get Payment with id {id} Failed",
                    Detail = firstError?.Message ?? "Invalid request."
                });
            }
            return Ok(result.Value);
        }
        [HttpGet]
        public async Task<IActionResult> GetPayments([FromQuery] GetInvoicePayments query)
        {
            var result = await _mediator.Send(query);
            var firstError = result.Errors.FirstOrDefault();
            if (result.IsFailed)
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = $"Get Paymens Failed",
                    Detail = firstError?.Message ?? "Invalid request."
                });
            return Ok(result.Value);
        }

        [HttpGet("invoice/{id}")]
        public async Task<IActionResult> GetPayments(int id)
        {
            var query = new GetPaymentsByInvoiceIdQuery(id);
            var result = await _mediator.Send(query);

            if (result.IsFailed)
            {
                return NotFound(new { message = result.Errors[0].Message });
            }

            return Ok(result.Value);
        }
    }
}
