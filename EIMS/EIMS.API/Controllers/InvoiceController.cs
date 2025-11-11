using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs;
using EIMS.Application.Features.Invoices.Commands.CreateInvoice;
using EIMS.Application.Features.Invoices.Queries;
using EIMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public InvoiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Invoice), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceCommand command)
        {
            var result = await _mediator.Send(command);
            if(result.IsFailed)
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
        public async Task<IActionResult> GetAll()
        {
            var invoices = await _mediator.Send(new GetAllInvoicesQuery());
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var invoice = await _mediator.Send(new GetInvoiceByIdQuery(id));
            return invoice != null ? Ok(invoice) : NotFound();
        }
    }
}
