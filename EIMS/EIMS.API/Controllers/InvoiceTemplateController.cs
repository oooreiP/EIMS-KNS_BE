using AutoMapper;
using EIMS.Application.DTOs.InvoiceTemplate;
using EIMS.Application.Features.InvoiceTemplate.Commands;
using EIMS.Application.Features.InvoiceTemplate.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceTemplateController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public InvoiceTemplateController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTemplate([FromBody] CreateTemplateRequest request)
        {
            var command = _mapper.Map<CreateTemplateCommand>(request);
            var result = await _sender.Send(command);
            if (result.IsFailed)
            {
                var firstError = result.Errors.FirstOrDefault();
                // Return error response
                return BadRequest(new ProblemDetails // Use ProblemDetails for standard error responses
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Invoice template creation failed",
                    Detail = firstError?.Message ?? "Invalid credentials provided." // Use the message from the Result
                });
            }
            return Ok(result.Value);
        }
        [HttpGet]
        public async Task<IActionResult> GetTemplates()
        {
            var result = await _sender.Send(new GetTemplatesQuery());
            if (result.IsFailed)
            {
                var firstError = result.Errors.FirstOrDefault();
                // Return error response
                return BadRequest(new ProblemDetails // Use ProblemDetails for standard error responses
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Invoice template get all failed",
                    Detail = firstError?.Message ?? "Invalid credentials provided." // Use the message from the Result
                });
            }
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTemplateById(int id)
        {
            var result = await _sender.Send(new GetTemplateByIdQuery(id));
            if (result.IsFailed)
            {
                var firstError = result.Errors.FirstOrDefault();
                // Return error response
                return BadRequest(new ProblemDetails // Use ProblemDetails for standard error responses
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Invoice template by ID failed",
                    Detail = firstError?.Message ?? "Invalid credentials provided." // Use the message from the Result
                });
            }
            return Ok(result.Value);
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateTemplate(int id, [FromBody] UpdateTemplateRequest request)
        {
            if (id != request.TemplateID)
                return BadRequest("ID mismatch.");
            var command = _mapper.Map<UpdateTemplateCommand>(request);

            var result = await _sender.Send(command);

            if (result.IsFailed)
                return BadRequest(result.Errors.FirstOrDefault()?.Message);

            return Ok(new { Message = "Template updated successfully" });
        }
        [HttpGet("{id}/view")]
        public async Task<IActionResult> ViewTemplate(int id)
        {
            var query = new ViewTemplateQuery(id);
            var result = await _sender.Send(query);

            if (result.IsFailed)
                return NotFound(result.Errors.FirstOrDefault()?.Message);

            // Return HTML directly so the browser/frontend can render it
            return Content(result.Value, "text/html");
        }
    }
}

