using EIMS.Application.Features.Emails.Commands;
using EIMS.Application.Features.Emails.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTemplatesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmailTemplatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/email-templates?searchTerm=invoice
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetEmailTemplatesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result.Value);
        }
        // GET: api/email-templates/variables/invoice
        [HttpGet("variables/{category}")]
        public async Task<IActionResult> GetVariables(string category)
        {
            var query = new GetEmailTemplateVariablesQuery { Category = category };
            var result = await _mediator.Send(query);

            if (result.IsFailed) return BadRequest(result.Errors);
            return Ok(result.Value);
        }
        // GET: api/email-templates/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetEmailTemplateByIdQuery { Id = id });
            if (result.IsFailed) return NotFound(result.Errors);
            return Ok(result.Value);
        }
        [HttpGet("base-content/{code}")]
        public async Task<IActionResult> GetBaseContent(string code)
        {
            var result = await _mediator.Send(new GetBaseContentByCodeQuery(code));

            if (result.IsFailed)
                return NotFound(result.Errors);
            return Content(result.Value, "text/html", Encoding.UTF8);
        }
        // POST: api/email-templates
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmailTemplateCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsFailed) return BadRequest(result.Errors);
            return Ok(new { id = result.Value, message = "Tạo mẫu email thành công" });
        }

        // PUT: api/email-templates/5
        // PUT: api/email-templates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEmailTemplateCommand command)
        {
            if (id != command.EmailTemplateID) return BadRequest("ID không khớp");

            var result = await _mediator.Send(command);
            if (result.IsFailed) return BadRequest(result.Errors);
            return Ok(new { message = "Cập nhật thành công" });
        }

        // DELETE: api/email-templates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteEmailTemplateCommand { Id = id });
            if (result.IsFailed) return BadRequest(result.Errors);
            return Ok(new { message = "Xóa thành công" });
        }
    }
}
