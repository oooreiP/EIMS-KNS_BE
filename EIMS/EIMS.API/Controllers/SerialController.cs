using AutoMapper;
using EIMS.Application.DTOs.Serials;
using EIMS.Application.Features.Serial.Commands;
using EIMS.Application.Features.Serial.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SerialController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        public SerialController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetSerials()
        {
            var result = await _sender.Send(new GetSerialQuery());
            if (result.IsFailed)
            {
                var firstError = result.Errors.FirstOrDefault();
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Get Serials Failed",
                    Detail = firstError?.Message ?? "Invalid request."
                });
            }
            return Ok(result.Value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSerial([FromBody] CreateSerialRequest request)
        {
            var command = _mapper.Map<CreateSerialCommand>(request);
            var result = await _sender.Send(command);
            if (result.IsFailed)
            {
                var firstError = result.Errors.FirstOrDefault();
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Create Serials Failed",
                    Detail = firstError?.Message ?? "Invalid request."
                });
            }
            return Ok(result.Value);
        }
    }
}