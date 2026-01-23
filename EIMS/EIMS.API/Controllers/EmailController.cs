using EIMS.Application.Features.Emails.Commands;
using EIMS.Application.Features.Emails.Queries;
using EIMS.Application.Features.Minutes.Queries;
using EIMS.Domain.Enums;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;
        public EmailController(IMediator mediator, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _env = env;
        }

        /// <summary>
        /// Gửi email hóa đơn kèm file PDF/XML từ Cloudinary.
        /// </summary>
        [HttpPost("{invoiceId}/send-email")]
        public async Task<IActionResult> SendInvoiceEmail(int invoiceId, [FromBody] SendInvoiceEmailCommand command)
        {
            command.InvoiceId = invoiceId;
            var result = await _mediator.Send(command);

            if (result.IsFailed)
                return BadRequest(new { success = false, message = result.Errors[0].Message });

            return Ok(new
            {
                success = true,
                message = "Email đã được gửi thành công",
                sentTo = command.RecipientEmail ?? "customer-default@email.com",
                sentAt = DateTime.UtcNow
            });
        }
        [HttpGet("preview-minutes-template")]
        public async Task<IActionResult> PreviewMinutesTemplate(MinutesType type)
        {
            var query = new PreviewMinutesTemplateQuery() { Type = type, RootPath = _env.ContentRootPath };
            var result = await _mediator.Send(query);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return File(
                result.Value.FileContent,
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                result.Value.FileName
            );
        }
        [HttpPost("{invoiceId}/send-minutes")]
        public async Task<IActionResult> SendMinutes(int invoiceId, [FromBody] SendInvoiceMinutesCommand command)
        {
            command.InvoiceId = invoiceId;
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(Result.Ok("Đã gửi biên bản thành công.")) : BadRequest(result);
        }
        [HttpPost("preview-minutes")]
        public async Task<IActionResult> PreviewMinutes([FromBody] PreviewInvoiceMinutesQuery query)
        {

            var result = await _mediator.Send(query);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }
            return File(
                result.Value.FileContent,
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                result.Value.FileName
            );
        }
    }
}
