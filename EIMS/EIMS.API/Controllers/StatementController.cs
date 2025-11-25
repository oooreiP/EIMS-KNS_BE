using AutoMapper;
using EIMS.Application.DTOs.InvoiceStatement;
using EIMS.Application.Features.InvoiceStatements.Commands;
using EIMS.Application.Features.InvoiceStatements.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StatementController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        public StatementController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateStatement([FromBody] GenerateStatementRequest request)
        {
            var response = _mapper.Map<CreateStatementCommand>(request);
            var result = await _sender.Send(response);
            if (result.IsFailed)
            {
                var firstError = result.Errors.FirstOrDefault();
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Generate Statement Failed",
                    Detail = firstError?.Message ?? "Invalid request."
                });
            }
            return Ok(result.Value);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatementById(int id)
        {
            var query = new GetStatementByIdQuery(id);
            var result = await _sender.Send(query);
            if (result.IsFailed)
            {
                var firstError = result.Errors.FirstOrDefault();
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Get Statement Failed",
                    Detail = firstError?.Message ?? "Invalid request."
                });
            }
            return Ok(result.Value);
        }
    }
}