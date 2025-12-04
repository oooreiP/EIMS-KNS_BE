using EIMS.Application.Features.InvoiceStatuses.Commands;
using EIMS.Application.Features.InvoiceStatuses.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceStatusesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvoiceStatusesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllInvoiceStatusesQuery());
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetInvoiceStatusByIdQuery { Id = id });
            if (result.IsFailed) return NotFound(new { message = result.Errors[0].Message });
            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInvoiceStatusCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok(new { id = result.Value, message = "Tạo trạng thái thành công" });

            return BadRequest(new { errors = result.Errors.Select(e => e.Message) });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateInvoiceStatusCommand command)
        {
            if (id != command.InvoiceStatusID) return BadRequest("ID không khớp.");

            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok(new { message = "Cập nhật thành công" });

            return BadRequest(new { errors = result.Errors.Select(e => e.Message) });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteInvoiceStatusCommand { Id = id });
            if (result.IsSuccess)
                return Ok(new { message = "Xóa thành công" });
            return BadRequest(new { errors = result.Errors.Select(e => e.Message) });
        }
    }
}

