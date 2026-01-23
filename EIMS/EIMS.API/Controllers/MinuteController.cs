using EIMS.Application.DTOs.Minutes;
using EIMS.Application.Features.Minutes.Commands;
using EIMS.Application.Features.Minutes.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinuteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MinuteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateMinuteWithFileDto dto)
        {
            var command = new CreateMinuteInvoiceCommand(dto);
            var resultId = await _mediator.Send(command);
            return Ok(new { Id = resultId, Message = "Đã upload biên bản lên Cloud và gửi kế toán thành công." });
        }
        // GET: api/minutes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetMinuteInvoiceByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
                return Ok(result.Value);

            return NotFound(result.Errors);
        }

        // GET: api/minutes?pageIndex=1&pageSize=10&searchTerm=abc&status=1
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetMinuteInvoicesQuery query)
        {
            var result = await _mediator.Send(query);

            // Vì GetAll hiếm khi lỗi logic, thường chỉ trả về list rỗng
            return Ok(result.Value);
        }
        [HttpPost("sign-seller/{id}")]
        public async Task<IActionResult> SignBySeller(int id)
        {
            var command = new SignMinuteInvoiceCommand(id, "ĐẠI DIỆN BÊN B");

            var result = await _mediator.Send(command);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(new { Message = "Đã ký số thành công", NewUrl = result.Value });
        }
    }
}
