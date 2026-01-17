using AutoMapper;
using EIMS.Application.Features.InvoiceRequests.Commands;
using EIMS.Application.Features.Invoices.Queries;
using EIMS.Application.Features.InvoiceRequests.Queries;
using EIMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceRequestController : ControllerBase
    {
        private readonly IMediator _mediator;
        public InvoiceRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(typeof(Invoice), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRequest([FromBody] CreateInvoiceRequestCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsFailed)
            {
                var firstError = result.Errors.FirstOrDefault();
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Invoice creation failed",
                    Detail = firstError?.Message ?? "Invalid credentials provided."
                });
            }
            return Ok(result.Value);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllInvoiceRequestsQuery query)
        { 
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("{id}/prefill_invoice")]
        public async Task<IActionResult> GetRequestToFillById(int id)
        {
            var query = new GetSaleRequestByIDQuery(id);
            var result = await _mediator.Send(query);

            if (result.IsFailed)
            {
                return NotFound(result.Errors);
            }

            return Ok(result.Value);
        }
    }
}
