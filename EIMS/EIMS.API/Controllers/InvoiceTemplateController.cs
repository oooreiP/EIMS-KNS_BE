using AutoMapper;
using EIMS.Application.DTOs.InvoiceTemplate;
using EIMS.Application.Features.InvoiceTemplate.Commands;
using MediatR;
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
                    Title = "Invoice tempate creation failed",
                    Detail = firstError?.Message ?? "Invalid credentials provided." // Use the message from the Result
                });
            }
            return Ok(result.Value);
        }
    }
}

