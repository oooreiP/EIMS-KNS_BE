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
        [HttpGet]
        public async Task<IActionResult> GetStatements([FromQuery] GetInvoiceStatementsQuery query)
        {
            var result = await _sender.Send(query);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }
        [HttpPost("generate-batch")]
        public async Task<IActionResult> GenerateBatchStatements([FromBody] GenerateStatementRequest request)
        {
            // Note: We use the same Request DTO (Year/Month), but map it to the NEW Command
            var command = new GenerateAllStatementsCommand
            {
                Month = request.Month,
                Year = request.Year
                // User ID is filled by your UserIdPopulationBehavior automatically? 
                // If not, ensure you set it here or in the behavior.
            };

            var result = await _sender.Send(command);

            if (result.IsFailed)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Batch Generation Failed",
                    Detail = result.Errors.FirstOrDefault()?.Message
                });
            }
            return Ok(result.Value);
        }
    }
}