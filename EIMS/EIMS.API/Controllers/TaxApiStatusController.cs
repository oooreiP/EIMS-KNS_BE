using EIMS.Application.Features.TaxApiStatuses.Commands;
using EIMS.Application.Features.TaxApiStatuses.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxApiStatusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaxApiStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetTaxApiStatusListQuery());
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetTaxApiStatusByIdQuery(id));
            if (result.IsFailed) return NotFound(result.Errors[0].Message);
            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaxApiStatusCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsFailed) return BadRequest(result.Errors[0].Message);
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTaxApiStatusCommand command)
        {
            if (id != command.TaxApiStatusID) return BadRequest("ID không khớp.");

            var result = await _mediator.Send(command);
            if (result.IsFailed) return BadRequest(result.Errors[0].Message);
            return Ok("Cập nhật thành công.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteTaxApiStatusCommand(id));
            if (result.IsFailed) return BadRequest(result.Errors[0].Message);
            return Ok("Xóa thành công.");
        }
    }
}
