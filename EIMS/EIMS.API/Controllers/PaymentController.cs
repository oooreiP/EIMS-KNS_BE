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
            var result = await _mediator.Send(new GetInvoicePaymentByIdQuery { PaymentID = id });
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

        [HttpGet("sale/{saleId}")]
        public async Task<IActionResult> GetPaymentsBySaleId(
            int saleId,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null,
            [FromQuery] int? invoiceId = null,
            [FromQuery] int? customerId = null)
        {
            var query = new GetInvoicePayments
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                SearchTerm = searchTerm,
                InvoiceId = invoiceId,
                CustomerId = customerId,
                SalesId = saleId
            };

            var result = await _mediator.Send(query);
            var firstError = result.Errors.FirstOrDefault();
            if (result.IsFailed)
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Get Payments By Sale Failed",
                    Detail = firstError?.Message ?? "Invalid request."
                });
            return Ok(result.Value);
        }
        [HttpGet("monthly-debt")]
        public async Task<IActionResult> GetMonthlyDebt([FromQuery] int month, [FromQuery] int year, [FromQuery] int? customerId)
        {
            var query = new GetMonthlyDebtQuery
            {
                Month = month,
                Year = year,
                CustomerId = customerId
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
