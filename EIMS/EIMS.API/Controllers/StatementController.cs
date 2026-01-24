using System;
using System.Text;
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

        [HttpGet("{id}/payments")]
        public async Task<IActionResult> GetStatementPayments(int id)
        {
            var query = new GetStatementPaymentsQuery(id);
            var result = await _sender.Send(query);

            if (result.IsFailed)
            {
                var firstError = result.Errors.FirstOrDefault();
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Get Statement Payments Failed",
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

        [HttpGet("sale/{saleId}")]
        public async Task<IActionResult> GetStatementsBySaleId(
            int saleId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? customerId = null,
            [FromQuery] string? statementCode = null,
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null,
            [FromQuery] int? periodMonth = null,
            [FromQuery] int? periodYear = null,
            [FromQuery] int? statusId = null)
        {
            var query = new GetInvoiceStatementsQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                CustomerID = customerId,
                StatementCode = statementCode,
                FromDate = fromDate,
                ToDate = toDate,
                PeriodMonth = periodMonth,
                PeriodYear = periodYear,
                StatusID = statusId,
                SalesId = saleId
            };

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
            [HttpPost("{id}/send-email")]
            public async Task<IActionResult> SendStatementEmail(int id, [FromBody] SendStatementEmailCommand command)
            {
                command.StatementId = id;
                command.RootPath = _env.ContentRootPath;

                var result = await _sender.Send(command);
                if (result.IsFailed)
                {
                    var firstError = result.Errors.FirstOrDefault();
                    return BadRequest(new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Send Statement Email Failed",
                        Detail = firstError?.Message ?? "Invalid request."
                    });
                }

                return Ok(new { message = "Statement email sent." });
            }
        [HttpPost("{id}/send-debt-reminder")]
        public async Task<IActionResult> SendDebtReminderEmail(int id, [FromBody] SendDebtReminderEmailCommand command)
        {
            command.StatementId = id;
            command.RootPath = _env.ContentRootPath;

            var result = await _sender.Send(command);
            if (result.IsFailed)
            {
                var firstError = result.Errors.FirstOrDefault();
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Send Debt Reminder Failed",
                    Detail = firstError?.Message ?? "Invalid request."
                });
            }

            return Ok(new { message = "Debt reminder email sent." });
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
        [HttpGet("{id}/preview-statement")]
        public async Task<IActionResult> PreviewHTML(int id)
        {
            string rootPath = _env.ContentRootPath;
            var query = new PreviewStatementQuery(id, rootPath);

            Result<string> result = await _sender.Send(query);
            if (result.IsFailed)
            {
                return BadRequest(new { Error = result.Errors.FirstOrDefault()?.Message });
            }
            return Content(result.Value, "text/html", Encoding.UTF8);
        }
        [HttpPost("{id}/payments")]
        public async Task<IActionResult> CreateStatementPayment(int id, [FromBody] CreateStatementPaymentCommand command)
        {
            command.StatementId = id;
            var result = await _sender.Send(command);

            if (result.IsFailed)
            {
                var firstError = result.Errors.FirstOrDefault();
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Create Statement Payment Failed",
                    Detail = firstError?.Message ?? "Invalid request."
                });
            }

            return Ok(result.Value);
        }
    }
}