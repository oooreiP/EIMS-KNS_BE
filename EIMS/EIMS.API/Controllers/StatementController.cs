using AutoMapper;
using EIMS.Application.DTOs.InvoiceStatement;
using EIMS.Application.DTOs.Mails;
using EIMS.Application.Features.InvoiceStatements.Commands;
using EIMS.Application.Features.InvoiceStatements.Queries;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class StatementController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public StatementController(ISender sender, IMapper mapper, IWebHostEnvironment env)
        {
            _sender = sender;
            _mapper = mapper;
            _env = env;
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
        public async Task<IActionResult> GenerateBatchStatements([FromBody] GenerateAllStatementsRequest request)
        {
            // Now using the specific DTO without CustomerID
            var command = new GenerateAllStatementsCommand
            {
                Month = request.Month,
                Year = request.Year
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
        [HttpPost("send-monthly-reminders")]
        //[Authorize(Roles = "Accountant,HOD")]
        public async Task<IActionResult> SendMonthlyReminders()
        {
            var command = new SendMonthlyDebtRemindersCommand();
            var result = await _sender.Send(command);

            if (result.IsSuccess)
            {
                return Ok(new { message = $"Đã gửi thông báo nhắc nợ tới {result.Value} khách hàng." });
            }
            return BadRequest(result.Errors);
        }
        [HttpGet("{id}/export-pdf")]
        public async Task<IActionResult> ExportPdf(int id)
        {
            string rootPath = _env.ContentRootPath;
            var query = new GetStatementPdfQuery(id, rootPath);

            Result<FileAttachment> result = await _sender.Send(query);
            if (result.IsFailed)
            {
                return BadRequest(new { Error = result.Errors.FirstOrDefault()?.Message });
            }

            var fileData = result.Value;
            return File(
                fileData.FileContent,   
                "application/pdf",     
                fileData.FileName      
            );
        }
    }
}